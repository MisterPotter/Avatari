using UnityEngine;
using System;

public class GenerateToken : MonoBehaviour {

    public string token {
        get {
            return PlayerPrefs.GetString("token");
        }
    }

    private void Awake() {
        CheckGUID();
    }

    private void CheckGUID() {
        String token = PlayerPrefs.GetString("token");
        if(token == "") {
            PlayerPrefs.SetString("token", Guid.NewGuid().ToString());
        }
    }

}
