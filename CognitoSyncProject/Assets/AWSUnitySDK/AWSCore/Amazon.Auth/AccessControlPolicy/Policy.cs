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

using Amazon.Auth.AccessControlPolicy.Internal;

namespace Amazon.Auth.AccessControlPolicy
{
    /// <summary>
    /// An AWS access control policy is a object that acts as a container for one or
    /// more statements, which specify fine grained rules for allowing or denying
    /// various types of actions from being performed on your AWS resources.
    /// <para>
    /// By default, all requests to use your resource coming from anyone but you are
    /// denied. Access control polices can override that by allowing different types
    /// of access to your resources, or by explicitly denying different types of
    /// access.
    /// </para>
    /// <para>
    /// Each statement in an AWS access control policy takes the form:
    /// "A has permission to do B to C where D applies".
    /// <list type="definition">
    ///     <item>
    ///         <term>A is the prinicpal</term>
    ///         <description>The AWS account that is making a request to
    ///             access or modify one of your AWS resources.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <term>B is the action</term>
    ///         <description>the way in which your AWS resource is being accessed or modified, such
    ///             as sending a message to an Amazon SQS queue, or storing an object in an Amazon S3 bucket.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <term>C is the resource</term>
    ///         <description>your AWS entity that the principal wants to access, such
    ///             as an Amazon SQS queue, or an object stored in Amazon S3.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <term>D is the set of conditions</term>
    ///         <description>optional constraints that specify when to allow or deny
    ///             access for the principal to access your resource.  Many expressive conditions are available,
    ///             some specific to each service.  For example you can use date conditions to allow access to
    ///             your resources only after or before a specific time.
    ///         </description>
    ///     </item>
    /// </list>
    /// </para>
    /// <para>
    /// Note that an AWS access control policy should not be confused with the
    /// similarly named "POST form policy" concept used in Amazon S3.
    /// </para>
    /// </summary>
    public class Policy
    {

        /// <summary>
        /// The default policy version
        /// </summary>
        private const string DEFAULT_POLICY_VERSION = "2012-10-17";

        private string id;
        private string version = DEFAULT_POLICY_VERSION;
        private IList<Statement> statements = new List<Statement>();

        /// <summary>
        /// Constructs an empty AWS access control policy ready to be populated with
        /// statements.
        /// </summary>
        public Policy()
        {
        }

        /// <summary>
        /// Constructs a new AWS access control policy with the specified policy ID.
        /// The policy ID is a user specified string that serves to help developers
        /// keep track of multiple polices. Policy IDs are often used as a human
        /// readable name for a policy.
        /// </summary>
        /// <param name="id">The policy ID for the new policy object. Policy IDs serve to
        ///            help developers keep track of multiple policies, and are often
        ///            used to give the policy a meaningful, human readable name.</param>
        public Policy(string id)
        {
            this.id = id;
        }

        /// <summary>
        /// Constructs a new AWS access control policy with the specified policy ID
        /// and collection of statements. The policy ID is a user specified string
        /// that serves to help developers keep track of multiple polices. Policy IDs
        /// are often used as a human readable name for a policy.
        /// </summary>
        /// <param name="id">The policy ID for the new policy object. Policy IDs serve to
        ///            help developers keep track of multiple policies, and are often
        ///            used to give the policy a meaningful, human readable name.</param>
        /// <param name="statements">The statements to include in the new policy.</param>
        public Policy(string id, IList<Statement> statements)
        {
            this.id = id;
            this.statements = statements;
        }

        /// <summary>
        /// Gets and Sets the policy ID for this policy. Policy IDs serve to help
        /// developers keep track of multiple policies, and are often used as human
        /// readable name for a policy.
        /// </summary>
        public string Id
        {
            get
            {
                return id;
            }
            set
            {
                this.id = value;
            }
        }

        /// <summary>
        /// Sets the policy ID for this policy and returns the updated policy so that
        /// multiple calls can be chained together.
        /// <para>
        /// Policy IDs serve to help developers keep track of multiple policies, and
        /// are often used as human readable name for a policy.
        /// </para>
        /// </summary>
        /// <paraparam name="id">The polich ID for this policy</paraparam>
        /// <returns>this instance</returns>
        public Policy WithId(string id)
        {
            Id = id;
            return this;
        }

        /// <summary>
        /// Gets and sets the version of this AWS policy.
        /// </summary>
        public string Version
        {
            get
            {
                return version;
            }
            set
            {
                this.version = value;
            }
        }

        /// <summary>
        /// Gets and Sets the collection of statements contained by this policy. Individual
        /// statements in a policy are what specify the rules that enable or disable
        /// access to your AWS resources.
        /// </summary>
        public IList<Statement> Statements
        {
            get
            {
                return statements;
            }
            set
            {
                this.statements = value;
            }
        }

        /// <summary>
        /// Sets the collection of statements contained by this policy and returns
        /// this policy object so that additional method calls can be chained
        /// together.
        /// <para>
        /// Individual statements in a policy are what specify the rules that enable
        /// or disable access to your AWS resources.
        /// </para>
        /// </summary>
        /// <param name="statements">The collection of statements included in this policy.</param>
        /// <returns>this instance</returns>
        public Policy WithStatements(params Statement[] statements)
        {
            if (this.Statements == null)
            {
                this.Statements = new List<Statement>();
            }
            foreach (Statement element in statements)
            {
                this.Statements.Add(element);
            }

            return this;
        }

        /// <summary>
        /// Returns a JSON string representation of this AWS access control policy,
        /// suitable to be sent to an AWS service as part of a request to set an
        /// access control policy.
        /// </summary>
        /// <returns>A JSON string representation of this AWS access control policy.</returns>
        public string ToJson()
        {
            return ToJson(true);
        }

        /// <summary>
        /// Returns a JSON string representation of this AWS access control policy,
        /// suitable to be sent to an AWS service as part of a request to set an
        /// access control policy.
        /// </summary>
        /// <param name="prettyPrint">Toggle pretty print for the generated JSON document</param>
        /// <returns>A JSON string representation of this AWS access control policy.</returns>
        public string ToJson(bool prettyPrint)
        {
            return JsonPolicyWriter.WritePolicyToString(prettyPrint, this);
        }

        /// <summary>
        /// Parses a JSON document of a policy and creates a Policy object.
        /// </summary>
        /// <param name="json">JSON document of a policy.</param>
        /// <returns></returns>
        public static Policy FromJson(string json)
        {
            return JsonPolicyReader.ReadJsonStringToPolicy(json);
        }
    }
}
