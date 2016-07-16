<?php

namespace AppBundle\Controller;

use Sensio\Bundle\FrameworkExtraBundle\Configuration\Route;
use Symfony\Bundle\FrameworkBundle\Controller\Controller;
use Symfony\Component\HttpFoundation\Request;
use Symfony\Component\HttpFoundation\JsonResponse;
use Symfony\Component\HttpFoundation\Session\Session;



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
      $session = new Session();

      $response = new JsonResponse();
      if(true){
        $account_id = $session->get('user_id');

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
                        'defence' => $avatar->getdefenceBase(),
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

    /**
     * @Route("/avatar/set", name="avatar_set")
     */
    public function avatarSetAction(Request $request)
    {
      $session = new Session();
      $response = new JsonResponse();
      $em = $this->getDoctrine()->getManager();
      $account_id = $session->get('user_id');

      $account = $em->getRepository('AppBundle:Account')->findOneById($account_id);
      if(!$account){
        $response->setData(array(
            'status' => 503,
            'data' => 'Not Logged In'
        ));
        return $response;
      }
      $set = [];
      $avatar = $account->getAvatar();
      if($level = $request->get('level', false)){
        $avatar->setLevel((int)$level);
        $set[] = 'level';
      }
      if($exp = $request->get('experience', false)){
        $avatar->setExpCurrent((int)$exp);
        $set[] = 'experience';
      }
      if($health = $request->get('health', false)){
        $avatar->setHealthCurrent((int)$health);
        $set[] = 'health';
      }
      if($strength = $request->get('strength', false)){
        $avatar->setStrengthBase((int)$strength);
        $set[] = 'strength';
      }
      if($agility = $request->get('agility', false)){
        $avatar->setAgilityBase((int)$agility);
        $set[] = 'agility';
      }
      if($defense = $request->get('defense', false)){
        $avatar->setDefenceBasehBase((int)$defense);
        $set[] = 'defense';
      }
      $response->setData(array(
          'status' => 200,
          'data' => $set
      ));
      $em->persist($avatar);
      $em->flush();

      return $response;
    }
}
