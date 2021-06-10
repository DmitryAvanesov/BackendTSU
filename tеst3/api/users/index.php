<?php // api/logout

require_once('../includes/db.inc.php');

$db = initDB();
$method = $_SERVER['REQUEST_METHOD'];
$std = new stdClass();
$headers = getallheaders();
$token = explode(' ', $headers['Authorization'])[1];

if ($method == 'GET') {
  $res = $db->query("SELECT user FROM token WHERE token = '$token' AND expiration > NOW()");

  if (!$res) {
    echo "An error occured during the last database request";
    exit;
  }

  $row = $res->fetch_assoc();

  if (!$row) {
    header('HTTP/1.0 403 Access Denied');
    echo "You aren't logged in";
  } else {
    header('HTTP/1.1 200 OK');
    $user = $row['user'];
    $res = $db->query("SELECT id, login, role FROM user");

    if (!$res) {
      echo "An error occured during the last database request";
      exit;
    }

    while ($row = $res->fetch_assoc()) {
      echo 'id: ' . $row['id'] . ', login: ' . $row['login'] . ', role: ' . $row['role'] . "\n";
    }
  }
} else if ($method == 'POST') {
  $res = $db->query("SELECT user FROM token WHERE token = '$token' AND expiration > NOW()");

  if (!$res) {
    echo "An error occured during the last database request";
    exit;
  }

  $row = $res->fetch_assoc();
  $id = $row['user'];
  $res = $db->query("SELECT role FROM user WHERE id = '$id'");

  if (!$res) {
    echo "An error occured during the last database request";
    exit;
  }

  $row = $res->fetch_assoc();
  $role = $row['role'];

  if ($role != 'admin') {
    header('HTTP/1.0 403 Access Denied');
    echo "You are not admin";
  } else {
    $data = json_decode(file_get_contents('php://input'));
    $login = $data->login;
    $res = $db->query("SELECT login FROM user WHERE login = '$login'");

    if (!$res) {
      echo "An error occured during the last database request";
      exit;
    }

    $row = $res->fetch_assoc();

    if ($row) {
      header('HTTP/1.0 403 Access Denied');
      echo "This login is already in use";
    } else {
      header('HTTP/1.1 200 OK');
      $password = md5($data->password);
      $role = $data->role;
      $res = $db->query("INSERT INTO user(login, password, role) VALUES ('$login', '$password', '$role')");

      if (!$res) {
        echo "An error occured during the last database request";
        exit;
      }

      echo "User has been added";
    }
  }
} else {
  header('HTTP/1.1 400 Bad Request');
  echo "Wrong request method";
}
