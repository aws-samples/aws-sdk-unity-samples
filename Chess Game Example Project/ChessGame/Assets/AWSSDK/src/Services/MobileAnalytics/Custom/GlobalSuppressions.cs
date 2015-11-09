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


// Identifier should not match keywords
[module: SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", Scope="type", Target="Amazon.MobileAnalytics.Model.Event", MessageId="Event")]

// supress warning for implementing ISerializable interface
[module: SuppressMessage("Microsoft.Usage", "CA2237:MarkISerializableTypesWithSerializable", Scope="type", Target="Amazon.MobileAnalytics.MobileAnalyticsManager.Internal.EventStoreException")]

// supress warning for implementing IDisposable interface
[module: SuppressMessage("Microsoft.Design", "CA1001:TypesThatOwnDisposableFieldsShouldBeDisposable", Scope = "type", Target = "Amazon.MobileAnalytics.MobileAnalyticsManager.Internal.DeliveryClient")]

// Keep MobileAnalyticsManager namespace
[module: SuppressMessage("Microsoft.Naming", "CA1724:TypeNamesShouldNotMatchNamespaces", Scope = "type", Target = "Amazon.MobileAnalytics.MobileAnalyticsManager.MobileAnalyticsManager")]

// supress warnings for catching general exception
[module: SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Scope = "member", Target = "Amazon.MobileAnalytics.MobileAnalyticsManager.MobileAnalyticsManager.#ResumeSession()")]
[module: SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Scope = "member", Target = "Amazon.MobileAnalytics.MobileAnalyticsManager.MobileAnalyticsManager.#PauseSession()")]
[module: SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Scope = "member", Target = "Amazon.MobileAnalytics.MobileAnalyticsManager.Internal.Session.#RetrieveSessionStorage()")]
[module: SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Scope = "member", Target = "Amazon.MobileAnalytics.MobileAnalyticsManager.Internal.DeliveryClient.#EnqueueEventsForDeliveryAsync(Amazon.MobileAnalytics.Model.Event)")]
[module: SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Scope = "member", Target = "Amazon.MobileAnalytics.MobileAnalyticsManager.Internal.SQLiteEventStore.#GetEvents(System.String,System.Int32)")]
[module: SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Scope = "member", Target = "Amazon.MobileAnalytics.MobileAnalyticsManager.Internal.SQLiteEventStore.#DeleteEvent(System.Collections.Generic.List`1<System.String>)")]
[module: SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Scope = "member", Target = "Amazon.MobileAnalytics.MobileAnalyticsManager.Internal.SQLiteEventStore.#PutEvent(System.String,System.String)")]
[module: SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Scope = "member", Target = "Amazon.MobileAnalytics.MobileAnalyticsManager.Internal.SQLiteEventStore.#NumberOfEvents(System.String)")]
[module: SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Scope = "member", Target = "Amazon.MobileAnalytics.MobileAnalyticsManager.Internal.BackgroundRunner.#DoWork()")]
[module: SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Scope = "member", Target = "Amazon.MobileAnalytics.MobileAnalyticsManager.Internal.SQLiteEventStore.#SetupSQLiteEventStore()")]
[module: SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Scope = "member", Target = "Amazon.MobileAnalytics.MobileAnalyticsManager.Internal.DeliveryClient.#SubmitEvents(System.Collections.Generic.List`1<System.String>,System.Collections.Generic.List`1<Amazon.MobileAnalytics.Model.Event>)")]
[module: SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Scope="member", Target="Amazon.MobileAnalytics.MobileAnalyticsManager.Internal.DeliveryClient.#EnqueueEventsForDelivery(Amazon.MobileAnalytics.Model.Event)")]

[module: SuppressMessage("Microsoft.Performance", "CA1810:InitializeReferenceTypeStaticFieldsInline", Scope = "member", Target = "Amazon.MobileAnalytics.MobileAnalyticsManager.MobileAnalyticsManager.#.cctor()")]
[module: SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Scope = "member", Target = "Amazon.MobileAnalytics.MobileAnalyticsManager.Internal.ConnectivityPolicy.#HasNetworkConnectivity()")]

[module: SuppressMessage("Microsoft.Security", "CA2122:DoNotIndirectlyExposeMethodsWithLinkDemands", Scope = "member", Target = "Amazon.Runtime.Internal.ClientContext.#.ctor(System.String,Amazon.Runtime.Internal.ClientContextConfig)")]
[module: SuppressMessage("Microsoft.Security", "CA2122:DoNotIndirectlyExposeMethodsWithLinkDemands", Scope = "member", Target = "Amazon.MobileAnalytics.MobileAnalyticsManager.Internal.Session.#.ctor(System.String,Amazon.MobileAnalytics.MobileAnalyticsManager.MobileAnalyticsManagerConfig)")]
[module: SuppressMessage("Microsoft.Security", "CA2122:DoNotIndirectlyExposeMethodsWithLinkDemands", Scope = "member", Target = "Amazon.MobileAnalytics.MobileAnalyticsManager.Internal.SQLiteEventStore.#.ctor(Amazon.MobileAnalytics.MobileAnalyticsManager.MobileAnalyticsManagerConfig)")]

[module: SuppressMessage("Microsoft.Performance", "CA1810:InitializeReferenceTypeStaticFieldsInline", Scope = "member", Target = "Amazon.MobileAnalytics.MobileAnalyticsManager.Internal.SQLiteEventStore.#.cctor()")]
[module: SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Scope = "member", Target = "Amazon.MobileAnalytics.MobileAnalyticsManager.Internal.SQLiteEventStore.#DBfileFullPath")]