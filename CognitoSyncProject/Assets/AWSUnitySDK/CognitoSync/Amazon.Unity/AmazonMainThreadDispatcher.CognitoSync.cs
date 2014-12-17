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
using System.Threading;
using System.Collections;

using Amazon.CognitoSync;
using Amazon.CognitoSync.Model;
using Amazon.CognitoSync.SyncManager;

namespace Amazon.Unity
{
	public partial class AmazonMainThreadDispatcher
	{	
		private class AmazonCognitoCallbackState : AmazonCallbackState
		{
			AmazonCognitoCallback _callback;
			AmazonCognitoResult _result;
			
			public AmazonCognitoCallbackState(AmazonCognitoCallback callback, AmazonCognitoResult result)
			{
				this._callback = callback;
				this._result = result;
			}
			
			public override IEnumerator FireCallbackOnCoRoutine()
			{
				if (_callback != null)
				{
					_callback(_result);
				}
				yield break;
			}
		}

		internal static void ExecCallback(AmazonCognitoCallback callback, AmazonCognitoResult result)
		{
			_callbackQueue.Enqueue (new AmazonCognitoCallbackState(callback, result));
		}
	}
}

