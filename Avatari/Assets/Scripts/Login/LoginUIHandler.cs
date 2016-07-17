using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using SimpleJSON;

public class LoginUIHandler : MonoBehaviour {

    private Text avatarName;

    private void Awake() {
        avatarName = Utility.LoadObject<Text>("AvatarInput");
    }

    public void Login() {
        Debug.Log(avatarName.text);
        if (avatarName.text != "") {
            StartCoroutine(CheckAccountExists(avatarName.text));
        }
    }

    /**
     *  Checks if an account already exists. If it does, then authenticate,
     *  If it doesn't already exists, create it, then authenticate.
     */
    private IEnumerator CheckAccountExists(string name) {
        WWWForm checkAccountForm = new WWWForm();
        checkAccountForm.AddField(Config.TokenParam, Config.Token);
        checkAccountForm.AddField(Config.LoginNameParam, name);
        WWW checkAcccountRequest = new WWW(Config.ControllerURLLogin, checkAccountForm);

        yield return checkAcccountRequest;

        var data = JSON.Parse(checkAcccountRequest.text);
        var response = data["status"];

        if(response.AsInt != 200) {
            Debug.Log("Creating account");
            StartCoroutine(CreateAccount(name));
        } else {
            Debug.Log("Authenicating");
            Debug.Log("Ok: " + checkAcccountRequest.text);
            Authenticate();
        }
    }

    /**
     * Creates an account and starts the login functionality.
     */
    private IEnumerator CreateAccount(string name) {
        WWWForm createAccountForm = new WWWForm();
        createAccountForm.AddField(Config.TokenParam, Config.Token);
        createAccountForm.AddField(Config.LoginNameParam, name);
        WWW createAcccountRequest = new WWW(Config.ControllerURLAccounts, createAccountForm);

        yield return createAccountForm;

        var data = JSON.Parse(createAcccountRequest.text);
        var response = data["status"];

        if (response.AsInt != 200) {
            Debug.Log("Account Created");
            StartCoroutine(CheckAccountExists(name));
        } else {
            throw new Exception("Error, could not create account: " + name);
        }
    }

    private void Authenticate() {
        Debug.Log("Opening URL...");
        Application.OpenURL(Config.ControllerURLOAuth);
    }



    private void OnApplicationFocus(bool focusState) {
        if (focusState) Debug.Log("We back baby");
    }

}
