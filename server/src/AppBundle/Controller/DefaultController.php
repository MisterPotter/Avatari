<?php

namespace AppBundle\Controller;

use Sensio\Bundle\FrameworkExtraBundle\Configuration\Route;
use Symfony\Bundle\FrameworkBundle\Controller\Controller;
use Symfony\Component\HttpFoundation\Request;
use Symfony\Component\HttpFoundation\JsonResponse;


class DefaultController extends Controller
{
    /**
     * @Route("/", name="homepage")
     */
    public function indexAction(Request $request)
    {
      $response = new JsonResponse();
      $response->setData(array(
        'status' => 200,
        'data' => 'api is live'
      ));
      return $response;
    }

    /**
     * @Route("/db", name="database")
     */
    public function databaseAction(Request $request)
    {
        $conn = $this->container->get('database_connection');
        $sql = 'SHOW TABLES';
        $test = $conn->query($sql);

        $data = $test->fetchAll();

        $response = new JsonResponse();
        $response->setData(array(
          'status' => 200,
          'data' => $data
        ));
        return $response;
    }

    /**
     * @Route("/items", name="items")
     */
    public function itemsAction(Request $request)
    {
      $response = new JsonResponse();
      $items = $this->getDoctrine()->getRepository('AppBundle:Item')->findAll();
      $itemsData = array();
      foreach ($items as $item) {
        $itemsData[] = [
          'id' => $item->getId(),
          'name' => $item->getName(),
          'description' => $item->getDescription(),
          'type' => $item->getType(),
          'rarity' => $item->getRarity()
        ];
      }

      $data['items'] = $itemsData;
      $response->setData(array(
          'status' => 200,
          'data' => $data
      ));
      return $response;
    }

    /**
     * @Route("/avatar", name="avatar")
     */
    public function avatarAction(Request $request)
    {
      $response = new JsonResponse();
      /*
      if (!$this->get('security.authorization_checker')->isGranted('IS_AUTHENTICATED_FULLY')) {
        $response->setData(array(
          'status' => 503,
          'data' => 'not logged in'
        ));
      } else {
      */
      if(true){
        $account_id = 1;
        $account = $this->getDoctrine()->getRepository('AppBundle:Account')->findOneById($account_id);
        $avatar = $account->getAvatar();

        $items = $avatar->getItems();
        $itemsData = array();
        foreach ($items as $item) {
          $itemsData[] = [
            'id' => $item->getId(),
            'name' => $item->getName(),
            'description' => $item->getDescription(),
            'type' => $item->getType(),
            'rarity' => $item->getRarity()
          ];
        }

        $goals = $avatar->getGoals();
        $goalsData = array();
        foreach ($goals as $goal) {
          $goalsData[] = [
            'name' => $goal->getName(),
          ];
        }

        $data['avatar'] = [
          'name'    => $avatar->getName(),
          'level'   => $avatar->getLevel(),
          'health'  => [
                        'health_max' => $avatar->getHealthMax(),
                        'health_current' => $avatar->getHealthCurrent()
                      ],
          'exp'  => [
                        'exp_max' => $avatar->getExpMax(),
                        'exp_current' => $avatar->getExpCurrent()
                      ],
          'stats' => [
                        'strength' => $avatar->getStrengthBase(),
                        'agility' => $avatar->getAgilityBase(),
                        'defence' => $avatar->getDefenceBase(),
                      ],
          'items' => $itemsData,
          'goals' => $goalsData
        ];

        $response->setData(array(
            'status' => 200,
            'data' => $data
        ));
      }
      return $response;
    }
}
