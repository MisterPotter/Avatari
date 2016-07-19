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

      if (!$request->get('avatari_user_id')) {
        $response->setData(array(
          'status' => 503,
          'data' => 'not logged in'
        ));
      } else {
        $account_id = $request->get('avatari_user_id');
        $account = $this->getDoctrine()->getRepository('AppBundle:Account')->findById($account_id);

        $response->setData(array(
            'status' => 200,
            'data' => $account
        ));
      }

      return $response;
    }

    /**
     * @Route("/account/logout", name="logout")
     */
    public function logoutAction(Request $request)
    {
      $session = new Session();
      $response = new JsonResponse();

      $session->invalidate();

      $response->setData(array(
          'status' => 200,
          'data' => 'logged out'
      ));

      return $response;
    }

    /**
     * @Route("/account/create", name="account_create")
     */
    public function createAction(Request $request)
    {
      $em = $this->getDoctrine()->getManager();
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

        $tari = $em->getRepository('AppBundle:Tari')->findOneById(1);
        $area = $em->getRepository('AppBundle:Area')->findOneById(1);

        $avatar->setArea($area);
        $avatar->setTari($tari);
        $avatar->setName($request->get('avatar_name'));
        $account->setToken($request->get('token'));
        $fitbit->setAccount($account);
        $avatar->setAccount($account);

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
      $session = new Session();
      $em = $this->getDoctrine()->getManager();
      $errors = [];
      $response = new JsonResponse();

      if ($request->get('avatari_user_id')){
        $errors[] = 'You are already logged in';
      }

      if (!$token = $request->get('token', false)){
        $errors[] = 'Token field must be set';
      }

      $account = $em->getRepository("AppBundle:Account")->findOneByToken($token);

      if(!$account){
        $errors[] = 'Token does not match any existing Accounts';
      }

      if (count($errors) != 0) {
        $response->setData(array(
          'status' => 503,
          'data' => $errors
        ));
      } else {
        $session->set('avatari_user_id', $account->getId());
        $response->setData(array(
          'status' => 200,
          'data' => $account->getId()
        ));
      }

      return $response;
    }

}
