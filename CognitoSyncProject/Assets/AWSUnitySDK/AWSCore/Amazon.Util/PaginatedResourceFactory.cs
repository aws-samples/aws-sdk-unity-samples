/*
 * Copyright 2014-2014 Amazon.com, Inc. or its affiliates. All Rights Reserved.
 *
 *
 * Licensed under the AWS Mobile SDK for Unity Developer Preview License Agreement (the "License").
 * You may not use this file except in compliance with the License.
 * A copy of the License is located in the "license" file accompanying this file.
 * See the License for the specific language governing permissions and limitations under the License.
 *
 */
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Globalization;

namespace Amazon.Util
{
    internal static class PaginatedResourceFactory
    {
        internal static object Create<ItemType, RequestType, ResponseType>(PaginatedResourceInfo pri)
        {
            pri.Verify();

            return Create<ItemType, RequestType, ResponseType>(pri.Client, pri.MethodName, pri.Request, pri.TokenRequestPropertyPath, pri.TokenResponsePropertyPath, pri.ItemListPropertyPath);
        }

        private static PaginatedResource<ItemType> Create<ItemType, RequestType, ResponseType>
            (object client, string methodName, object request, string tokenRequestPropertyPath, string tokenResponsePropertyPath, string itemListPropertyPath)
        {
            ITypeInfo clientType = TypeFactory.GetTypeInfo(client.GetType());
            MethodInfo fetcherMethod = clientType.GetMethod(methodName);

            Func<RequestType, ResponseType> call = (req) =>
            {
                return (ResponseType)fetcherMethod.Invoke(client, new object[] { req });
            };


            return Create<ItemType, RequestType, ResponseType>(call, (RequestType)request, tokenRequestPropertyPath, tokenResponsePropertyPath, itemListPropertyPath);
        }

        private static PaginatedResource<ItemType> Create<ItemType, RequestType, ResponseType>
            (Func<RequestType, ResponseType> call, RequestType request, string tokenRequestPropertyPath, string tokenResponsePropertyPath, string itemListPropertyPath)
        {
            Func<string, Marker<ItemType>> fetcher =
                token =>
                {
                    List<ItemType> currentItems;
                    string nextToken;

                    SetPropertyValueAtPath(request, tokenRequestPropertyPath, token);
                    ResponseType nextSet = call(request);

                    nextToken = GetPropertyValueFromPath<string>(nextSet, tokenResponsePropertyPath);
                    currentItems = GetPropertyValueFromPath<List<ItemType>>(nextSet, itemListPropertyPath);

                    return new Marker<ItemType>(currentItems, nextToken);
                };
            return new PaginatedResource<ItemType>(fetcher);
        }

        private static void SetPropertyValueAtPath(object instance, string path, string value)
        {
            String[] propPath = path.Split('.');
            object currentValue = instance;
            Type currentType = instance.GetType();
            PropertyInfo currentProperty = null;
            int i = 0;
            for (; i < propPath.Length - 1; i++)
            {
                string property = propPath[i];
                currentProperty = TypeFactory.GetTypeInfo(currentType).GetProperty(property);
                currentValue = currentProperty.GetValue(currentValue, null);
                currentType = currentProperty.PropertyType;
            }
            currentProperty = TypeFactory.GetTypeInfo(currentType).GetProperty(propPath[i]);
            currentProperty.SetValue(currentValue, value, null);
        }
        private static T GetPropertyValueFromPath<T>(object instance, string path)
        {
            String[] propPath = path.Split('.');
            object currentValue = instance;
            Type currentType = instance.GetType();
            PropertyInfo currentProperty = null;

            foreach (string property in propPath)
            {
                currentProperty = TypeFactory.GetTypeInfo(currentType).GetProperty(property);
                currentValue = currentProperty.GetValue(currentValue, null);
                currentType = currentProperty.PropertyType;
            }
            return (T)currentValue;
        }
        internal static Type GetPropertyTypeFromPath(Type start, string path)
        {
            String[] propPath = path.Split('.');
            Type currentType = start;
            PropertyInfo currentProperty = null;
            foreach (string property in propPath)
            {
                currentProperty = TypeFactory.GetTypeInfo(currentType).GetProperty(property);
                currentType = currentProperty.PropertyType;
            }
            return currentType;
        }

        private static Type GetFuncType<T, U>()
        {
            return typeof(Func<T, U>);
        }
        internal static T Cast<T>(object o)
        {
            return (T)o;
        }
    }

    internal class PaginatedResourceInfo
    {
        private string tokenRequestPropertyPath;
        private string tokenResponsePropertyPath;

        internal object Client
        {
            get;
            set;
        }
        internal string MethodName
        {
            get;
            set;
        }
        internal object Request
        {
            get;
            set;
        }
        internal string TokenRequestPropertyPath
        {
            get
            {
                string ret = tokenRequestPropertyPath;
                if (String.IsNullOrEmpty(ret))
                {
                    ret = "NextToken";
                }
                return ret;
            }
            set { tokenRequestPropertyPath = value; }
        }
        internal string TokenResponsePropertyPath
        {
            get
            {
                string ret = tokenResponsePropertyPath;
                if (String.IsNullOrEmpty(ret))
                {
                    ret = "{0}";
                    if (Client != null && !String.IsNullOrEmpty(MethodName))
                    {
                        MethodInfo mi = TypeFactory.GetTypeInfo(Client.GetType()).GetMethod(MethodName);
                        if (mi != null)
                        {
                            Type responseType = mi.ReturnType;
                            string baseName = responseType.Name;
                            if (baseName.EndsWith("Response", StringComparison.Ordinal))
                            {
                                baseName = baseName.Substring(0, baseName.Length - 8);
                            }
                            if (TypeFactory.GetTypeInfo(responseType).GetProperty(string.Format(CultureInfo.InvariantCulture, "{0}Result", baseName)) != null)
                            {
                                ret = string.Format(CultureInfo.InvariantCulture, ret, string.Format(CultureInfo.InvariantCulture, "{0}Result.{1}", baseName, "{0}"));
                            }
                        }
                    }
                    ret = string.Format(CultureInfo.InvariantCulture, ret, "NextToken");
                }
                return ret;
            }
            set { tokenResponsePropertyPath = value; }
        }
        internal string ItemListPropertyPath
        {
            get;
            set;
        }

        internal PaginatedResourceInfo WithClient(object client)
        {
            Client = client;
            return this;
        }

        internal PaginatedResourceInfo WithMethodName(string methodName)
        {
            MethodName = methodName;
            return this;
        }

        internal PaginatedResourceInfo WithRequest(object request)
        {
            Request = request;
            return this;
        }

        internal PaginatedResourceInfo WithTokenRequestPropertyPath(string tokenRequestPropertyPath)
        {
            TokenRequestPropertyPath = tokenRequestPropertyPath;
            return this;
        }

        internal PaginatedResourceInfo WithTokenResponsePropertyPath(string tokenResponsePropertyPath)
        {
            TokenResponsePropertyPath = tokenResponsePropertyPath;
            return this;
        }

        internal PaginatedResourceInfo WithItemListPropertyPath(string itemListPropertyPath)
        {
            ItemListPropertyPath = itemListPropertyPath;
            return this;
        }

        internal void Verify()
        {
            //Client is set
            if (Client == null)
            {
                throw new ArgumentException("PaginatedResourceInfo.Client needs to be set.");
            }

            //MethodName exists on Client and takes one arguement.
            Type clientType = Client.GetType();
            MethodInfo mi = TypeFactory.GetTypeInfo(clientType).GetMethod(MethodName);
            if (mi == null)
            {
                throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "{0} has no method called {1}", clientType.Name, MethodName));
            }
            if (mi.GetParameters().Length != 1)
            {
                throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "{1} on {0} has incompatable signiture", clientType.Name, MethodName));
            }

            //Request is valid type.
            Type requestType = mi.GetParameters()[0].ParameterType;
            try
            {
                Convert.ChangeType(Request, requestType, CultureInfo.InvariantCulture);
            }
            catch (Exception)
            {
                throw new ArgumentException("PaginatedResourcInfo.Request is an incompatible type.");
            }

            //Properties exist 
            Type responseType = mi.ReturnType;
            VerifyProperty("TokenRequestPropertyPath", requestType, TokenRequestPropertyPath, typeof(string));
            VerifyProperty("TokenResponsePropertyPath", responseType, TokenResponsePropertyPath, typeof(string));
            VerifyProperty("ItemListPropertyPath", responseType, ItemListPropertyPath, typeof(string), true);
        }
        private static void VerifyProperty(string propName, Type start, string path, Type expectedType)
        {
            VerifyProperty(propName, start, path, expectedType, false);
        }
        private static void VerifyProperty(string propName, Type start, string path, Type expectedType, bool skipTypecheck)
        {
            Type type = null;
            if (String.IsNullOrEmpty(path))
            {
                throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "{0} must contain a value.", propName));
            }
            try
            {
                type = PaginatedResourceFactory.GetPropertyTypeFromPath(start, path);
            }
            catch (Exception)
            {
                throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "{0} does not exist on {1}", path, start.Name));
            }
            if (!skipTypecheck && type != expectedType)
            {
                throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "{0} on {1} is not of type {2}", path, start.Name, expectedType.Name));
            }
        }
    }
}
