<?php

// web/index.php
require_once __DIR__.'/../vendor/autoload.php';

//Use statements
use Symfony\Component\HttpFoundation\Response;
use Symfony\Component\HttpFoundation\Request;

//App config
$app = new Silex\Application();
$app['debug'] = true;

//Routes
$app->get('/', function (){
    $output = "hello world";
    return $output;
});

$app->get('/test', function (){
    $output = "test";
    return $output;
});

//Error Handling
$app->error(function (\Exception $e, Request $request, $code) {
    switch ($code) {
        case 404:
            $message = 'The requested page could not be found.';
            break;
        default:
            $message = 'We are sorry, but something went terribly wrong.';
    }

    return new Response($message);
});

$app->run();

?>
