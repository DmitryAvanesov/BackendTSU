<?php

function initDB()
{
  $link = mysqli_connect("fdb19.awardspace.net", "2597028_backend7", "xGqYBnpLu6FX5ne", "2597028_backend7");

  if (!$link) {
    echo "Ошибка: Невозможно установить соединение с MySQL." . PHP_EOL;
    echo "Код ошибки errno: " . mysqli_connect_errno() . PHP_EOL;
    echo "Текст ошибки error: " . mysqli_connect_error() . PHP_EOL;
    exit;
  }

  return $link;
}

function closeDB($link) {
  mysqli_close($link);
}
