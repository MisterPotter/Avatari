using UnityEngine;

public class Config {
    /*
     *  Authentication
     */
    public static readonly string ControllerURLRoot = "http://ec2-54-187-83-158.us-west-2.compute.amazonaws.com/app_dev.php/";
    public static readonly string ControllerURLAccounts = ControllerURLRoot + "account/create";
    public static readonly string ControllerURLLogin = ControllerURLRoot + "account/login";
    public static readonly string ControllerURLOAuthFormat = ControllerURLRoot + "oauth?token={0}";
    public static readonly string ControllerURLIsOAuth = ControllerURLRoot + "isOauth";

    /*
     *  Inventory.
     *
     *  API refers to all the content we have access to for the
     *  Fitbit API. What we actually get back here is what the server
     *  requests, if differen't data is needed, the requests from the
     *  server to the fitbit API need to be changed.
     */
    public static readonly string ControllerURLAPI = ControllerURLRoot + "api";
    public static readonly string ControllerURLAreas = ControllerURLRoot + "areas";
    public static readonly string ControllerURLItems = ControllerURLRoot + "items";
    public static readonly string ControllerURLPlayer = ControllerURLRoot + "avatar";
    public static readonly string ControllerURLTaris = ControllerURLRoot + "taris";

    /*
     *  Request parameters.
     */
    public static readonly string LoginNameParam = "avatar_name";
    public static readonly string TokenParam = "token";

    /*
     *  Keys for parsing responses.
     */
    public static readonly string Data = "data";
    public static readonly string SessionKey = "avatari_user_id";
    public static readonly string Status = "status";
}
