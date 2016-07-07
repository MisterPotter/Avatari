<?php

namespace AppBundle\Controller;

use Sensio\Bundle\FrameworkExtraBundle\Configuration\Route;
use Symfony\Bundle\FrameworkBundle\Controller\Controller;
use Symfony\Component\HttpFoundation\Request;
use Symfony\Component\HttpFoundation\JsonResponse;
use Symfony\Component\HttpFoundation\Session\Session;



class AccountController extends Controller
{
    /**
     * @Route("/account", name="account")
     */
    public function indexAction(Request $request)
    {
      $response = new JsonResponse();

      if (!$this->get('security.authorization_checker')->isGranted('IS_AUTHENTICATED_FULLY')) {
        $response->setData(array(
          'status' => 503,
          'data' => 'not logged in'
        ));
      } else {
        $account_id = $this->getUser()->getId();
        $account = $this->getDoctrine()->getRepository('AppBundle:Account')->findById($account_id);

        $response->setData(array(
            'status' => 200,
            'data' => $account
        ));
      }

      return $response;
    }

    /**
     * @Route("/account/create", name="account_create")
     */
    public function createAction(Request $request)
    {
      $userManager = $this->get('fos_user.user_manager');
      $user = $userManager->createUser();

      $errors = [];
      $response = new JsonResponse();

      if (!$request->get('email', false)){
        $errors[] = 'Email field must be set';
      }

      if (!$request->get('password', false)){
        $errors[] = 'Password field must be set';
      }

      if (count($errors) != 0) {
        $response->setData(array(
          'status' => 503,
          'data' => $errors
        ));
      }

      return $response;
    }

}
