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
using System.Collections.Generic;

/// <summary>
/// Creates, stores and deletes the Characters in the scene.
/// </summary>
public class CharacterList : MonoBehaviour {

    /// <summary>
    /// The Character prefab to instantiate when InstantiateCharacter is called.
    /// </summary>
    public Character characterPrefab;

    /// <summary>
    /// Separation between two consecutively created characters
    /// </summary>
    private float characterDistance = 2.5f;

    /// <summary>
    /// The actual list of characters created by this class.
    /// </summary>
    private List<Character> characters = new List<Character>();

    void OnGUI ()
    {
        float ratio = Screen.width/600.0f;
        GUI.skin.button.fontSize = (int)(15*ratio);

        if (GUI.Button (new Rect ((Screen.width / 2) - 60*ratio, 30*ratio, 120*ratio, 30*ratio), "New character"))
        {
            InstantiateCharacter();
        }

        if(GUI.Button(new Rect(Screen.width-160*ratio, Screen.height - 50*ratio, 120*ratio, 30*ratio), "Delete all"))
        {
            DeleteAllCharacters();
        }
    }

    /// <summary>
    /// Create a new character and adds it to the list.
    /// </summary>
    /// <returns>The character.</returns>
    public Character InstantiateCharacter()
    {
        GameObject unit;
        if (characters.Count == 0)
        {
            unit = Instantiate (characterPrefab.gameObject) as GameObject;
        }
        else
        {
            Character last = characters[characters.Count-1];
			unit = Instantiate (characterPrefab.gameObject, last.transform.position + (last.transform.right * characterDistance), last.transform.rotation) as GameObject;
        }
        Character character = unit.GetComponent<Character> ();
        characters.Add (character);
        return character;
    }

    /// <summary>
    /// Destroys all the characters contained by this class.
    /// </summary>
    public void DeleteAllCharacters()
    {
        foreach (Character c in characters)
        {
            GameObject.Destroy(c.gameObject);
        }
        characters.Clear ();
    }

    /// <summary>
    /// Returns a list of strings, each one containing one of the serialized representation of a Character.
    /// </summary>
    /// <returns>The list of serialized characters</returns>
    public string[] SerializeCharacters()
    {
        List<string> serialized = new List<string>();
        foreach (Character character in characters)
        {
            serialized.Add(character.Serialize());
        }
        return serialized.ToArray ();
    }

    /// <summary>
    /// Initializes the list of characters with a list of serialized strings, like the one returned by SerializeCharacters()
    /// </summary>
    /// <param name="characterStrings">List of serialized characters.</param>
    public void DeserializeCharacters(string[] characterStrings)
    {
        DeleteAllCharacters ();
        foreach (string s in characterStrings)
        {
            Character character = InstantiateCharacter();
            character.Deserialize(s);
        }
    }


}
