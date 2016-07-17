using UnityEngine;

public class Config {
    /*
     *  Authentication
     */
    public static readonly string ControllerURLRoot = "http://ec2-54-200-193-115.us-west-2.compute.amazonaws.com/app_dev.php/";
    public static readonly string ControllerURLAccounts = ControllerURLRoot + "account/create";
    public static readonly string ControllerURLLogin = ControllerURLRoot + "account/login";
    public static readonly string ControllerURLOAuthFormat = ControllerURLRoot + "oauth?token={0}";
    public static readonly string ControllerURLIsOAuth = ControllerURLRoot + "isOauth";

    /*
     *  Inventory
     */
    public static readonly string ControllerURLAPI = ControllerURLRoot + "api";
    public static readonly string ControllerURLItems = ControllerURLRoot + "items";
    public static readonly string ControllerURLPlayer = ControllerURLRoot + "avatar";

    /*
     *  Request parameters.
     */
    public static readonly string LoginNameParam = "avatar_name";
    public static readonly string TokenParam = "token";
    
    /*
     *  Keys for parsing
     */
    public static readonly string SessionKey = "avatari_user_id";
}
