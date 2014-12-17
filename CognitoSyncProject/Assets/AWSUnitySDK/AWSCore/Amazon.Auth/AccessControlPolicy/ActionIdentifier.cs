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
using System.Collections.Generic;
using System.Text;

namespace Amazon.Auth.AccessControlPolicy
{
    /// <summary>
    /// An access control policy action identifies a specific action in a service
    /// that can be performed on a resource. For example, sending a message to a
    /// queue.
    /// <para>
    /// ActionIdentifiers allow you to limit what your access control policy statement affects.
    /// For example, you could create a policy statement that enables a certain group
    /// of users to send messages to your queue, but not allow them to perform any
    /// other actions on your queue.
    /// </para>
    /// <para>
    /// The action is B in the statement
    /// "A has permission to do B to C where D applies."
    /// </para>
    /// <para>Free form access control policy actions may include a wildcard (*) to match
    /// multiple actions.
    /// </para>
    /// <para>
    /// Constants for known actions can be found in the Amazon.Auth.AccessControlPolicy.ActionIdentifiers namespace.
    /// </para>
    /// </summary>
    public class ActionIdentifier
    {
        private string actionName;

        /// <summary>
        /// Constructs an Actionidentifer with the given action name.
        /// </summary>
        /// <param name="actionName">The name of the action</param>
        public ActionIdentifier(string actionName)
        {
            this.actionName = actionName;
        }

        /// <summary>
        /// Gets and sets the name of this action. For example, 'sqs:SendMessage' is the
        /// name corresponding to the SQS action that enables users to send a message
        /// to an SQS queue.
        /// </summary>
        public string ActionName 
        {
            get
            {
                return this.actionName;
            }
            set
            {
                this.actionName = value;
            }
        }
    }
}
