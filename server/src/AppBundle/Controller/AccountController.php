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
      $account_id = $this->getUser()->getId();
      $account = $this->getDoctrine()->getRepository('AppBundle:Account')->findById($account_id);

      $response = new JsonResponse();

      if (!$account_id) {
        $response->setData(array(
          'status' => 503,
          'data' => 'not logged in'
        ));
      } elseif(!$account) {
        $response->setData(array(
          'status' => 404,
          'data' => 'account does not exist'
        ));
      } else {
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
      $session = new Session();

      $account = new Account();

      $account = $this->getDoctrine()->getRepository('AppBundle:Account')->findById($account_id);

      $response = new JsonResponse();

      if (!$account_id) {
        $response->setData(array(
          'status' => 503,
          'data' => 'not logged in'
        ));
      } elseif(!$account) {
        $response->setData(array(
          'status' => 404,
          'data' => 'account does not exist'
        ));
      } else {
        $response->setData(array(
          'status' => 200,
          'data' => $account
        ));
      }

      return $response;
    }

}
