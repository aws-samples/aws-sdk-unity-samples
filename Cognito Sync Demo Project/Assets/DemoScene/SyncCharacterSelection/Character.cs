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
/// Stores a character's information (ie: color and experience).
/// To simulate the character gaining experience, it will increase when clicked.
/// </summary>
public class Character : MonoBehaviour {

    Color color;
    int experience;

    void Awake ()
    {
        SetColor (new Color (0.2f+Random.Range (0.0f, 0.7f), 0.2f+Random.Range (0.0f, 0.7f), 0.2f+Random.Range (0.0f, 0.7f)));
        SetExp (0);
    }

    /// <summary>
    /// Returns a string with the character's information that
    /// can be deserialized by the Deserialize() function.
    /// </summary>
    public string Serialize()
    {
        string s = color.r + ";" + color.g + ";" + color.b + ";" + experience;
        return s;
    }

    /// <summary>
    /// Initializes the character's parameters from a serialized string
    /// </summary>
    /// <param name="s">The serialized content</param>
    public void Deserialize(string s)
    {
        string[] features = s.Split(';');
        Color color = new Color(float.Parse(features[0]),float.Parse(features[1]),float.Parse(features[2]));
        SetColor (color);
        int experience = int.Parse (features[3]);
        SetExp (experience);
    }

    /// <summary>
    /// Changes the color of the character's material.
    /// </summary>
    /// <param name="color">The new color</param>
    public void SetColor(Color color)
    {
        this.color = color;
        foreach (MeshRenderer m in gameObject.GetComponentsInChildren<MeshRenderer>())
        {
            m.material.color = color;
        }
    }

    /// <summary>
    /// Sets the exp.
    /// </summary>
    /// <param name="experience">Experience.</param>
    public void SetExp(int experience)
    {
        this.experience = experience;
        int level = Mathf.CeilToInt(Mathf.Sqrt(experience/100))+1;
        TextMesh lvlText = GetComponentInChildren<TextMesh>();
        lvlText.text = "Lvl "+level+"\nExp: "+experience;
    }

    /// <summary>
    /// Detects clicks on the character, and then increases its experience.
    /// </summary>
    void Update()
    {
       if (Input.GetMouseButtonDown(0))
       {
           RaycastHit hitInfo = new RaycastHit();
           if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo) && hitInfo.transform == this.transform)
           {
               SetExp(experience + Random.Range(20,40));
           }
        }
     }
}
