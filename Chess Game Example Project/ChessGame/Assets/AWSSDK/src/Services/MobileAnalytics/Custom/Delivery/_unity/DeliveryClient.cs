//
// Copyright 2014-2015 Amazon.com, 
// Inc. or its affiliates. All Rights Reserved.
// 
// Licensed under the Amazon Software License (the "License"). 
// You may not use this file except in compliance with the 
// License. A copy of the License is located at
// 
//     http://aws.amazon.com/asl/
// 
// or in the "license" file accompanying this file. This file is 
// distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR 
// CONDITIONS OF ANY KIND, express or implied. See the License 
// for the specific language governing permissions and 
// limitations under the License.
//
using System;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using System.Net;

using Amazon.Runtime;
using Amazon.CognitoIdentity;

using Amazon.MobileAnalytics.MobileAnalyticsManager;
using Amazon.MobileAnalytics;
using Amazon.MobileAnalytics.Model;

using ThirdParty.Json.LitJson;
using Amazon.Runtime.Internal.Util;


namespace Amazon.MobileAnalytics.MobileAnalyticsManager.Internal
{
    /// <summary>
    /// Delivery client periodically sends events in local persistent storage to Mobile Analytics server.
    /// Once the events is delivered successfully, those events would be deleted from local storage.
    /// </summary>
    internal class DeliveryClient : IDeliveryClient
    {
        private Logger _logger = Logger.GetLogger(typeof(DeliveryClient));


        private static bool _deliveryInProgress = false;
        private static object _deliveryLock = new object();

        private readonly IDeliveryPolicyFactory _policyFactory;
        private IEventStore _eventStore;

        private Amazon.Runtime.Internal.ClientContext _clientContext;
        private AmazonMobileAnalyticsClient _mobileAnalyticsLowLevelClient;
        private string _appId;
        private List<IDeliveryPolicy> _deliveryPolicies;

        /// <summary>
        /// Constructor of <see cref="Amazon.MobileAnalytics.MobileAnalyticsManager.DeliveryClient"/> class.
        /// </summary>
        /// <param name="isDataAllowed">If set to <c>true</c> The delivery will be attempted even on Data Network, else it will be only attempted on Wifi.</param>
        /// <param name="clientContext">An instance of ClientContext <see cref="Amazon.MobileAnalytics.MobileAnalyticsManager.Internal.ClientContext"/></param>
        /// <param name="credentials">An instance of Credentials <see cref="Amazon.Runtime.AWSCredentials"/></param>
        /// <param name="regionEndPoint">Region end point <see cref="Amazon.RegionEndpoint"/></param>
        public DeliveryClient(bool isDataAllowed, Amazon.Runtime.Internal.ClientContext clientContext, AWSCredentials credentials, RegionEndpoint regionEndPoint) :
            this(new DeliveryPolicyFactory(isDataAllowed), clientContext, credentials, regionEndPoint)
        {
        }

        /// <summary>
        /// Constructor of <see cref="Amazon.MobileAnalytics.MobileAnalyticsManager.DeliveryClient"/> class.
        /// </summary>
        /// <param name="isDataAllowed">An instance of IDeliveryPolicyFactory <see cref="Amazon.MobileAnalytics.MobileAnalyticsManager.Internal.IDeliveryPolicyFactory"/></param>
        /// <param name="clientContext">An instance of ClientContext <see cref="Amazon.MobileAnalytics.MobileAnalyticsManager.Internal.ClientContext"/></param>
        /// <param name="credentials">An instance of Credentials <see cref="Amazon.Runtime.AWSCredentials"/></param>
        /// <param name="regionEndPoint">Region end point <see cref="Amazon.RegionEndpoint"/></param>
        public DeliveryClient(IDeliveryPolicyFactory policyFactory, Amazon.Runtime.Internal.ClientContext clientContext, AWSCredentials credentials, RegionEndpoint regionEndPoint)
        {
            _policyFactory = policyFactory;
            _mobileAnalyticsLowLevelClient = new AmazonMobileAnalyticsClient(credentials, regionEndPoint);
            _clientContext = clientContext;
            _appId = clientContext.AppID;
            _eventStore = new SQLiteEventStore(AWSConfigsMobileAnalytics.MaxDBSize, AWSConfigsMobileAnalytics.DBWarningThreshold);
            _deliveryPolicies = new List<IDeliveryPolicy>();
            _deliveryPolicies.Add(_policyFactory.NewConnectivityPolicy());
            _deliveryPolicies.Add(_policyFactory.NewBackgroundSubmissionPolicy());
        }

        /// <summary>
        /// Enqueues the events for delivery. The event is stored in an <see cref="Amazon.MobileAnalytics.MobileAnalyticsManager.Internal.IEventStore"/>.
        /// </summary>
        /// <param name="eventObject">Event object. <see cref="Amazon.MobileAnalytics.Model.Event"/></param>
        public void EnqueueEventsForDelivery(Amazon.MobileAnalytics.Model.Event eventObject)
        {
            ThreadPool.QueueUserWorkItem(new WaitCallback(delegate
            {
                string eventString = eventObject.MarshallToJson();
                bool eventStored = false;

                try
                {
                    eventStored = _eventStore.PutEvent(eventString, _appId);
                }
                catch (Exception e)
                {
                    _logger.Error(e, "Event {0} is unable to be stored.", eventObject.EventType);
                }

                if (eventStored)
                {
                    _logger.DebugFormat("Event {0} is queued for delivery", eventObject.EventType);
                }
                else
                {
                    EventStoreException e = new EventStoreException("Event cannot be stored.");
                    _logger.Error(e, "Event {0} is unable to be queued for delivery.", eventObject.EventType);
                }

            }));
        }

        /// <summary>
        /// Set custom policies to the delivery client. This will allow you to fine grain control on when an attempt should be made to deliver the events on the service.
        /// </summary>
        /// <param name="policy">An instance of <see cref="Amazon.MobileAnalytics.MobileAnalyticsManager.IDeliveryPolicy"/></param>
        public void AddDeliveryPolicies(IDeliveryPolicy policy)
        {
            if (policy != null)
                _deliveryPolicies.Add(policy);
        }

        /// <summary>
        /// Attempts the delivery of all the events from local store to service
        /// </summary>
        public void AttemptDelivery()
        {
            lock (_deliveryLock)
            {
                if (_deliveryInProgress)
                {
                    _logger.InfoFormat("Delivery already in progress, failing new delivery");
                    return;
                }
                _deliveryInProgress = true;
            }

            if (_mobileAnalyticsLowLevelClient == null)
            {
                throw new InvalidOperationException("You must set Client before attempting delivery");
            }
            AttemptDelivery(_deliveryPolicies);
        }


        /// <summary>
        /// Attempts the delivery. 
        /// It will fail delivery if any of the policies.isAllowed() returns false.
        /// The policies are attmpted in batches of fixed size. To increase or decrease the size of bytes 
        /// transfered per batch you can awsconfig.xml and configure the maxRequestSize property. 
        /// </summary>
        /// <param name="policies">list of <see cref="Amazon.MobileAnalytics.MobileAnalyticsManager.IDeliveryPolicy"/></param>
        private void AttemptDelivery(List<IDeliveryPolicy> policies)
        {
            //validate all the policies before attempting the delivery
            foreach (IDeliveryPolicy policy in policies)
            {
                if (!policy.IsAllowed())
                {
                    _logger.InfoFormat("Policy restriction: {0}", policy.GetType().Name);
                    lock (_deliveryLock)
                    {
                        _deliveryInProgress = false;
                    }
                    return;
                }
            }

            List<string> rowIds = new List<string>();

            long maxRequestSize = AWSConfigsMobileAnalytics.MaxRequestSize;
            List<JsonData> eventList = _eventStore.GetAllEvents(_appId);

            if (eventList.Count == 0)
            {
                _logger.InfoFormat("No Events to deliver");
                lock (_deliveryLock)
                {
                    _deliveryInProgress = false;
                }
                return;
            }

            long eventsLength = 0L;

            List<Amazon.MobileAnalytics.Model.Event> eventArray = new List<Amazon.MobileAnalytics.Model.Event>();
            foreach (JsonData eventData in eventList)
            {
                eventsLength += ((string)eventData["event"]).Length;
                if (eventsLength < maxRequestSize)
                {
                    string eventString = (string)eventData["event"];

                    _logger.InfoFormat("Event string is {0}", eventString);

                    Amazon.MobileAnalytics.Model.Event _analyticsEvent = Amazon.MobileAnalytics.Model.Event.UnmarshallFromJson(eventString);
                    eventArray.Add(_analyticsEvent);
                    rowIds.Add(eventData["id"].ToString());
                }
                else
                {
                    SubmitEvents(rowIds, eventArray, HandleResponse);
                    rowIds = new List<string>();
                    eventArray = new List<Amazon.MobileAnalytics.Model.Event>();
                    eventsLength = 0L;
                }
            }

            SubmitEvents(rowIds, eventArray, HandleResponse);
        }

        /// <summary>
        /// Submits a single batch of events to the service.
        /// </summary>
        /// <param name="rowIds">Row identifiers. The list of rowIds is returned back once the service execution completes on the background thread.</param>
        /// <param name="eventArray">All the events that need to be submitted</param>
        /// <param name="callback">Callback Handler once the Service Attempts the delivery</param>
        private void SubmitEvents(List<string> rowIds, List<Amazon.MobileAnalytics.Model.Event> eventList, AmazonServiceCallback<PutEventsRequest, PutEventsResponse> callback)
        {
            if (eventList == null || eventList.Count == 0)
            {
                lock (_deliveryLock)
                {
                    _deliveryInProgress = false;
                }
                return;
            }

            PutEventsRequest putRequest = new PutEventsRequest();
            putRequest.Events = eventList;
            putRequest.ClientContext = Convert.ToBase64String(
                                        System.Text.Encoding.UTF8.GetBytes(_clientContext.ToJsonString()));
            putRequest.ClientContextEncoding = "base64";
            _logger.DebugFormat("Client Context is : {0}", _clientContext.ToJsonString());

            AsyncOptions options = new AsyncOptions();
            options.ExecuteCallbackOnMainThread = false;
            options.State = rowIds;

            _mobileAnalyticsLowLevelClient.PutEventsAsync(putRequest, callback, options);
        }

        /// <summary>
        /// Call to handle the response for each delivery Attempt.
        /// Notifies all the policies once delivery attempt has been completed.
        /// If the delivery is successful then it deletes the events from the <see cref="Amazon.MobileAnalytics.MobileAnalyticsManager.Internal.IEventStore"/>
        /// If the delivery was not successful then depending on the response the event may be deleted (e.g. 400 Http response , non throttle scenario) or the delivery count may be incremented.
        /// </summary>
        /// <param name="result">result <see cref="Amazon.Runtime.AmazonServiceResult"/></param>
        private void HandleResponse(AmazonServiceResult<PutEventsRequest, PutEventsResponse> result)
        {
            List<string> rowsToUpdate = (List<string>)(result.state);
            bool success = false;
            bool retriable = false;

            if (result.Exception == null)
            {
                success = true;
            }
            else if (result.Exception is Amazon.MobileAnalytics.Model.BadRequestException)
            {
                retriable = true;
            }
            else if (result.Exception is AmazonMobileAnalyticsException)
            {
                if ( string.Equals(((AmazonMobileAnalyticsException)(result.Exception)).ErrorCode,"ThrottlingException", StringComparison.CurrentCultureIgnoreCase) ||
                    ((AmazonMobileAnalyticsException)(result.Exception)).StatusCode >= HttpStatusCode.InternalServerError || ((AmazonMobileAnalyticsException)(result.Exception)).StatusCode == 0)

                {
                    retriable = true;
                }
            }


            if (success)
            {
                _logger.InfoFormat("Deliver {0} events succefully and delete those events.", rowsToUpdate.Count);
                _eventStore.DeleteEvent(rowsToUpdate);
            }
            else
            {
                if (retriable)
                {
                    _logger.InfoFormat("Unable to deliver {0} events. events will be retried in next attempt.", rowsToUpdate.Count);
                    _eventStore.IncrementDeliveryAttempt(rowsToUpdate);
                }
                else
                {
                    _logger.InfoFormat("Delivery of {0} events failed. The error is not retriable. Delete those events from local storage.", rowsToUpdate.Count);
                    _eventStore.DeleteEvent(rowsToUpdate);
                }
            }

            foreach (IDeliveryPolicy policy in _deliveryPolicies)
            {
                policy.HandleDeliveryAttempt(success);
            }

            lock (_deliveryLock)
            {
                _deliveryInProgress = false;
            }
        }
    }
}
