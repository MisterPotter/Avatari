using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

/**
 *  Utility functions to be used throughout the project.
 */
public class Utility {

    /**
     *  @author: Tyler
     *  @summary: loads a game object from the scene
     *  @return: Default for type T if the game object is not found,
     *      the found object otherwise.
     */
    public static T LoadObject<T>(string tag) {
        GameObject desiredGameObject = GameObject.FindGameObjectWithTag(tag);
        T desiredObject = default(T);

        if(desiredGameObject != null) {
            desiredObject = desiredGameObject.GetComponent<T>();
        } else {
            throw new Exception("Game object with tag: " + tag
                + " was not found");
        }

        return desiredObject;
    }

    public static Sprite GetSprite(string spriteName, Sprite[] sprites) {
        foreach (Sprite sprite in sprites) {
            if (sprite.name == spriteName) {
                return sprite;
            }
        }

        throw new Exception("Sprite with name: " + spriteName + " not found.");
    }

    public static T DeepClone<T>(T obj) {
        using (var ms = new MemoryStream()) {
            var formatter = new BinaryFormatter();
            formatter.Serialize(ms, obj);
            ms.Position = 0;

            return (T)formatter.Deserialize(ms);
        }
    }
}
