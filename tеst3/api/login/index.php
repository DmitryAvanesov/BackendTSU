<?php // api/login

require_once('../includes/db.inc.php');

$db = initDB();
$method = $_SERVER['REQUEST_METHOD'];
$std = new stdClass();

if ($method != 'POST') {
  header('HTTP/1.1 400 Bad Request');
  echo "Wrong request method";
} else {
  $data = json_decode(file_get_contents('php://input'));
  $login = $data->login;
  $password = md5($data->password);
  $res = $db->query("SELECT id FROM user WHERE login = '$login' AND password = '$password'");

  if (!$res) {
    echo "An error has occured during the last database request";
    exit;
  }

  $row = $res->fetch_assoc();

  if (!$row) {
    header('HTTP/1.0 401 Unauthorized');
    echo "Wrong username and/or password";
  } else {
    header('HTTP/1.1 200 OK');
    $id = $row['id'];
    $res = $db->query("SELECT token FROM token WHERE user = '$id' AND expiration > NOW()");

    if (!$res) {
      echo "An error occured during the last database request";
      exit;
    }
    
    $row = $res->fetch_assoc();

    if (!$row) {
      $token = uniqid();
      $res = $db->query("INSERT INTO token(token, user, expiration) VALUES ('$token', '$id', NOW() + INTERVAL 1 HOUR)");
    }
    else {
      $token = $row['token'];
    }
  
    $std->api_token = $token;
    echo json_encode($std);
  }
}
