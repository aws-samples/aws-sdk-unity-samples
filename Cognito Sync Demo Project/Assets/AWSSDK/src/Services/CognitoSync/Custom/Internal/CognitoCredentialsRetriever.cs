#define AWSSDK_UNITY
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
using System.Collections.Generic;
using System.IO;
using System.Linq;

using Amazon.CognitoSync.Model;
using Amazon.CognitoSync.Model.Internal.MarshallTransformations;
using Amazon.Runtime;
using Amazon.Runtime.Internal;
using Amazon.Runtime.Internal.Auth;
using Amazon.Runtime.Internal.Transform;
using Amazon.Util.Internal;
using System.Reflection;
using System.Globalization;

using Amazon.CognitoIdentity;


namespace Amazon.CognitoSync.Internal
{
    /// <summary>
    /// This handler overrides the base CredentialsRetriever which is removed from the pipeline. When
    /// the service client is using the CognitoAWSCredentials credentials object it makes sure that all
    /// CognitoSync calls have the latest identity id and identity pool id.
    /// </summary>
    public class CognitoCredentialsRetriever : CredentialsRetriever
    {
        public CognitoCredentialsRetriever(AWSCredentials credentials)
            : base(credentials)
        { }

#if BCL || AWSSDK_UNITY
        protected override void PreInvoke(IExecutionContext executionContext)
        {
            base.PreInvoke(executionContext);

            // Only configure IdentityPoolId and IdentityId when using CognitoAWSCredentials
            var cognitoCredentials = Credentials as CognitoAWSCredentials;
            if (cognitoCredentials != null)
            {
                SetIdentity(executionContext, cognitoCredentials.GetIdentityId(), cognitoCredentials.IdentityPoolId);       
            }
        }
#elif AWS_ASYNC_API
        public override async System.Threading.Tasks.Task<T> InvokeAsync<T>(IExecutionContext executionContext)
        {
            T result = await base.InvokeAsync<T>(executionContext);

            // Only configure IdentityPoolId and IdentityId when using CognitoAWSCredentials
            var cognitoCredentials = Credentials as CognitoAWSCredentials;
            if (cognitoCredentials != null)
            {                
                string identityId = await cognitoCredentials.GetIdentityIdAsync();
                SetIdentity(executionContext, identityId, cognitoCredentials.IdentityPoolId);                
            }

            return result;
        }
#endif

        private void SetIdentity(IExecutionContext executionContext, string identityId, string identityPoolId)
        {
            var request = executionContext.RequestContext.OriginalRequest;
            var syncRequest = request as AmazonCognitoSyncRequest;
            if (syncRequest != null)
            {
                // Set the IdentityId and IdentityPoolId fields on the request.
                CSRequestCache.SetFields(syncRequest, identityPoolId, identityId);
            }
        }

        /// <summary>
        /// A cache of objects that store the reflection objects. This is done so that reflection is only used once in order to 
        /// set the fields on the request.
        /// </summary>
        private static class CSRequestCache
        {
            /// <summary>
            /// This class holds on to the reflection objects to invoke the setters for IdentityId and IdentityPoolId for the given type.
            /// </summary>
            private class CSRequest
            {
                private ITypeInfo requestType;
                private PropertyInfo identityPoolIdProperty;
                private PropertyInfo identityIdProperty;
                public static ITypeInfo SyncRequestType = TypeFactory.GetTypeInfo(typeof(AmazonCognitoSyncRequest));

                public CSRequest(Type requestType)
                {
                    this.requestType = TypeFactory.GetTypeInfo(requestType);

                    if (!SyncRequestType.IsAssignableFrom(this.requestType))
                        throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "Unable to assign {0} from {1}",
                            SyncRequestType.FullName, requestType.FullName));


                    this.identityIdProperty = this.requestType.GetProperty("IdentityId");
                    this.identityPoolIdProperty = this.requestType.GetProperty("IdentityPoolId");
                }

                public void SetFields(AmazonCognitoSyncRequest request, string identityPoolId, string identityId)
                {
                    if (identityIdProperty != null)
                        identityIdProperty.SetValue(request, identityId, null);
                    if (identityPoolIdProperty != null)
                        identityPoolIdProperty.SetValue(request, identityPoolId, null);
                }
            }

            private static Dictionary<Type, CSRequest> requestCache = new Dictionary<Type, CSRequest>();
            static CSRequestCache()
            {
                PopulateCache();
            }

            private static void PopulateCache()
            {
                var sourceAssembly = CSRequest.SyncRequestType.Assembly;
#if BCL || AWSSDK_UNITY
                var allTypes = sourceAssembly.GetTypes();
#else
                var allTypesInfos = sourceAssembly.DefinedTypes;
                var allTypes = new List<Type>();
                foreach (var typeInfo in allTypesInfos)
                    allTypes.Add(typeInfo.AsType());
#endif
                // Look up all CognitoSync request objects for caching.
                foreach (var type in allTypes)
                {
                    var typeInfo = TypeFactory.GetTypeInfo(type);
                    if (CSRequest.SyncRequestType.IsAssignableFrom(typeInfo) && !typeInfo.Equals(CSRequest.SyncRequestType))
                    {
                        var csRequest = new CSRequest(type);
                        requestCache[type] = csRequest;
                    }
                }
            }

            /// <summary>
            /// Set the IdentityId and IdentityPoolId properties on the request object
            /// </summary>
            /// <param name="request"></param>
            /// <param name="identityPoolId"></param>
            /// <param name="identityId"></param>
            public static void SetFields(AmazonCognitoSyncRequest request, string identityPoolId, string identityId)
            {
                Type requestType = request.GetType();
                CSRequest csRequest;
                if (requestCache.TryGetValue(requestType, out csRequest))
                {
                    csRequest.SetFields(request, identityPoolId, identityId);
                }
            }
        }
    }
}
