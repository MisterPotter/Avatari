using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LoginUIHandler : MonoBehaviour {

    private Text avatarName;

    private void Awake() {
        avatarName = Utility.LoadObject<Text>("AvatarInput");
        WWW www = new WWW(Config.ControllerURLRoot);
        StartCoroutine(WaitForRequest(www));
    }

    public void Login() {
        Debug.Log(avatarName.text);

        // Post our account
        WWWForm accountsForm = new WWWForm();
        accountsForm.AddField("token", Config.Token);
        accountsForm.AddField("avatar_name", avatarName.text);
        WWW accountWWW = new WWW(Config.ControllerURLAccounts, accountsForm);
        StartCoroutine(WaitForRequest(accountWWW));

        // Login 
        WWWForm loginForm = new WWWForm();
        loginForm.AddField("token", Config.Token);
        WWW loginWWW = new WWW(Config.ControllerURLLogin, loginForm);

        // Open up tab
        Application.OpenURL(Config.ControllerURLOAuth);
    }

    IEnumerator WaitForRequest(WWW www) {
        yield return www;

        if (www.error == null) {
            Debug.Log("WWW Ok: " + www.text);
        } else {
            Debug.Log("WWW Error: " + www.error);
        }
    }

}
