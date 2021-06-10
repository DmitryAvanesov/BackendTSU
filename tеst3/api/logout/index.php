<?php // api/logout

require_once('../includes/db.inc.php');

$db = initDB();
$method = $_SERVER['REQUEST_METHOD'];
$std = new stdClass();

if ($method != 'POST') {
  header('HTTP/1.1 400 Bad Request');
  echo "Wrong request method";
} else {
  $headers = getallheaders();
  $token = explode(' ', $headers['Authorization'])[1];
  $res = $db->query("SELECT token FROM token WHERE token = '$token'");

  if (!$res) {
    echo "An error occured during the last database request";
    exit;
  }

  $row = $res->fetch_assoc();

  if (!$row) {
    header('HTTP/1.1 500 Internal Server Error');
    echo "You aren't logged in";
  }
  else {
    header('HTTP/1.1 200 OK');
    $res = $db->query("DELETE FROM token WHERE token = '$token'");

    if (!$res) {
      echo "An error occured during the last database request";
      exit;
    }

    echo "Logged out";
  }
}