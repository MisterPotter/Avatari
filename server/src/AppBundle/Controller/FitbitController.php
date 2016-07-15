<?php

namespace AppBundle\Controller;

use Sensio\Bundle\FrameworkExtraBundle\Configuration\Route;
use Symfony\Bundle\FrameworkBundle\Controller\Controller;
use Symfony\Component\HttpFoundation\Request;
use Symfony\Component\HttpFoundation\JsonResponse;
use djchen\OAuth2\Client\Provider\Fitbit;
use Symfony\Component\HttpFoundation\Session\Session;

class FitbitController extends Controller
{
    /**
     * @Route("/oauth", name="oauth")
     */
    public function indexAction(Request $request)
    {
      $em = $this->getDoctrine()->getManager();
      $session = new Session();
      $account_id = $session->get('user_id');

      $account = $em->getRepository('AppBundle:Account')->findOneById($account_id);
      $fitbit = $account->getFitbit();
      $provider = new Fitbit([
          'clientId'          => '227T6K',
          'clientSecret'      => '538c707fa361dbfcb1b63c7ea24b27ee',
          'redirectUri'       => 'http://ec2-54-200-193-115.us-west-2.compute.amazonaws.com/app_dev.php/oauth'
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
              $accessToken = $provider->getAccessToken('authorization_code', [
                  'code' => $_GET['code']
              ]);
              $request = $provider->getAuthenticatedRequest(
                  'GET',
                  Fitbit::BASE_FITBIT_API_URL . '/1/user/-/activities/steps/date/today/1m.json',
                  $accessToken,
                  ['headers' => ['Accept-Language' => 'en_US'], ['Accept-Locale' => 'en_US']]
              );
              $data = $provider->getResponse($request);
              $fitbit->setToken($accessToken->getToken());
              $em->persist($fitbit);
              $em->flush();
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

    /**
     * @Route("/api", name="api")
     */
    public function apiAction(Request $request){
      $em = $this->getDoctrine()->getManager();
      $session = new Session();
      $account_id = $session->get('user_id');

      $account = $em->getRepository('AppBundle:Account')->findOneById($account_id);
      $fitbit = $account->getFitbit();
      $provider = new Fitbit([
          'clientId'          => '227T6K',
          'clientSecret'      => '538c707fa361dbfcb1b63c7ea24b27ee',
          'redirectUri'       => 'http://ec2-54-200-193-115.us-west-2.compute.amazonaws.com/app_dev.php/oauth'
      ]);

      $accessToken = $fitbit->getToken();

      $request = $provider->getAuthenticatedRequest(
          'GET',
          Fitbit::BASE_FITBIT_API_URL . '/1/user/-/profile.json',
          $accessToken,
          ['headers' => ['Accept-Language' => 'en_US'], ['Accept-Locale' => 'en_US']]
      );
      $data = $provider->getResponse($request);
        $response = new JsonResponse();
        $response->setData(array(
          'status' => 200,
          'data' => $data
        ));
        return $response;

    }

}
