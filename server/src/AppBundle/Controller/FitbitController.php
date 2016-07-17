<?php

namespace AppBundle\Controller;

use Sensio\Bundle\FrameworkExtraBundle\Configuration\Route;
use Symfony\Bundle\FrameworkBundle\Controller\Controller;
use Symfony\Component\HttpFoundation\Request;
use Symfony\Component\HttpFoundation\JsonResponse;
use djchen\OAuth2\Client\Provider\Fitbit;
use Symfony\Component\HttpFoundation\Session\Session;
use Symfony\Component\HttpFoundation\Response;


class FitbitController extends Controller
{
    /**
     * @Route("/oauth", name="oauth")
     */
    public function oauthAction(Request $request)
    {
      $em = $this->getDoctrine()->getManager();
      $session = new Session();
      if(!$session->get('token')){
        $session->set('token', $request->get('token'));
      }

      $account = $em->getRepository('AppBundle:Account')->findOneByToken($session->get('token'));
      if(!$account){
        $response = new JsonResponse();
        $response->setData(array(
          'status' => 503,
          'data' => 'Token does not match'
        ));
        return $response;
      }

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
          $session->set('oauth2state', $provider->getState());

          // Redirect the user to the authorization URL.
          header('Location: ' . $authorizationUrl);
          exit;

      // Check given state against previously stored one to mitigate CSRF attack
    } elseif (empty($_GET['state']) || ($_GET['state'] !== $session->get('oauth2state'))) {
          $session->set('oauth2state', NULL);
          exit('Invalid state');
      } else {
          try {
              $accessToken = $provider->getAccessToken('authorization_code', [
                  'code' => $_GET['code']
              ]);

              $fitbit->setToken($accessToken->getToken());
              $fitbit->setRefreshToken($accessToken->getRefreshToken());
              $fitbit->setExpires($accessToken->getExpires());
              $fitbit->setHasExpired($accessToken->hasExpired());
              // Using the access token, we may look up details about the
              // resource owner.
              $resourceOwner = $provider->getResourceOwner($accessToken);
              $resourceOwner = $resourceOwner->toArray();
              $fitbit->setFitbitId($resourceOwner['encodedId']);

              $em->persist($fitbit);
              $em->flush();

              $response = new Response();
              $response->setContent('<html><body><h1>Authentication Successful</h1><h2>Please Return to Avatari</h2></body></html>');
              $response->setStatusCode(Response::HTTP_OK);

              // set a HTTP response header
              $response->headers->set('Content-Type', 'text/html');

              // print the HTTP headers followed by the content
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
      $account_id = $request->get('avatari_user_id');

      $account = $em->getRepository('AppBundle:Account')->findOneById($account_id);
      if(!$account){
        $response = new JsonResponse();
        $response->setData(array(
          'status' => 503,
          'data' => 'Not Logged In'
        ));
        return $response;
      }

      $fitbit = $account->getFitbit();
      $provider = new Fitbit([
          'clientId'          => '227T6K',
          'clientSecret'      => '538c707fa361dbfcb1b63c7ea24b27ee',
          'redirectUri'       => 'http://ec2-54-200-193-115.us-west-2.compute.amazonaws.com/app_dev.php/oauth'
      ]);

      try {

          $resources = ['calories','steps','distance','minutesSedentary','minutesLightlyActive','minutesFairlyActive','minutesVeryActive','activityCalories'];

          foreach($resources as $resource){
            $request = $provider->getAuthenticatedRequest(
                'GET',
                Fitbit::BASE_FITBIT_API_URL . '/1/user/'.$fitbit->getFitbitId().'/activities/'.$resource.'/date/today/1w.json',
                $fitbit->getToken(),
                ['headers' => ['Accept-Language' => 'en_CA'], ['Accept-Locale' => 'en_CA']]
            );
            $data[$resource] = $provider->getResponse($request);
          }

          $request = $provider->getAuthenticatedRequest(
              'GET',
              Fitbit::BASE_FITBIT_API_URL . '/1/user/'.$fitbit->getFitbitId().'/activities.json',
              $fitbit->getToken(),
              ['headers' => ['Accept-Language' => 'en_CA'], ['Accept-Locale' => 'en_CA']]
          );
          $data["lifetime"] = $provider->getResponse($request);

          $request = $provider->getAuthenticatedRequest(
              'GET',
              Fitbit::BASE_FITBIT_API_URL . '/1/user/'.$fitbit->getFitbitId().'/activities/list.json?offset=0&limit=20&sort=desc&beforeDate='.date("Y-m-d"),
              $fitbit->getToken(),
              ['headers' => ['Accept-Language' => 'en_CA'], ['Accept-Locale' => 'en_CA']]
          );
          $data["recent"] = $provider->getResponse($request);

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

    /**
     * @Route("/refresh", name="refresh")
     */
    public function refreshAction(Request $request){
      $em = $this->getDoctrine()->getManager();
      $session = new Session();
      $account_id = $request->get('avatari_user_id');

      $account = $em->getRepository('AppBundle:Account')->findOneById($account_id);
      if(!$account){
        $response = new JsonResponse();
        $response->setData(array(
          'status' => 503,
          'data' => 'Not Logged In'
        ));
        return $response;
      }

      $fitbit = $account->getFitbit();
      $provider = new Fitbit([
          'clientId'          => '{fitbit-oauth2-client-id}',
          'clientSecret'      => '{fitbit-client-secret}',
          'redirectUri'       => 'https://example.com/callback-url'
      ]);
      $response = new JsonResponse();
      if ($fitbit->hasExpired()) {
          $fitbit->setRefreshToken($provider->getAccessToken('refresh_token', [
              'refresh_token' => $fitbit->getRefreshToken()
          ]));
          $em->persist($fitbit);
          $em->flush();
      }
      $response->setData(array(
        'status' => 200,
        'data' => 'Access token refreshed'
      ));
      return $response;
    }

    /**
     * @Route("/isOauth", name="isOauth")
     */
    public function isOauthAction(Request $request){
      $em = $this->getDoctrine()->getManager();
      $session = new Session();
      $response = new JsonResponse();

      $account_id = $request->get('avatari_user_id');
      $account = $em->getRepository('AppBundle:Account')->findOneById($account_id);

      if(!$account){
        $response->setData(array(
          'status' => 503,
          'data' => 'Not Logged In'
        ));
        return $response;
      }

      $fitbit = $account->getFitbit();
      if($fitbit->getToken() == Null){
        $response->setData(array(
          'status' => 503,
          'data' => 'User does not have oauth'
        ));
      } else {
        $response->setData(array(
          'status' => 200,
          'data' => 'User has oauth'
        ));
      }
      return $response;
    }
}
