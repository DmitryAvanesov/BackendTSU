<?php

function initDB()
{
  $link = mysqli_connect("fdb19.awardspace.net", "2597028_backendtest3", "PqmcW4JxbLY67X2", "2597028_backendtest3");

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
