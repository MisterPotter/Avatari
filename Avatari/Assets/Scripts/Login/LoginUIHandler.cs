using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
using System.Collections;
using SimpleJSON;

public class LoginUIHandler : MonoBehaviour {

    private Text avatarName;
    private GenerateToken tokenGenerator;

    private bool browserOpened;
    private string name;
    private string token;
    private int sessionKey;

    private void Awake() {
        this.avatarName = Utility.LoadObject<Text>("AvatarInput");
        this.tokenGenerator = Utility.LoadObject<GenerateToken>(
            "TokenGenerator"
        );
        this.browserOpened = false;
    }

    public void Login() {
        if (avatarName.text != "") {
            this.name = this.avatarName.text;
            this.token = this.tokenGenerator.token;
            StartCoroutine(CheckAccountExists(avatarName.text));
        }
    }

    /**
     *  Checks if an account already exists. If it does, then authenticate,
     *  If it doesn't already exists, create it, then authenticate.
     */
    private IEnumerator CheckAccountExists(string name) {
        WWWForm checkAccountForm = new WWWForm();
        checkAccountForm.AddField(Config.TokenParam, this.token);
        checkAccountForm.AddField(Config.LoginNameParam, name);
        WWW checkAcccountRequest = new WWW(Config.ControllerURLLogin, checkAccountForm);

        yield return checkAcccountRequest;

        var data = JSON.Parse(checkAcccountRequest.text);
        var response = data["status"];

        if(response.AsInt != 200) {
            StartCoroutine(CreateAccount(name));
        } else {
            this.sessionKey = data["data"].AsInt;
            Authenticate();
        }
    }

    /**
     * Creates an account and starts the login functionality.
     */
    private IEnumerator CreateAccount(string name) {
        WWWForm createAccountForm = new WWWForm();
        createAccountForm.AddField(Config.TokenParam, this.token);
        createAccountForm.AddField(Config.LoginNameParam, name);
        WWW createAccountRequest = new WWW(Config.ControllerURLAccounts, createAccountForm);

        yield return createAccountRequest;

        var data = JSON.Parse(createAccountRequest.text);
        var response = data["status"];

        if (response.AsInt == 200) {
            StartCoroutine(CheckAccountExists(name));
        } else {
            throw new Exception("Error, could not create account: " + name);
        }
    }

    /**
     *  Check if autentication was successfull.
     */
    private IEnumerator IsAuthenticated() {
        WWWForm authForm = new WWWForm();
        authForm.AddField(Config.SessionKey, this.sessionKey);

        WWW checkAuthRequest = new WWW(Config.ControllerURLIsOAuth, authForm);
        yield return checkAuthRequest;

        var data = JSON.Parse(checkAuthRequest.text);
        var response = data["status"];

        if (response.AsInt == 200) {
            SceneManager.LoadScene("home");
        } else {
            throw new Exception("Did not authenticate!");
        }
    }

    /**
     *  Open up the url for authenitcation.
     */
    private void Authenticate() {
        Application.OpenURL(String.Format(Config.ControllerURLOAuthFormat, this.token));
        browserOpened = true;
    }

    private void OnApplicationFocus(bool focusState) {
        if(focusState && browserOpened) {
            StartCoroutine(IsAuthenticated());
        }
    }

}
