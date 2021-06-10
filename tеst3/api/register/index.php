<?php // api/register

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
  $res = $db->query("SELECT id FROM user WHERE login = '$login'");

  if (!$res) {
    echo "An error has occured during the last database request";
    exit;
  }

  $row = $res->fetch_assoc();

  if ($row) {
    header('HTTP/1.0 401 Unauthorized');
    echo "Username is already used";
  } else {
    $password = md5($data->password);
    $role = 'regular';
    $res = $db->query("INSERT INTO user(login, password, role) VALUES ('$login', '$password', '$role')");

    if (!$res) {
      header('HTTP/1.0 401 Unauthorized');
      echo $db->error;
    } else {
      header('HTTP/1.1 200 OK');
      $token = uniqid();
      $res = $db->query("SELECT id FROM user WHERE login = '$login' AND password = '$password'");

      if (!$res) {
        echo "An error has occured during the last database request";
        exit;
      }
    
      $row = $res->fetch_assoc();
      $id = $row['id'];
      $res = $db->query("INSERT INTO token(token, user, expiration) VALUES ('$token', '$id', NOW() + INTERVAL 1 HOUR)");
      $std->api_token = $token;
      echo json_encode($std);
    }
  }
}

closeDB($db);
