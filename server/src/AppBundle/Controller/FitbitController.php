<?php

namespace AppBundle\Controller;

use Sensio\Bundle\FrameworkExtraBundle\Configuration\Route;
use Symfony\Bundle\FrameworkBundle\Controller\Controller;
use Symfony\Component\HttpFoundation\Request;
use Symfony\Component\HttpFoundation\JsonResponse;
use djchen\OAuth2\Client\Provider\Fitbit;

class FitbitController extends Controller
{
    /**
     * @Route("/oauth", name="oauth")
     */
    public function indexAction(Request $request)
    {
      return self::authCode();
    }

    public function authCode(){
      $provider = new Fitbit([
          'clientId'          => '227T6K',
          'clientSecret'      => '538c707fa361dbfcb1b63c7ea24b27ee',
          'redirectUri'       => 'http://localhost/app_dev.php/oauth'
      ]);

      // If we don't have an authorization code then get one
      if (!isset($_GET['code'])) {

          // Fetch the authorization URL from the provider; this returns the
          // urlAuthorize option and generates and applies any necessary parameters
          // (e.g. state).
          $authorizationUrl = $provider->getAuthorizationUrl();

          // Get the state generated for you and store it to the session.
          $_SESSION['oauth2state'] = $provider->getState();

          // Redirect the user to the authorization URL.
          header('Location: ' . $authorizationUrl);
          exit;

      // Check given state against previously stored one to mitigate CSRF attack
      } elseif (empty($_GET['state']) || ($_GET['state'] !== $_SESSION['oauth2state'])) {
          unset($_SESSION['oauth2state']);
          exit('Invalid state');

      } else {

          try {

              // Try to get an access token using the authorization code grant.
              $accessToken = $provider->getAccessToken('authorization_code', [
                  'code' => $_GET['code']
              ]);

              // We have an access token, which we may use in authenticated
              // requests against the service provider's API.
              echo $accessToken->getToken() . "\n";
              echo $accessToken->getRefreshToken() . "\n";
              echo $accessToken->getExpires() . "\n";
              echo ($accessToken->hasExpired() ? 'expired' : 'not expired') . "\n";

              // Using the access token, we may look up details about the
              // resource owner.
              $resourceOwner = $provider->getResourceOwner($accessToken);

              var_export($resourceOwner->toArray());

              // The provider provides a way to get an authenticated API request for
              // the service, using the access token; it returns an object conforming
              // to Psr\Http\Message\RequestInterface.
              $request = $provider->getAuthenticatedRequest(
                  'GET',
                  Fitbit::BASE_FITBIT_API_URL . '/1/user/-/profile.json',
                  $accessToken,
                  ['headers' => ['Accept-Language' => 'en_US'], ['Accept-Locale' => 'en_US']]
                  // Fitbit uses the Accept-Language for setting the unit system used
                  // and setting Accept-Locale will return a translated response if available.
                  // https://dev.fitbit.com/docs/basics/#localization
              );
              // Make the authenticated API request and get the response.
              $data = $provider->getResponse($request);
              $response = new JsonResponse();
              $response->setData(array(
                'status' => 200,
                'data' => $data
              ));
              return $response;
          } catch (\League\OAuth2\Client\Provider\Exception\IdentityProviderException $e) {

              // Failed to get the access token or user details.
              exit($e->getMessage());
          }

      }
    }

    public function refresh(){
      $provider = new djchen\OAuth2\Client\Provider\Fitbit([
          'clientId'          => '227T6K',
          'clientSecret'      => '538c707fa361dbfcb1b63c7ea24b27ee',
          'redirectUri'       => 'http://localhost/app_dev.php/oauth'
      ]);

      $existingAccessToken = getAccessTokenFromYourDataStore();

      if ($existingAccessToken->hasExpired()) {
          $newAccessToken = $provider->getAccessToken('refresh_token', [
              'refresh_token' => $existingAccessToken->getRefreshToken()
          ]);

          // Purge old access token and store new access token to your data store.
      }
    }
}
