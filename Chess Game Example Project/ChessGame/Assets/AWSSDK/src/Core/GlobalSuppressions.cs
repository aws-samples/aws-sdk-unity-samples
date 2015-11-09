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
using System.Diagnostics.CodeAnalysis;

// Suppressions for identifiers in base classes
[module: SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", Scope = "member", Target = "Amazon.Runtime.Internal.Util.ILogger.#Error(System.Exception,System.String,System.Object[])", MessageId = "Error")]

// Identifiers should have correct prefix
[module: SuppressMessage("Microsoft.Naming", "CA1715:IdentifiersShouldHaveCorrectPrefix", Scope = "member", Target = "Amazon.Runtime.AmazonWebServiceClient.#Invoke`2(!!1,System.AsyncCallback,System.Object,System.Boolean,Amazon.Runtime.Internal.Transform.IMarshaller`2<!!0,!!1>,Amazon.Runtime.Internal.Transform.ResponseUnmarshaller,Amazon.Runtime.Internal.Auth.AbstractAWSSigner)", MessageId = "T")]
[module: SuppressMessage("Microsoft.Naming", "CA1715:IdentifiersShouldHaveCorrectPrefix", Scope = "type", Target = "Amazon.Runtime.Internal.Transform.IResponseUnmarshaller`2", MessageId = "T")]
[module: SuppressMessage("Microsoft.Naming", "CA1715:IdentifiersShouldHaveCorrectPrefix", Scope = "type", Target = "Amazon.Runtime.Internal.Transform.IUnmarshaller`2", MessageId = "T")]
[module: SuppressMessage("Microsoft.Naming", "CA1715:IdentifiersShouldHaveCorrectPrefix", Scope = "type", Target = "Amazon.Runtime.Internal.Transform.DictionaryUnmarshaller`4", MessageId = "T")]
[module: SuppressMessage("Microsoft.Naming", "CA1715:IdentifiersShouldHaveCorrectPrefix", Scope = "type", Target = "Amazon.Runtime.Internal.Transform.IMarshaller`2", MessageId = "T")]
[module: SuppressMessage("Microsoft.Naming", "CA1715:IdentifiersShouldHaveCorrectPrefix", Scope = "type", Target = "Amazon.Runtime.Internal.Transform.ListUnmarshaller`2", MessageId = "T")]
[module: SuppressMessage("Microsoft.Naming", "CA1715:IdentifiersShouldHaveCorrectPrefix", Scope = "type", Target = "Amazon.Runtime.Internal.Transform.KeyValueUnmarshaller`4", MessageId = "T")]
[module: SuppressMessage("Microsoft.Naming", "CA1715:IdentifiersShouldHaveCorrectPrefix", Scope = "type", Target = "Amazon.Runtime.Internal.Transform.IRequestMarshaller`2", MessageId = "T")]
[module: SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix", Scope = "type", Target = "Amazon.Runtime.Internal.AutoConstructedList`1")]

// Identifiers should not contain type names
[module: SuppressMessage("Microsoft.Naming", "CA1720:IdentifiersShouldNotContainTypeNames", Scope = "member", Target = "Amazon.Runtime.Internal.Auth.AWS4PreSignedUrlSigner.#MaxAWS4PreSignedUrlExpiry", MessageId = "signed")]
[module: SuppressMessage("Microsoft.Naming", "CA1720:IdentifiersShouldNotContainTypeNames", Scope = "member", Target = "Amazon.Runtime.Internal.Auth.AWS4Signer.#FormatDateTime(System.DateTime,System.String)", MessageId = "string")]

// Array properties/fields
[module: SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays", Scope = "member", Target = "Amazon.Runtime.Internal.Util.HashStream.#CalculatedHash")]
[module: SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays", Scope = "member", Target = "Amazon.Runtime.Internal.Util.HashStream.#ExpectedHash")]
[module: SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays", Scope = "member", Target = "Amazon.Auth.AccessControlPolicy.Condition.#Values")]
[module: SuppressMessage("Microsoft.Security", "CA2105:ArrayFieldsShouldNotBeReadOnly", Scope = "member", Target = "Amazon.Runtime.Internal.Auth.AWS4Signer.#TerminatorBytes")]
[module: SuppressMessage("Microsoft.Security", "CA2105:ArrayFieldsShouldNotBeReadOnly", Scope = "member", Target = "Amazon.Util.Internal.TypeFactory.#EmptyTypes")]
[module: SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays", Scope = "member", Target = "Amazon.Runtime.Internal.IRequest.#Content")]
[module: SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays", Scope = "member", Target = "Amazon.Runtime.Internal.Util.LogMessage.#Args")]
[module: SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays", Scope = "member", Target = "Amazon.Runtime.Internal.Auth.AWS4SigningResult.#SignatureBytes")]
[module: SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays", Scope = "member", Target = "Amazon.Runtime.Internal.Auth.AWS4SigningResult.#SigningKey")]
[module: SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays", Scope = "member", Target = "Amazon.Runtime.Internal.Util.EncryptUploadPartStream.#InitializationVector")]

// Nested generic types
[module: SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Scope = "member", Target = "Amazon.Runtime.IRequestMetrics.#Properties")]
[module: SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Scope = "member", Target = "Amazon.Runtime.IRequestMetrics.#Timings")]
[module: SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Scope = "member", Target = "Amazon.Runtime.Internal.Settings.SettingsCollection.#.ctor(System.Collections.Generic.Dictionary`2<System.String,System.Collections.Generic.Dictionary`2<System.String,System.Object>>)")]

// Suppressions for exceptions in setters
[module: SuppressMessage("Microsoft.Usage", "CA2219:DoNotRaiseExceptionsInExceptionClauses", Scope = "member", Target = "Amazon.Runtime.Internal.HttpRequest.#WriteToRequestBody(System.IO.Stream,System.IO.Stream,System.Collections.Generic.IDictionary`2<System.String,System.String>,Amazon.Runtime.IRequestContext)")]

// Serialization-only classes
[module: SuppressMessage("Microsoft.Performance", "CA1812:AvoidUninstantiatedInternalClasses", Scope = "type", Target = "Amazon.Runtime.InstanceProfileAWSCredentials+SecurityCredentials")]
[module: SuppressMessage("Microsoft.Performance", "CA1812:AvoidUninstantiatedInternalClasses", Scope = "type", Target = "Amazon.Runtime.InstanceProfileAWSCredentials+SecurityInfo")]

// Passing base types
[module: SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters", Scope = "member", Target = "Amazon.S3.Model.S3AccessControlList.#RemoveGrant(Amazon.S3.Model.S3Grantee,Amazon.S3.S3Permission)")]
[module: SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters", Scope = "member", Target = "Amazon.S3.Model.S3BucketLoggingConfig.#RemoveGrant(Amazon.S3.Model.S3Grantee,Amazon.S3.S3Permission)")]
[module: SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters", Scope = "member", Target = "Amazon.Auth.AccessControlPolicy.ConditionFactory.#NewCannedACLCondition(Amazon.S3.S3CannedACL)")]

// Casting
[module: SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily", Scope = "member", Target = "Amazon.Runtime.Internal.Util.WrapperStream.#SearchWrappedStream(System.Func`2<System.IO.Stream,System.Boolean>)")]
[module: SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily", Scope = "member", Target = "Amazon.Runtime.Internal.Transform.JsonUnmarshallerContext.#ReadText()")]

// Identifier suffix
[module: SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix", Scope = "type", Target = "Amazon.Runtime.StreamTransferProgressArgs")]

// Interface methods not callable by child types
[module: SuppressMessage("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes", Scope = "member", Target = "Amazon.Runtime.AmazonWebServiceRequest.#Amazon.Runtime.Internal.IRequestEvents.FireBeforeRequestEvent(System.Object,Amazon.Runtime.RequestEventArgs)")]
[module: SuppressMessage("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes", Scope = "member", Target = "Amazon.Runtime.AmazonWebServiceRequest.#Amazon.Runtime.Internal.IRequestEvents.AddBeforeRequestHandler(Amazon.Runtime.RequestEventHandler)")]
[module: SuppressMessage("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes", Scope = "member", Target = "Amazon.Runtime.AmazonWebServiceRequest.#Amazon.Runtime.Internal.IAmazonWebServiceRequest.RemoveBeforeRequestHandler(Amazon.Runtime.RequestEventHandler)")]
[module: SuppressMessage("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes", Scope = "member", Target = "Amazon.Runtime.AmazonWebServiceRequest.#Amazon.Runtime.Internal.IAmazonWebServiceRequest.get_RequestState()")]
[module: SuppressMessage("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes", Scope = "member", Target = "Amazon.Runtime.AmazonWebServiceRequest.#Amazon.Runtime.Internal.IAmazonWebServiceRequest.set_StreamUploadProgressCallback(System.EventHandler`1<Amazon.Runtime.StreamTransferProgressArgs>)")]
[module: SuppressMessage("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes", Scope = "member", Target = "Amazon.Runtime.AmazonWebServiceRequest.#Amazon.Runtime.Internal.IAmazonWebServiceRequest.get_StreamUploadProgressCallback()")]
[module: SuppressMessage("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes", Scope = "member", Target = "Amazon.Runtime.AmazonWebServiceRequest.#Amazon.Runtime.Internal.IAmazonWebServiceRequest.get_UseSigV4()")]
[module: SuppressMessage("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes", Scope = "member", Target = "Amazon.Runtime.AmazonWebServiceRequest.#Amazon.Runtime.Internal.IAmazonWebServiceRequest.set_UseSigV4(System.Boolean)")]
[module: SuppressMessage("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes", Scope = "member", Target = "Amazon.Runtime.AmazonWebServiceRequest.#Amazon.Runtime.Internal.IAmazonWebServiceRequest.AddBeforeRequestHandler(Amazon.Runtime.RequestEventHandler)")]

// General exception types
[module: SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Scope = "member", Target = "Amazon.Runtime.Internal.Util.InternalLog4netLogger.#loadStatics()")]
[module: SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Scope = "member", Target = "Amazon.Runtime.Internal.Util.BackgroundDispatcher`1.#HandleInvoked()")]
[module: SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Scope = "member", Target = "Amazon.Runtime.FallbackCredentialsFactory.#GetCredentials(System.Boolean)")]
[module: SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Scope = "member", Target = "Amazon.Runtime.Internal.Util.MetricError.#.ctor(Amazon.Runtime.Internal.Util.Metric,System.Exception,System.String,System.Object[])")]
[module: SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Scope = "member", Target = "Amazon.Runtime.AmazonWebServiceClient.#getRequestStreamCallback(System.IAsyncResult)")]
[module: SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Scope = "member", Target = "Amazon.Runtime.AmazonWebServiceClient.#getResponseCallback(System.IAsyncResult)")]
[module: SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Scope = "member", Target = "Amazon.Runtime.InstanceProfileAWSCredentials.#GenerateNewCredentials()")]
[module: SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Scope = "member", Target = "Amazon.Util.CryptoUtilFactory+CryptoUtil.#get_SHA256HashAlgorithmInstance()")]
[module: SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Scope = "member", Target = "Amazon.Util.AWSSDKUtils.#PreserveStackTrace(System.Exception)")]
[module: SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Scope = "member", Target = "Amazon.Util.AWSSDKUtils.#ForceCanonicalPathAndQuery(System.Uri)")]
[module: SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Scope = "member", Target = "Amazon.Util.AWSSDKUtils.#DetermineFramework()")]
[module: SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Scope = "member", Target = "Amazon.Runtime.Internal.Util.EventStream.#BeginRead(System.Byte[],System.Int32,System.Int32,System.AsyncCallback,System.Object)")]
[module: SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Scope = "member", Target = "Amazon.AWSSection.#Serialize(System.String)")]
[module: SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Scope = "member", Target = "Amazon.Runtime.AbstractWebServiceClient.#DontUnescapePathDotsAndSlashes(System.Uri)")]
[module: SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Scope = "member", Target = "Amazon.RegionEndpoint.#TryLoadEndpointDefinitionsFromAssemblyDir()")]
[module: SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Scope = "member", Target = "Amazon.Runtime.Internal.HttpHandler`1.#DontUnescapePathDotsAndSlashes(System.Uri)")]
[module: SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Scope = "member", Target = "Amazon.Runtime.Internal.HttpHandler`1.#GetResponseCallback(System.IAsyncResult)")]
[module: SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Scope = "member", Target = "Amazon.Runtime.Internal.HttpHandler`1.#GetRequestStreamCallback(System.IAsyncResult)")]
[module: SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Scope = "member", Target = "Amazon.Runtime.Internal.PipelineHandler.#AsyncCallback(Amazon.Runtime.IAsyncExecutionContext)")]
[module: SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Scope = "member", Target = "Amazon.Runtime.Internal.HttpErrorResponseExceptionHandler.#HandleSuppressed404(Amazon.Runtime.IExecutionContext,Amazon.Runtime.Internal.Transform.IWebResponseData)")]
[module: SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Scope = "member", Target = "Amazon.Runtime.Internal.ErrorHandler.#InvokeAsyncCallback(Amazon.Runtime.IAsyncExecutionContext)")]
[module: SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Scope = "member", Target = "Amazon.Runtime.AmazonServiceClient.#DontUnescapePathDotsAndSlashes(System.Uri)")]
[module: SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Scope = "member", Target = "Amazon.Runtime.Internal.Util.MetricError.#.ctor(Amazon.Runtime.Metric,System.Exception,System.String,System.Object[])")]
[module: SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Scope = "member", Target = "Amazon.Runtime.Internal.Util.RequestMetrics.#ToString()")]
[module: SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Scope = "member", Target = "Amazon.Runtime.Internal.HttpHandler`1.#CompleteFailedRequest(Amazon.Runtime.IHttpRequest`1<!0>)")]
[module: SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Scope = "member", Target = "Amazon.Runtime.Internal.Settings.PersistenceManager.#loadSettingsType(System.String)")]
[module: SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Scope = "member", Target = "Amazon.Runtime.Internal.Settings.PersistenceManager.#decryptAnyEncryptedValues(System.Collections.Generic.Dictionary`2<System.String,System.Collections.Generic.Dictionary`2<System.String,System.Object>>)")]
[module: SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Scope = "member", Target = "Amazon.Util.Internal.InternalSDKUtils.#DetermineFramework()")]

// Overflow operations
[module: SuppressMessage("Microsoft.Usage", "CA2233:OperationsShouldNotOverflow", Scope = "member", Target = "Amazon.Runtime.Internal.Util.EncryptStream.#Read(System.Byte[],System.Int32,System.Int32)", MessageId = "offset+16")]
[module: SuppressMessage("Microsoft.Usage", "CA2233:OperationsShouldNotOverflow", Scope = "member", Target = "Amazon.Runtime.Internal.Util.EncryptUploadPartStream.#Read(System.Byte[],System.Int32,System.Int32)", MessageId = "offset+16")]

// Nested types, breaking change
[module: SuppressMessage("Microsoft.Design", "CA1034:NestedTypesShouldNotBeVisible", Scope = "type", Target = "Amazon.Auth.AccessControlPolicy.ConditionFactory+ArnComparisonType")]
[module: SuppressMessage("Microsoft.Design", "CA1034:NestedTypesShouldNotBeVisible", Scope = "type", Target = "Amazon.Auth.AccessControlPolicy.ConditionFactory+DateComparisonType")]
[module: SuppressMessage("Microsoft.Design", "CA1034:NestedTypesShouldNotBeVisible", Scope = "type", Target = "Amazon.Auth.AccessControlPolicy.ConditionFactory+IpAddressComparisonType")]
[module: SuppressMessage("Microsoft.Design", "CA1034:NestedTypesShouldNotBeVisible", Scope = "type", Target = "Amazon.Auth.AccessControlPolicy.ConditionFactory+NumericComparisonType")]
[module: SuppressMessage("Microsoft.Design", "CA1034:NestedTypesShouldNotBeVisible", Scope = "type", Target = "Amazon.Auth.AccessControlPolicy.ConditionFactory+StringComparisonType")]
[module: SuppressMessage("Microsoft.Design", "CA1034:NestedTypesShouldNotBeVisible", Scope = "type", Target = "Amazon.Auth.AccessControlPolicy.Statement+StatementEffect")]
[module: SuppressMessage("Microsoft.Design", "CA1034:NestedTypesShouldNotBeVisible", Scope = "type", Target = "Amazon.RegionEndpoint+Endpoint")]
[module: SuppressMessage("Microsoft.Design", "CA1034:NestedTypesShouldNotBeVisible", Scope = "type", Target = "Amazon.Runtime.Internal.AsyncResult+AsyncRequestState")]
[module: SuppressMessage("Microsoft.Design", "CA1034:NestedTypesShouldNotBeVisible", Scope = "type", Target = "")]
[module: SuppressMessage("Microsoft.Design", "CA1034:NestedTypesShouldNotBeVisible", Scope = "type", Target = "")]
[module: SuppressMessage("Microsoft.Design", "CA1034:NestedTypesShouldNotBeVisible", Scope = "type", Target = "")]
[module: SuppressMessage("Microsoft.Design", "CA1034:NestedTypesShouldNotBeVisible", Scope = "type", Target = "Amazon.Runtime.Internal.Settings.SettingsCollection+ObjectSettings")]
[module: SuppressMessage("Microsoft.Design", "CA1034:NestedTypesShouldNotBeVisible", Scope = "type", Target = "Amazon.Runtime.Internal.Settings.SettingsCollection+ObjectSettings")]
[module: SuppressMessage("Microsoft.Design", "CA1034:NestedTypesShouldNotBeVisible", Scope = "type", Target = "Amazon.Runtime.FallbackCredentialsFactory+CredentialsGenerator")]
[module: SuppressMessage("Microsoft.Design", "CA1034:NestedTypesShouldNotBeVisible", Scope = "type", Target = "Amazon.Runtime.RefreshingAWSCredentials+CredentialsRefreshState")]

// Normalize strings to uppercase, breaking change
[module: SuppressMessage("Microsoft.Globalization", "CA1308:NormalizeStringsToUppercase", Scope = "member", Target = "Amazon.Auth.AccessControlPolicy.ConditionFactory.#NewCondition(System.String,System.Boolean)")]
[module: SuppressMessage("Microsoft.Globalization", "CA1308:NormalizeStringsToUppercase", Scope = "member", Target = "Amazon.RegionEndpoint.#GetBySystemName(System.String)")]
[module: SuppressMessage("Microsoft.Globalization", "CA1308:NormalizeStringsToUppercase", Scope = "member", Target = "Amazon.Runtime.Internal.Auth.AWS3Signer.#GetCanonicalizedHeadersForStringToSign(Amazon.Runtime.Internal.IRequest)")]
[module: SuppressMessage("Microsoft.Globalization", "CA1308:NormalizeStringsToUppercase", Scope = "member", Target = "Amazon.Runtime.Internal.Auth.AWS3Signer.#GetHeadersForStringToSign(Amazon.Runtime.Internal.IRequest)")]
[module: SuppressMessage("Microsoft.Globalization", "CA1308:NormalizeStringsToUppercase", Scope = "member", Target = "Amazon.Runtime.Internal.Auth.AWS4Signer.#CalculateSignature(System.Collections.Generic.IDictionary`2<System.String,System.String>,System.Collections.Generic.IDictionary`2<System.String,System.String>,System.String,System.String,System.String,System.String,Amazon.Runtime.ImmutableCredentials)")]
[module: SuppressMessage("Microsoft.Globalization", "CA1308:NormalizeStringsToUppercase", Scope = "member", Target = "Amazon.Runtime.Internal.Auth.AWS4Signer.#DetermineRegion(Amazon.Runtime.ClientConfig)")]
[module: SuppressMessage("Microsoft.Globalization", "CA1308:NormalizeStringsToUppercase", Scope = "member", Target = "Amazon.Runtime.Internal.Auth.AWS4Signer.#DetermineService(Amazon.Runtime.ClientConfig)")]
[module: SuppressMessage("Microsoft.Globalization", "CA1308:NormalizeStringsToUppercase", Scope = "member", Target = "Amazon.Runtime.Internal.Auth.AWS4Signer.#GetCanonicalizedHeaders(System.Collections.Generic.List`1<System.String>,System.Collections.Generic.IDictionary`2<System.String,System.String>)")]
[module: SuppressMessage("Microsoft.Globalization", "CA1308:NormalizeStringsToUppercase", Scope = "member", Target = "Amazon.Runtime.Internal.Auth.AWS4Signer.#GetSignedHeaders(System.Collections.Generic.List`1<System.String>)")]
[module: SuppressMessage("Microsoft.Globalization", "CA1308:NormalizeStringsToUppercase", Scope = "member", Target = "Amazon.Runtime.Internal.Auth.S3Signer.#buildCanonicalizedHeaders(System.Collections.Generic.IDictionary`2<System.String,System.String>)")]
[module: SuppressMessage("Microsoft.Globalization", "CA1308:NormalizeStringsToUppercase", Scope = "member", Target = "Amazon.Runtime.Internal.Util.StringUtils.#FromBool(System.Boolean)")]
[module: SuppressMessage("Microsoft.Globalization", "CA1308:NormalizeStringsToUppercase", Scope = "member", Target = "Amazon.Runtime.Internal.Auth.AWS4Signer.#CanonicalizeHeaderNames(System.Collections.Generic.IEnumerable`1<System.Collections.Generic.KeyValuePair`2<System.String,System.String>>)")]
[module: SuppressMessage("Microsoft.Globalization", "CA1308:NormalizeStringsToUppercase", Scope = "member", Target = "Amazon.Runtime.Internal.Auth.AWS4Signer.#CanonicalizeHeaders(System.Collections.Generic.ICollection`1<System.Collections.Generic.KeyValuePair`2<System.String,System.String>>)")]
[module: SuppressMessage("Microsoft.Globalization", "CA1308:NormalizeStringsToUppercase", Scope = "member", Target = "Amazon.Runtime.Internal.Auth.S3Signer.#BuildCanonicalizedHeaders(System.Collections.Generic.IDictionary`2<System.String,System.String>)")]
[module: SuppressMessage("Microsoft.Globalization", "CA1308:NormalizeStringsToUppercase", Scope = "member", Target = "Amazon.Runtime.Internal.Auth.AWS4Signer.#DetermineSigningRegion(Amazon.Runtime.ClientConfig,System.String,Amazon.RegionEndpoint,Amazon.Runtime.Internal.IRequest)")]
[module: SuppressMessage("Microsoft.Globalization", "CA1308:NormalizeStringsToUppercase", Scope = "member", Target = "Amazon.Runtime.Internal.Auth.AWS4Signer.#CanonicalizeHeaders(System.Collections.Generic.IDictionary`2<System.String,System.String>)")]
[module: SuppressMessage("Microsoft.Globalization", "CA1308:NormalizeStringsToUppercase", Scope = "member", Target = "Amazon.Runtime.Internal.Auth.AWS4Signer.#CanonicalizeHeaderNames(System.Collections.Generic.IDictionary`2<System.String,System.String>)")]

// Types names matching namespaces
[module: SuppressMessage("Microsoft.Naming", "CA1724:TypeNamesShouldNotMatchNamespaces", Scope = "type", Target = "Amazon.Auth.AccessControlPolicy.Policy")]
[module: SuppressMessage("Microsoft.Naming", "CA1724:TypeNamesShouldNotMatchNamespaces", Scope = "type", Target = "Amazon.Auth.AccessControlPolicy.Principal")]

// Uri should not be string
[module: SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings", Scope = "member", Target = "Amazon.Util.AWSSDKUtils.#DetermineRegion(System.String)", MessageId = "0#")]
[module: SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings", Scope = "member", Target = "Amazon.Util.AWSSDKUtils.#DetermineService(System.String)", MessageId = "0#")]

[module: SuppressMessage("Microsoft.Design", "CA1056:UriPropertiesShouldNotBeStrings", Scope = "member", Target = "Amazon.Runtime.ClientConfig.#ServiceURL")]

[module: SuppressMessage("Microsoft.Design", "CA1055:UriReturnValuesShouldNotBeStrings", Scope = "member", Target = "Amazon.Util.AWSSDKUtils.#UrlEncode(System.String,System.Boolean)")]

// Use properties, breaking changes
[module: SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Scope = "member", Target = "Amazon.Runtime.AWSCredentials.#GetCredentials()")]
[module: SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Scope = "member", Target = "Amazon.Runtime.InstanceProfileAWSCredentials.#GetAvailableRoles()")]
[module: SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Scope = "member", Target = "Amazon.Runtime.Internal.RequestMetrics.#GetErrors()")]
[module: SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Scope = "member", Target = "Amazon.Runtime.AWSCredentials.#GetCredentialsAsync()")]
[module: SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Scope = "member", Target = "Amazon.Runtime.Internal.Settings.PersistenceManager.#GetSettingsStoreFolder()")]
[module: SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Scope = "member", Target = "Amazon.Runtime.Internal.Settings.SettingsWatcher.#GetSettings()")]
[module: SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Scope = "member", Target = "Amazon.Runtime.Internal.Util.RequestMetrics.#GetErrors()")]
[module: SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Scope = "member", Target = "Amazon.Runtime.IHttpRequest`1.#GetRequestContentAsync()")]

// Use properties, huge change
[module: SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Scope = "member", Target = "Amazon.Runtime.Internal.Transform.ResponseMetadataUnmarshaller.#GetInstance()")]
[module: SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Scope = "member", Target = "Amazon.Runtime.Internal.Transform.MemoryStreamUnmarshaller.#GetInstance()")]
[module: SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Scope = "member", Target = "Amazon.Runtime.Internal.Transform.StringUnmarshaller.#GetInstance()")]
[module: SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Scope = "member", Target = "Amazon.Runtime.Internal.Transform.LongUnmarshaller.#GetInstance()")]
[module: SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Scope = "member", Target = "Amazon.Runtime.Internal.Transform.IntUnmarshaller.#GetInstance()")]
[module: SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Scope = "member", Target = "Amazon.Runtime.Internal.Transform.ErrorResponseUnmarshaller.#GetInstance()")]
[module: SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Scope = "member", Target = "Amazon.Runtime.Internal.Transform.FloatUnmarshaller.#GetInstance()")]
[module: SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Scope = "member", Target = "Amazon.Runtime.Internal.Transform.JsonErrorResponseUnmarshaller.#GetInstance()")]
[module: SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Scope = "member", Target = "Amazon.Runtime.Internal.Transform.DateTimeUnmarshaller.#GetInstance()")]
[module: SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Scope = "member", Target = "Amazon.Runtime.Internal.Transform.ByteUnmarshaller.#GetInstance()")]
[module: SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Scope = "member", Target = "Amazon.Runtime.Internal.Transform.DoubleUnmarshaller.#GetInstance()")]
[module: SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Scope = "member", Target = "Amazon.Runtime.Internal.Transform.BoolUnmarshaller.#GetInstance()")]
[module: SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Scope = "member", Target = "Amazon.Runtime.Internal.Transform.DateTimeEpochLongMillisecondsUnmarshaller.#GetInstance()")]
[module: SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Scope = "member", Target = "Amazon.Util.Internal.ITypeInfo.#GetElementType()")]
[module: SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Scope = "member", Target = "Amazon.Util.Internal.ITypeInfo.#GetGenericTypeDefinition()")]
[module: SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Scope = "member", Target = "Amazon.Util.Internal.ITypeInfo.#GetFields()")]
[module: SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Scope = "member", Target = "Amazon.Util.Internal.ITypeInfo.#GetProperties()")]

// Use properties, design decision to suppress
[module: SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Scope = "member", Target = "Amazon.Runtime.Internal.Util.WrapperStream.#GetSeekableBaseStream()")]

// Unused parameters
[module: SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", Scope = "member", Target = "Amazon.Runtime.AmazonWebServiceClient.#LogResponse(Amazon.Runtime.Internal.Util.RequestMetrics,Amazon.Runtime.Internal.IRequest,System.Net.HttpStatusCode)", MessageId = "request")]
[module: SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", Scope = "member", Target = "Amazon.Runtime.ConstantClass.#ToString(System.IFormatProvider)", MessageId = "provider")]

// Mark members as static, breaking changes
[module: SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Scope = "member", Target = "Amazon.Runtime.Internal.Auth.AWS4Signer.#SignRequest(Amazon.Runtime.Internal.IRequest,Amazon.Runtime.ClientConfig,Amazon.Runtime.Internal.Util.RequestMetrics,System.String,System.String)")]
[module: SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Scope = "member", Target = "Amazon.Runtime.Internal.Auth.AWS4PreSignedUrlSigner.#SignRequest(Amazon.Runtime.Internal.IRequest,Amazon.Runtime.ClientConfig,Amazon.Runtime.Internal.Util.RequestMetrics,System.String,System.String)")]
[module: SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Scope = "member", Target = "Amazon.Runtime.Internal.Auth.S3Signer.#SignRequest(Amazon.Runtime.Internal.IRequest,Amazon.Runtime.ClientConfig,Amazon.Runtime.Internal.Util.RequestMetrics,System.String,System.String)")]
[module: SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Scope = "member", Target = "Amazon.Runtime.Internal.Auth.S3Signer.#SignRequest(Amazon.Runtime.Internal.IRequest,Amazon.Runtime.Internal.Util.RequestMetrics,System.String,System.String)")]

// Link demand
[module: SuppressMessage("Microsoft.Security", "CA2122:DoNotIndirectlyExposeMethodsWithLinkDemands", Scope = "member", Target = "Amazon.Runtime.Internal.Util.TraceSourceUtil.#GetTraceSourceWithListeners(System.String,System.Diagnostics.SourceLevels)")]
[module: SuppressMessage("Microsoft.Security", "CA2122:DoNotIndirectlyExposeMethodsWithLinkDemands", Scope = "member", Target = "Amazon.Runtime.Internal.Settings.UserCrypto.#Encrypt(System.String)")]
[module: SuppressMessage("Microsoft.Security", "CA2122:DoNotIndirectlyExposeMethodsWithLinkDemands", Scope = "member", Target = "Amazon.Runtime.Internal.Settings.UserCrypto.#Decrypt(System.String)")]
[module: SuppressMessage("Microsoft.Security", "CA2122:DoNotIndirectlyExposeMethodsWithLinkDemands", Scope = "member", Target = "Amazon.Runtime.Internal.Settings.UserCrypto.#ConvertData(System.Byte[])")]
[module: SuppressMessage("Microsoft.Security", "CA2122:DoNotIndirectlyExposeMethodsWithLinkDemands", Scope = "member", Target = "Amazon.Runtime.Internal.Settings.SettingsWatcher.#set_Enable(System.Boolean)")]
[module: SuppressMessage("Microsoft.Security", "CA2122:DoNotIndirectlyExposeMethodsWithLinkDemands", Scope = "member", Target = "Amazon.Runtime.Internal.Settings.SettingsWatcher.#get_Enable()")]
[module: SuppressMessage("Microsoft.Security", "CA2122:DoNotIndirectlyExposeMethodsWithLinkDemands", Scope = "member", Target = "Amazon.Runtime.Internal.Settings.SettingsWatcher.#.ctor(System.String,System.String)")]

// Initialize reference type static fields inline
[module: SuppressMessage("Microsoft.Performance", "CA1810:InitializeReferenceTypeStaticFieldsInline", Scope = "member", Target = "Amazon.Util.AWSSDKUtils.#.cctor()")]

// Abstract types should not have constructors
[module: SuppressMessage("Microsoft.Design", "CA1012:AbstractTypesShouldNotHaveConstructors", Scope = "type", Target = "Amazon.Runtime.ClientConfig")]

// Avoid uncalled private code, properties used in serialization
[module: SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Scope = "member", Target = "Amazon.Runtime.InstanceProfileAWSCredentials+SecurityBase.#set_Code(System.String)")]
[module: SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Scope = "member", Target = "Amazon.Runtime.InstanceProfileAWSCredentials+SecurityBase.#get_LastUpdated()")]
[module: SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Scope = "member", Target = "Amazon.Runtime.InstanceProfileAWSCredentials+SecurityBase.#set_LastUpdated(System.DateTime)")]
[module: SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Scope = "member", Target = "Amazon.Runtime.InstanceProfileAWSCredentials+SecurityBase.#set_Message(System.String)")]

// Do not call overridable methods in constructors
[module: SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors", Scope = "member", Target = "Amazon.Runtime.AbstractWebServiceClient.#.ctor(Amazon.Runtime.AWSCredentials,Amazon.Runtime.ClientConfig,Amazon.Runtime.AbstractWebServiceClient+AuthenticationTypes)")]
[module: SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors", Scope = "member", Target = "Amazon.Runtime.ClientConfig.#.ctor()")]
[module: SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors", Scope = "member", Target = "Amazon.Runtime.AmazonServiceClient.#.ctor(Amazon.Runtime.AWSCredentials,Amazon.Runtime.ClientConfig)")]
[module: SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors", Scope = "member", Target = "Amazon.Runtime.Internal.ErrorHandler.#.ctor(Amazon.Runtime.Internal.Util.ILogger)")]
[module: SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors", Scope = "member", Target = "ThirdParty.MD5.MD5Managed.#.ctor()")]
[module: SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors", Scope = "member", Target = "Amazon.V4ClientSection.#.ctor(System.Xml.Linq.XElement)")]
[module: SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors", Scope = "member", Target = "Amazon.V4ClientSectionRoot.#.ctor(System.Xml.Linq.XElement)")]

// Events should have before or after prefix
[module: SuppressMessage("Microsoft.Naming", "CA1713:EventsShouldNotHaveBeforeOrAfterPrefix", Scope = "member", Target = "Amazon.Runtime.AbstractWebServiceClient.#BeforeRequestEvent")]
[module: SuppressMessage("Microsoft.Naming", "CA1713:EventsShouldNotHaveBeforeOrAfterPrefix", Scope = "member", Target = "Amazon.Runtime.AbstractWebServiceClient.#AfterResponseEvent")]
[module: SuppressMessage("Microsoft.Naming", "CA1713:EventsShouldNotHaveBeforeOrAfterPrefix", Scope = "member", Target = "Amazon.Runtime.AmazonServiceClient.#BeforeRequestEvent")]
[module: SuppressMessage("Microsoft.Naming", "CA1713:EventsShouldNotHaveBeforeOrAfterPrefix", Scope = "member", Target = "Amazon.Runtime.AmazonServiceClient.#AfterResponseEvent")]

// ISerializable attribute
[module: SuppressMessage("Microsoft.Usage", "CA2237:MarkISerializableTypesWithSerializable", Scope = "type", Target = "Amazon.Runtime.AmazonServiceException")]
[module: SuppressMessage("Microsoft.Usage", "CA2237:MarkISerializableTypesWithSerializable", Scope = "type", Target = "Amazon.Runtime.SignatureException")]
[module: SuppressMessage("Microsoft.Usage", "CA2237:MarkISerializableTypesWithSerializable", Scope = "type", Target = "Amazon.Runtime.AmazonClientException")]
[module: SuppressMessage("Microsoft.Usage", "CA2237:MarkISerializableTypesWithSerializable", Scope = "type", Target = "Amazon.Runtime.Internal.Auth.SignatureException")]
[module: SuppressMessage("Microsoft.Usage", "CA2237:MarkISerializableTypesWithSerializable", Scope = "type", Target = "Amazon.Runtime.Internal.HttpErrorResponseException")]

// Use generic event handler instances, breaking change
[module: SuppressMessage("Microsoft.Design", "CA1003:UseGenericEventHandlerInstances", Scope = "type", Target = "Amazon.Runtime.RequestEventHandler")]
[module: SuppressMessage("Microsoft.Design", "CA1003:UseGenericEventHandlerInstances", Scope = "type", Target = "Amazon.Runtime.ExceptionEventHandler")]
[module: SuppressMessage("Microsoft.Design", "CA1003:UseGenericEventHandlerInstances", Scope = "type", Target = "Amazon.Runtime.ResponseEventHandler")]
[module: SuppressMessage("Microsoft.Design", "CA1003:UseGenericEventHandlerInstances", Scope = "type", Target = "Amazon.Runtime.PreRequestEventHandler")]

// Declare event handlers correctly, breaking change
[module: SuppressMessage("Microsoft.Design", "CA1009:DeclareEventHandlersCorrectly", Scope = "member", Target = "Amazon.Runtime.AbstractWebServiceClient.#BeforeRequestEvent")]
[module: SuppressMessage("Microsoft.Design", "CA1009:DeclareEventHandlersCorrectly", Scope = "member", Target = "Amazon.Runtime.AbstractWebServiceClient.#ExceptionEvent")]
[module: SuppressMessage("Microsoft.Design", "CA1009:DeclareEventHandlersCorrectly", Scope = "member", Target = "Amazon.Runtime.AbstractWebServiceClient.#AfterResponseEvent")]

// Use events where appropriate
[module: SuppressMessage("Microsoft.Design", "CA1030:UseEventsWhereAppropriate", Scope = "member", Target = "Amazon.Runtime.Internal.IRequestEvents.#FireBeforeRequestEvent(System.Object,Amazon.Runtime.RequestEventArgs)")]

// Do not call overridable methods in constructors
[module: SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors", Scope = "member", Target = "Amazon.Runtime.Internal.Util.EncryptionWrapper.#.ctor()")]
[module: SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors", Scope = "member", Target = "Amazon.Runtime.Internal.Util.DecryptionWrapper.#.ctor()")]

// Suppressions for Runtime generic methods
[module: SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Scope = "member", Target = "Amazon.Runtime.Internal.RuntimePipeline.#RemoveHandler`1()")]
[module: SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Scope = "member", Target = "Amazon.Runtime.Internal.RuntimePipeline.#AddHandlerAfter`1(Amazon.Runtime.IPipelineHandler)")]
[module: SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Scope = "member", Target = "Amazon.Runtime.Internal.RuntimePipeline.#ReplaceHandler`1(Amazon.Runtime.IPipelineHandler)")]
[module: SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Scope = "member", Target = "Amazon.Runtime.Internal.RuntimePipeline.#AddHandlerBefore`1(Amazon.Runtime.IPipelineHandler)")]
[module: SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Scope = "member", Target = "Amazon.Runtime.Internal.DefaultRetryPolicy.#IsInnerException`1(System.Exception)")]
[module: SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Scope = "member", Target = "Amazon.Util.PaginatedResourceFactory.#Create`3(Amazon.Util.PaginatedResourceInfo)")]
[module: SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Scope = "member", Target = "Amazon.Runtime.Internal.ServiceClientHelpers.#CreateServiceFromAnother`2(Amazon.Runtime.AmazonServiceClient)")]
[module: SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Scope = "member", Target = "Amazon.Util.Internal.PlatformServices.ServiceFactory.#RegisterService`1(System.Type)")]

[module: SuppressMessage("Microsoft.Design", "CA1021:AvoidOutParameters", Scope = "member", Target = "Amazon.Runtime.Internal.DefaultRetryPolicy.#IsInnerException`1(System.Exception,!!0&)", MessageId = "1#")]
[module: SuppressMessage("Microsoft.Design", "CA1021:AvoidOutParameters", Scope = "member", Target = "Amazon.Runtime.Internal.Util.ICache`2.#GetValue(!0,System.Func`2<!0,!1>,System.Boolean&)", MessageId = "2#")]

// Suppression for methods on IHttpRequest
[module: SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Scope = "member", Target = "Amazon.Runtime.IHttpRequest`1.#GetRequestContent()")]
[module: SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Scope = "member", Target = "Amazon.Runtime.IHttpRequest`1.#GetResponse()")]

// Suppression for HttpWebRequestResponseData, HttpWebRequestResponseData.ResponseBody is disposed on its own.
[module: SuppressMessage("Microsoft.Design", "CA1001:TypesThatOwnDisposableFieldsShouldBeDisposable", Scope = "type", Target = "Amazon.Runtime.Internal.Transform.HttpWebRequestResponseData")]

// Suppression for UserCrypto DATA_BLOB, native resources cleaned up in the code
[module: SuppressMessage("Microsoft.Design", "CA1049:TypesThatOwnNativeResourcesShouldBeDisposable", Scope = "type", Target = "Amazon.Runtime.Internal.Settings.UserCrypto+DATA_BLOB")]

// Passing Uri instead of string (in these cases, the uri overload is called by the string overload)
[module: SuppressMessage("Microsoft.Usage", "CA2234:PassSystemUriObjectsInsteadOfStrings", Scope = "member", Target = "Amazon.S3.Internal.AmazonS3RedirectHandler.#FinalizeForRedirect(Amazon.Runtime.IExecutionContext,System.String)")]

// Move PInvoke to separate file
[module: SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass", Scope = "member", Target = "Amazon.Runtime.Internal.Settings.UserCrypto.#CryptUnprotectData(Amazon.Runtime.Internal.Settings.UserCrypto+DATA_BLOB&,System.String,Amazon.Runtime.Internal.Settings.UserCrypto+DATA_BLOB&,System.IntPtr,Amazon.Runtime.Internal.Settings.UserCrypto+CRYPTPROTECT_PROMPTSTRUCT&,Amazon.Runtime.Internal.Settings.UserCrypto+CryptProtectFlags,Amazon.Runtime.Internal.Settings.UserCrypto+DATA_BLOB&)")]
[module: SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass", Scope = "member", Target = "Amazon.Runtime.Internal.Settings.UserCrypto.#CryptProtectData(Amazon.Runtime.Internal.Settings.UserCrypto+DATA_BLOB&,System.String,Amazon.Runtime.Internal.Settings.UserCrypto+DATA_BLOB&,System.IntPtr,Amazon.Runtime.Internal.Settings.UserCrypto+CRYPTPROTECT_PROMPTSTRUCT&,Amazon.Runtime.Internal.Settings.UserCrypto+CryptProtectFlags,Amazon.Runtime.Internal.Settings.UserCrypto+DATA_BLOB&)")]

// Pass types by reference
[module: SuppressMessage("Microsoft.Design", "CA1045:DoNotPassTypesByReference", Scope = "member", Target = "Amazon.Util.Internal.InternalSDKUtils.#SetIsSet`2(System.Boolean,System.Collections.Generic.Dictionary`2<!!0,!!1>&)", MessageId = "1#")]
[module: SuppressMessage("Microsoft.Design", "CA1045:DoNotPassTypesByReference", Scope = "member", Target = "Amazon.Util.Internal.InternalSDKUtils.#SetIsSet`1(System.Boolean,System.Collections.Generic.List`1<!!0>&)", MessageId = "1#")]
[module: SuppressMessage("Microsoft.Design", "CA1045:DoNotPassTypesByReference", Scope = "member", Target = "Amazon.Util.Internal.InternalSDKUtils.#SetIsSet`1(System.Boolean,System.Nullable`1<!!0>&)", MessageId = "1#")]

// Collections that do not require ICollection<T> interface
[module: SuppressMessage("Microsoft.Design", "CA1010:CollectionsShouldImplementGenericInterface", Scope = "type", Target = "Amazon.WritableConfigurationElementCollection`1")]

// Empty Logger instance can be cached
[module: SuppressMessage("AwsSdkRules", "CR1002:PreventStaticLoggersRule", Scope = "member", Target = "Amazon.Runtime.Internal.Util.Logger.#emptyLogger")]

// Call HashAlgorithm.Create is for "SHA-256"
[module: SuppressMessage("AwsSdkRules", "CR1001:PreventHashAlgorithmCreateRule", Scope = "member", Target = "Amazon.Util.CryptoUtilFactory+CryptoUtil.#get_SHA256HashAlgorithmInstance()")]

// Visible instance fields
[module: SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Scope = "member", Target = "Amazon.Runtime.RefreshingAWSCredentials.#_currentState")]

// Indirect expose method with LinkDemands
[module: SuppressMessage("Microsoft.Security", "CA2122:DoNotIndirectlyExposeMethodsWithLinkDemands", Scope = "member", Target = "Amazon.Runtime.Internal.ClientContext.#.ctor(System.String,Amazon.Runtime.Internal.ClientContextConfig)")]
