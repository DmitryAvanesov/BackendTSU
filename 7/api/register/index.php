<?php // api/register

require_once('../includes/db.inc.php');

$db = initDB();
$method = $_SERVER['REQUEST_METHOD'];

if ($method != 'POST') {
  header('HTTP/1.1 400 Bad Request');
  echo "Wrong request method";
} else {
  $data = json_decode(file_get_contents('php://input'));
  $username = $data->username;
  $res = $db->query("SELECT id FROM user WHERE username = '$username'");

  if (!$res) {
    echo "An error occured during the last database request";
    exit;
  }

  $row = $res->fetch_assoc();

  if (count($row) > 0) {
    header('HTTP/1.1 400 Bad Request');
    echo "Username is already used";
  } else {
    $password = md5($data->password);
    $res = $db->query("INSERT INTO user(username, password) VALUES ('$username', '$password')");

    if (!$res) {
      header('HTTP/1.1 500 Internal Server Error');
      echo $db->error;
    } else {
      header('HTTP/1.1 200 OK');
      $std = new stdClass();
      $std->api_token = "themostsecuretokenintheworld";
      echo json_encode($std);
    }
  }
}

closeDB($db);
