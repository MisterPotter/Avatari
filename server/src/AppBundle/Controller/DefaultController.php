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
        $account_id = $request->get('avatari_user_id');

        $account = $this->getDoctrine()->getRepository('AppBundle:Account')->findOneById($account_id);
        $avatar = $account->getAvatar();

        $items = $avatar->getItems();
        $itemsData = array();
        foreach ($items as $item) {
          if($item == $avatar->getHead() || $item == $avatar->getBody() || $item == $avatar->getFeet() || $item == $avatar->getHands() || $item == $avatar->getWings() || $item == $avatar->getNeck()){
            $isEquipt = True;
          } else {
            $isEquipt = False;
          }
          $itemsData[] = [
            'id' => $item->getId(),
            'name' => $item->getName(),
            'description' => $item->getDescription(),
            'type' => $item->getType(),
            'rarity' => $item->getRarity(),
            'isEquipt' => $isEquipt
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
          'items' => $itemsData

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
      $account_id = $request->get('avatari_user_id');

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


    /**
     * @Route("/avatar/item/give", name="avatar_give_item")
     */
    public function avatarGiveItemAction(Request $request)
    {
      $session = new Session();
      $response = new JsonResponse();
      $em = $this->getDoctrine()->getManager();
      $account_id = $request->get('avatari_user_id');

      $account = $em->getRepository('AppBundle:Account')->findOneById($account_id);
      if(!$account){
        $response->setData(array(
            'status' => 503,
            'data' => 'Not Logged In'
        ));
        return $response;
      }

      if($item_id = $request->get('item_id',false)){
        $avatar = $account->getAvatar();

        $item = $em->getRepository('AppBundle:Item')->findOneById($item_id);
        if(!$item){
          $response->setData(array(
              'status' => 503,
              'data' => 'Item does not exist'
          ));
        } else {
          $response->setData(array(
              'status' => 200,
              'data' => 'Item added'
          ));
          $avatar->addItem($item);
          $item->addAvatar($avatar);
          $em->persist($item);
          $em->persist($avatar);
          $em->flush();
        }
      } else {
        $response->setData(array(
            'status' => 503,
            'data' => 'item_id must be set'
        ));
      }

      return $response;
    }

    /**
     * @Route("/avatar/item/equip", name="avatar_equip_item")
     */
    public function avatarEquipItemAction(Request $request)
    {
      $session = new Session();
      $response = new JsonResponse();
      $em = $this->getDoctrine()->getManager();
      $account_id = $request->get('avatari_user_id');

      $account = $em->getRepository('AppBundle:Account')->findOneById($account_id);
      if(!$account){
        $response->setData(array(
            'status' => 503,
            'data' => 'Not Logged In'
        ));
        return $response;
      }

      if($item_id = $request->get('item_id',false)){

        $avatar = $account->getAvatar();

        $item = $em->getRepository('AppBundle:Item')->findOneById($item_id);
        if(!$item){
          $response->setData(array(
              'status' => 503,
              'data' => 'Item does not exist'
          ));
        } else {
          $response->setData(array(
              'status' => 200,
              'data' => 'Item equipt'
          ));
          if ($location = $request->get('location', false)){
            switch ($location) {
              case 'head':
                $avatar->setHead($item);
                break;
              case 'body':
                $avatar->setBody($item);
                break;
              case 'feet':
                $avatar->setFeet($item);
                break;
              case 'hands':
                $avatar->setHands($item);
                break;
              case 'wings':
                $avatar->setWings($item);
                break;
              case 'neck':
                $avatar->setNeck($item);
                break;

              default:
                $response->setData(array(
                    'status' => 503,
                    'data' => 'Invalid Location'
                ));
                break;
            }
            $avatar->setHead($item);
            $em->persist($avatar);
            $em->flush();
          } else {
            $response->setData(array(
                'status' => 503,
                'data' => 'location must be set'
            ));
          }
        }
      } else {
        $response->setData(array(
            'status' => 503,
            'data' => 'item_id must be set'
        ));
      }

      return $response;
    }
}
