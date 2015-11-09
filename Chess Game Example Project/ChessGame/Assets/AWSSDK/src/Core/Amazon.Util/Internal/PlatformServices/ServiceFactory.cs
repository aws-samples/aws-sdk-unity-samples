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

using System.Diagnostics.CodeAnalysis;

namespace Amazon.Util.Internal.PlatformServices
{
    public class ServiceFactory
    {
        internal const string NotImplementedErrorMessage =
            "This functionality is not implemented in the portable version of this assembly. "+
            "You should reference the AWSSDK.Core NuGet package from your main application project in order to reference the platform-specific implementation.";

        enum InstantiationModel
        {
            Singleton,
            InstancePerCall
        }

        private static readonly object _lock = new object();
        private static bool _factoryInitialized = false;

        private static IDictionary<Type, Type> _mappings =
            new Dictionary<Type, Type>()
            {
                {typeof(IApplicationSettings),typeof(ApplicationSettings)},
                {typeof(INetworkReachability),typeof(NetworkReachability)},
                {typeof(IApplicationInfo),typeof(ApplicationInfo)},
                {typeof(IEnvironmentInfo),typeof(EnvironmentInfo)}
            };

        private IDictionary<Type, InstantiationModel> _instantationMappings =
            new Dictionary<Type, InstantiationModel>()
            {
                {typeof(IApplicationSettings), InstantiationModel.InstancePerCall},
                {typeof(INetworkReachability), InstantiationModel.Singleton},
                {typeof(IApplicationInfo),InstantiationModel.Singleton},
                {typeof(IEnvironmentInfo),InstantiationModel.Singleton}
            };

        private IDictionary<Type, object> _singletonServices =
            new Dictionary<Type, object>();

        private ServiceFactory()
        {
            // Instantiate services registered as singletons.
            foreach (var service in _instantationMappings)
            {
                var serviceType = service.Key;
                if (service.Value == InstantiationModel.Singleton)
                {
                    var serviceInstance = Activator.CreateInstance(_mappings[serviceType]);
                    _singletonServices.Add(serviceType, serviceInstance);
                }
            }
            _factoryInitialized = true;
        }

        [SuppressMessage("Microsoft.Usage", "CA2211:NonConstantFieldsShouldNotBeVisible")]
        public static ServiceFactory Instance = new ServiceFactory();

        public static void RegisterService<T>(Type serviceType)
        {
            if (_factoryInitialized)
            {
                throw new InvalidOperationException(
                    "New services can only be registered before ServiceFactory is accessed by calling ServiceFactory.Instance .");
            }

            lock (_lock)
            {
                _mappings[typeof(T)] = serviceType;
            }
        }

        public T GetService<T>()
        {
            var serviceType = typeof(T);
            if (_instantationMappings[serviceType] == InstantiationModel.Singleton)
            {
                return (T)_singletonServices[serviceType];
            }

            var concreteType = GetServiceType<T>();
            return (T)Activator.CreateInstance(concreteType);
        }

        private static Type GetServiceType<T>()
        {
            lock (_lock)
            {
                return _mappings[typeof(T)];
            }
        }
    }
}
