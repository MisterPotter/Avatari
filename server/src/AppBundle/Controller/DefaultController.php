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
     * @Route("/taris", name="taris")
     */
    public function tarisAction(Request $request)
    {
      $response = new JsonResponse();
      $taris = $this->getDoctrine()->getRepository('AppBundle:Tari')->findAll();
      $tarisData = array();
      foreach ($taris as $tari) {
        $tarisData[] = [
          'id' => $tari->getId(),
          'name' => $tari->getName(),
          'name' => $tari->getSpriteName(),
          'description' => $tari->getDescription(),
        ];
      }

      $data['taris'] = $tarisData;
      $response->setData(array(
          'status' => 200,
          'data' => $data
      ));
      return $response;
    }

    /**
     * @Route("/areas", name="areas")
     */
    public function areasAction(Request $request)
    {
      $response = new JsonResponse();
      $areas = $this->getDoctrine()->getRepository('AppBundle:Area')->findAll();
      $areasData = array();
      foreach ($areas as $area) {
        $areasData[] = [
          'id' => $area->getId(),
          'name' => $area->getName(),
          'name' => $area->getSpriteName(),
          'description' => $area->getDescription(),
        ];
      }

      $data['areas'] = $areasData;
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

        $tari = $avatar->getTari();
        $tariData = [
          'id' => $tari->getId(),
          'name' => $tari->getName(),
          'spriteName' => $tari->getSpriteName(),
          'description' => $tari->getDescription(),
        ];

        $area = $avatar->getArea();
        $areaData = [
          'id' => $area->getId(),
          'name' => $area->getName(),
          'spriteName' => $area->getSpriteName(),
          'description' => $area->getDescription(),
        ];

        $data['avatar'] = [
          'name'    => $avatar->getName(),
          'level'   => $avatar->getLevel(),
          'tari'    => $tariData,
          'area'    => $areaData,
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
      $error = [];
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
      if($tari_id = $request->get('tari', false)){
        if($tari = $em->getRepository('AppBundle:Tari')->findOneById($tari_id)){
          $avatar->setTari($tari);
          $set[] = 'tari';
        }else{
          $error[] = "Tari with id ".$tari_id." does not exist";
        }
      }
      if($area_id = $request->get('area', false)){
        if($area = $em->getRepository('AppBundle:Area')->findOneById($area_id)){
          $avatar->setArea($area);
          $set[] = 'area';
        }else{
          $error[] = "Area with id ".$area_id." does not exist";
        }
      }
      if (count($error) == 0){
        $response->setData(array(
            'status' => 200,
            'data' => $set
        ));
        $em->persist($avatar);
        $em->flush();
      } else {
        $response->setData(array(
            'status' => 503,
            'data' => $error
        ));
      }


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
