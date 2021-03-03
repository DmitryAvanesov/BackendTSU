<?php // api/auth

require_once('../includes/db.inc.php');

$db = initDB();
$method = $_SERVER['REQUEST_METHOD'];
$std = new stdClass();
$std->api_token = $token;
$headers = getallheaders();
$token = explode(' ', $headers['Authorization'])[1];
$res = $db->query("SELECT user FROM token WHERE token = '$token' AND expiration > now()");

if (!$res) {
  echo "An error occured during the last database request";
  exit;
}

$row = $res->fetch_assoc();

if (count($row) == 1) {
  header('HTTP/1.1 200 OK');
  echo json_encode($std);
} else {
  if ($method != 'POST') {
    header('HTTP/1.1 400 Bad Request');
    echo "Wrong request method";
  } else {
    $data = json_decode(file_get_contents('php://input'));
    $username = $data->username;
    $password = md5($data->password);
    $res = $db->query("SELECT id FROM user WHERE username = '$username' AND password = '$password'");

    if (!$res) {
      echo "An error occured during the last database request";
      exit;
    }

    $row = $res->fetch_assoc();

    if (count($row) != 1) {
      header('HTTP/1.1 403 Forbidden');
      echo "Wrong username and/or password";
    } else {
      header('HTTP/1.1 200 OK');
      echo "Welcome!";
    }
  }
}
