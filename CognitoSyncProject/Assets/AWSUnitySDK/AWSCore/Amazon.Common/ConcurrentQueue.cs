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

namespace Amazon.Unity
{
    /// <summary>
    /// A simple thread safe FIFO Queue implementation using LinkedList
    /// Use Enqueue/Dequeue for using this datastructure instead of add/remove
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ConcurrentQueue<T>
    {
        LinkedList<T> queue = new LinkedList<T>();

        /// <summary>
        /// Enqueue to item at the end of the queue
        /// </summary>
        /// <param name="item">item to be inserted</param>
        public void Enqueue(T item)
        {
            lock (this.queue)
            {
                queue.AddLast(item);
            }
        }

        /// <summary>
        /// Returns Queue size.
        /// This is not synchronized since its a readonly operation 
        /// </summary>
        /// <returns>Queue size</returns>
        public int Count
        {
            get
            {
                return queue.Count;
            }
        }

        /// <summary>
        /// Removes and returns the first element
        /// </summary>
        /// <returns>First element in the queue</returns>
        public T Dequeue()
        {
            lock (this.queue)
            {
                T item = this.queue.First.Value;
                this.queue.RemoveFirst();
                return item;
            }
        }
    }
}
