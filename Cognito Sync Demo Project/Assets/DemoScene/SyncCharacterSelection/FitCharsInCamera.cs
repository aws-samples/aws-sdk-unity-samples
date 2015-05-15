/*
 * Copyright 2014-2014 Amazon.com, Inc. or its affiliates. All Rights Reserved.
 *
 *
 * Licensed under the AWS Mobile SDK for Unity Sample Application License Agreement (the "License").
 * You may not use this file except in compliance with the License.
 * A copy of the License is located in the "license" file accompanying this file.
 * See the License for the specific language governing permissions and limitations under the License.
 *
 */

using UnityEngine;
using System.Collections;

/// <summary>
/// Moves the camera away as more characters (ie: elements with tag "Player")
/// are added to the scene. To be attached to the camera.
/// </summary>
public class FitCharsInCamera : MonoBehaviour {

    int prevCount = 0;
    float time = 1;

    Vector3 meanVector3Pos;
    Vector3 initialDisplacement;

    /// <summary>
    /// Stores the initial position of the camera.
    /// </summary>
    void Start()
    {
        initialDisplacement = transform.position;
    }

    /// <summary>
    /// Detect new characters and move the camera acordingly.
    /// </summary>
    void Update ()
    {
        GameObject[] characters = GameObject.FindGameObjectsWithTag("Player");

        int count = characters.Length;

        if (count != prevCount)
        {
            prevCount = count;
            if (count == 0)
            {
                meanVector3Pos = initialDisplacement;
            }
            else
            {
                Vector3 tmpVector3Pos = new Vector3 ();
                foreach (GameObject go in characters) {
                    tmpVector3Pos = tmpVector3Pos + go.transform.position;
                }
                meanVector3Pos = tmpVector3Pos/count + initialDisplacement + new Vector3(0,0, -count);
            }
            time = 0;
        }

        if (time < 1)
        {
            Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, meanVector3Pos, time);
            time += Time.deltaTime;
        }

    }

}