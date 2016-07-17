using UnityEngine;

public class Config {
    /**
     *  Authentication
     */
    public static readonly string Token = "839uejdmi12ke9012ik1dpakpkdj30e2";
    public static readonly string ControllerURLRoot = "http://ec2-54-200-193-115.us-west-2.compute.amazonaws.com/app_dev.php/";
    public static readonly string ControllerURLAccounts = ControllerURLRoot + "account/create";
    public static readonly string ControllerURLLogin = ControllerURLRoot + "account/login";
    public static readonly string ControllerURLOAuth = ControllerURLRoot + "oauth?token=" + Token;
    public static readonly string LoginNameParam = "avatar_name";
    public static readonly string TokenParam = "token";
}
