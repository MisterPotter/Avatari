<?php

namespace AppBundle\Controller;

use Sensio\Bundle\FrameworkExtraBundle\Configuration\Route;
use Symfony\Bundle\FrameworkBundle\Controller\Controller;
use Symfony\Component\HttpFoundation\Request;
use Symfony\Component\HttpFoundation\JsonResponse;
use Symfony\Component\HttpFoundation\Session\Session;
use AppBundle\Entity\Account;
use AppBundle\Entity\Fitbit;
use AppBundle\Entity\Avatar;



class AccountController extends Controller
{
    /**
     * @Route("/account", name="account")
     */
    public function indexAction(Request $request)
    {
      $session = new Session();
      $response = new JsonResponse();

      if (!$session->get('user_id')) {
        $response->setData(array(
          'status' => 503,
          'data' => 'not logged in'
        ));
      } else {
        $account_id = $session->get('user_id');
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
      $errors = [];
      $response = new JsonResponse();

      if (!$request->get('token', false)){
        $errors[] = 'Token field must be set';
      }

      if (!$request->get('avatar_name', false)){
        $errors[] = 'Avatar Name field must be set';
      }

      if (count($errors) != 0) {
        $response->setData(array(
          'status' => 503,
          'data' => $errors
        ));
      } else {
        $account = new Account();
        $fitbit = new Fitbit();
        $avatar = new Avatar();
        $avatar->setName($request->get('avatar_name'));
        $account->setToken($request->get('token'));
        $account->setFitbit($fitbit);
        $account->setAvatar($avatar);

        $em = $this->getDoctrine()->getManager();
        $em->persist($account);
        $em->persist($fitbit);
        $em->persist($avatar);
        $em->flush();
        $response->setData(array(
          'status' => 200,
          'data' => $account
        ));
      }

      return $response;
    }

    /**
     * @Route("/account/login", name="account_login")
     */
    public function loginAction(Request $request)
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
