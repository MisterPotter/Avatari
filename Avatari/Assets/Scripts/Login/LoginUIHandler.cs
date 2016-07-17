using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Threading;
using SimpleJSON;

public class LoginUIHandler : MonoBehaviour {

    private Text avatarName;
    private Cache cache;
    private PopulateCache cachePopulator;
    private GenerateToken tokenGenerator;

    private bool authenticated;
    private bool browserOpened;
    private string token;
    private int sessionKey;

    private void Awake() {
        Initialize();
    }

    private void Initialize() {
        this.avatarName = Utility.LoadObject<Text>("AvatarInput");
        this.cache = Utility.LoadObject<Cache>("Cache");
        this.cachePopulator = Utility.LoadObject<PopulateCache>(
            "PopulateCache"
        );
        this.tokenGenerator = Utility.LoadObject<GenerateToken>(
            "TokenGenerator"
        );
        this.browserOpened = false;
        this.authenticated = false;

}

    public void Login() {
        if (avatarName.text != "") {
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
            Debug.Log(checkAcccountRequest.text);
            StartCoroutine(CreateAccount(name));
        } else {
            this.sessionKey = data["data"].AsInt;
            this.cache.sessionKey = this.sessionKey;
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
            this.cachePopulator.Populate();
            this.authenticated = true;
        } else {
            throw new Exception("Did not authenticate!");
        }
    }

    /**
     *  Open up the url for authenitcation.
     */
    private void Authenticate() {
        Debug.Log("Authenication nation");
        Application.OpenURL(String.Format(Config.ControllerURLOAuthFormat, this.token));
        browserOpened = true;
    }

    /**
     *  Check when the app in back in focus and the browser is open
     */
    private void OnApplicationFocus(bool focusState) {
        if(focusState && this.browserOpened && ! this.authenticated) {
            StartCoroutine(IsAuthenticated());
        }
    }

}
