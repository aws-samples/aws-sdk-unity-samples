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
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

using Amazon.Runtime;
using Amazon.Runtime.Internal.Settings;

namespace Amazon.Util
{
    /// <summary>
    /// This class allows AWS credentials to be registered with the SDK so that they can later be reference by
    /// a profile name. The AWS credentials will be available for AWS Toolkit for Visual Studio and 
    /// AWS Tools for Windows PowerShell. 
    /// <para>
    /// The credentials are stored under the current users AppData folder encrypted using Windows Data Protection API.
    /// </para>
    /// <para>
    /// To reference the profile from an application's App.config or Web.config use the AWSProfileName setting.
    /// <code>
    /// &lt;?xml version="1.0" encoding="utf-8" ?&gt;
    /// &lt;configuration&gt;
    ///     &lt;appSettings&gt;
    ///         &lt;add key="AWSProfileName" value="development"/&gt;
    ///     &lt;/appSettings&gt;
    /// &lt;/configuration&gt;
    /// </code>
    /// </para>
    /// </summary>
    public static class ProfileManager
    {
        /// <summary>
        /// Register a profile that can later be referenced by the profileName.
        /// This profile will only be visible for the current user.
        /// </summary>
        /// <param name="profileName">Name given to the AWS credentials.</param>
        /// <param name="accessKeyId">The AWS access key id</param>
        /// <param name="secretKey">The AWS secret key</param>
        public static void RegisterProfile(string profileName, string accessKeyId, string secretKey)
        {
            var settings = PersistenceManager.Instance.GetSettings(SettingsConstants.RegisteredProfiles);
            SettingsCollection.ObjectSettings os = null;
            os = settings.FirstOrDefault(x => string.Equals(x[SettingsConstants.DisplayNameField], profileName, StringComparison.OrdinalIgnoreCase));
            if(os == null)
                os = settings.NewObjectSettings(Guid.NewGuid().ToString());

            os[SettingsConstants.DisplayNameField] = profileName;
            os[SettingsConstants.AccessKeyField] = accessKeyId;
            os[SettingsConstants.SecretKeyField] = secretKey;

            PersistenceManager.Instance.SaveSettings(SettingsConstants.RegisteredProfiles, settings);
        }

        /// <summary>
        /// Unregistered a profile from the SDK account store.
        /// </summary>
        /// <param name="profileName">The name of the profile to remove.</param>
        public static void UnregisterProfile(string profileName)
        {
            var settings = PersistenceManager.Instance.GetSettings(SettingsConstants.RegisteredProfiles);
            var os = settings.FirstOrDefault(x => string.Equals(x[SettingsConstants.DisplayNameField], profileName, StringComparison.OrdinalIgnoreCase));
            if (os != null)
            {
                settings.Remove(os.UniqueKey);
                PersistenceManager.Instance.SaveSettings(SettingsConstants.RegisteredProfiles, settings);
            }
        }

        /// <summary>
        /// List the profile names registered with the SDK account store.
        /// </summary>
        /// <returns>The profile names.</returns>
        public static IEnumerable<string> ListProfileNames()
        {
            List<string> names = new List<string>();

            var settings = PersistenceManager.Instance.GetSettings(SettingsConstants.RegisteredProfiles);
            foreach (var os in settings)
            {
                names.Add(os[SettingsConstants.DisplayNameField]);
            }

            return names;
        }

        /// <summary>
        /// Tries to get the AWS credentials from the SDK account store.
        /// </summary>
        /// <param name="profileName">The profile to get the credentials for.</param>
        /// <param name="credentials">Outputs the credentials for the profile.</param>
        /// <returns>Returns true if the profile exists otherwise false is returned.</returns>
        public static bool TryGetAWSCredentials(string profileName, out AWSCredentials credentials)
        {
            var settings = PersistenceManager.Instance.GetSettings(SettingsConstants.RegisteredProfiles);
            var os = settings.FirstOrDefault(x => string.Equals(x[SettingsConstants.DisplayNameField], profileName, StringComparison.OrdinalIgnoreCase));
            if (os == null)
            {
                credentials = null;
                return false;
            }

            credentials = new BasicAWSCredentials(os[SettingsConstants.AccessKeyField], os[SettingsConstants.SecretKeyField]);
            return true;
        }

        /// <summary>
        /// Gets the AWS credentials from the SDK account store. 
        /// </summary>
        /// <param name="profileName">The profile to get the credentials for.</param>
        /// <returns>The AWS credentials for the profile.</returns>
        /// <exception cref="AmazonClientException">Thrown if the profile does not exist</exception>
        public static AWSCredentials GetAWSCredentials(string profileName)
        {
            AWSCredentials credentials;
            if (!TryGetAWSCredentials(profileName, out credentials))
                throw new AmazonClientException(string.Format(CultureInfo.InvariantCulture, "The account name {0} has not been registered.", profileName));

            return credentials;
        }
    }
}
