using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Text.RegularExpressions;
using Amazon.SimpleEmail;
using Amazon.Runtime;
using Amazon.CognitoIdentity;
using Amazon;
using Amazon.SimpleEmail.Model;
using System.Collections.Generic;

namespace AWSSDK.Examples
{
    public class SESExample : MonoBehaviour
    {
        public string IdentityPoolId = "";
 
        public string CognitoIdentityRegion = RegionEndpoint.USEast1.SystemName;

        private RegionEndpoint _CognitoIdentityRegion
        {
            get { return RegionEndpoint.GetBySystemName(CognitoIdentityRegion); }
        }

        public string SESRegion = RegionEndpoint.USEast1.SystemName;

        private RegionEndpoint _SESRegion
        {
            get { return RegionEndpoint.GetBySystemName(SESRegion); }
        }

        public InputField txtToEmailAddress;
        public InputField txtEmailContent;
        public InputField txtEmailSubject;
        public InputField txtFromEmailAddress;

        public Button btnSend;
        private const string EmailAddressValidator = @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$";

        private IAmazonSimpleEmailService _sesClient;
        private AWSCredentials _credentials;

        private AWSCredentials Credentials
        {
            get
            {
                if (_credentials == null)
                    _credentials = new CognitoAWSCredentials(IdentityPoolId, _CognitoIdentityRegion);
                return _credentials;
            }
        }

        private IAmazonSimpleEmailService Client
        {
            get
            {
                if (_sesClient == null)
                {
                    _sesClient = new AmazonSimpleEmailServiceClient(Credentials, _SESRegion);
                }
                return _sesClient;
            }
        }

        void Start()
        {
            UnityInitializer.AttachToGameObject(this.gameObject);
            btnSend.onClick.AddListener(SendEmail);
        }



        private void SendEmail()
        {
            var toEmailAddress = txtToEmailAddress.text;
            var emailBody = txtEmailContent.text;
            var subject = txtEmailSubject.text;
            var fromEmailAddress = txtFromEmailAddress.text;

            if (!ValidateEmail(toEmailAddress))
                return;

            if (!ValidateEmail(fromEmailAddress))
                return;

            if (emailBody == null || emailBody.Replace(" ", "").Length == 0)
                return;

            Client.SendEmailAsync(new SendEmailRequest()
                {
                    Destination = new Destination()
                    {
                        ToAddresses = new List<string>() { toEmailAddress }
                    },
                    Message = new Message()
                    {
                        Subject = new Content(subject),
                        Body = new Body(new Content(emailBody))
                    },
                    ReplyToAddresses = new List<string> { fromEmailAddress },
                    Source = fromEmailAddress
                }, (responseObj) =>
                {
                    if (responseObj.Exception == null)
                    {
                        var response = responseObj.Response;
                        Debug.Log(@"Message sent");
                        Debug.Log(string.Format(@"Http Status Code = {0}", response.HttpStatusCode));
                    }
                    else
                    {
                        Debug.LogException(responseObj.Exception);
                    }
                });
        }

        private bool ValidateEmail(string email)
        {
            return Regex.IsMatch(email, EmailAddressValidator, RegexOptions.IgnoreCase);
        }

    }
}
