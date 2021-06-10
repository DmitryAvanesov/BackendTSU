-- phpMyAdmin SQL Dump
-- version 4.6.4
-- https://www.phpmyadmin.net/
--
-- Хост: fdb19.awardspace.net
-- Время создания: Июл 07 2020 г., 08:45
-- Версия сервера: 5.7.20-log
-- Версия PHP: 5.5.38

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- База данных: `2597028_backendtest3`
--

-- --------------------------------------------------------

--
-- Структура таблицы `token`
--

CREATE TABLE `token` (
  `token` varchar(100) NOT NULL,
  `user` int(11) NOT NULL,
  `expiration` datetime NOT NULL
) ENGINE=MyISAM DEFAULT CHARSET=latin1;

--
-- Дамп данных таблицы `token`
--

INSERT INTO `token` (`token`, `user`, `expiration`) VALUES
('5f012ee34bb8c', 7, '2020-07-16 00:00:00'),
('5f042e130ab8b', 4, '2020-07-07 09:10:59'),
('5f042de34bb8b', 3, '2020-07-07 09:10:11');

-- --------------------------------------------------------

--
-- Структура таблицы `user`
--

CREATE TABLE `user` (
  `id` int(11) NOT NULL,
  `login` varchar(50) NOT NULL,
  `password` varchar(100) NOT NULL,
  `role` varchar(7) NOT NULL
) ENGINE=MyISAM DEFAULT CHARSET=latin1;

--
-- Дамп данных таблицы `user`
--

INSERT INTO `user` (`id`, `login`, `password`, `role`) VALUES
(3, 'User', 'ee11cbb19052e40b07aac0ca060c23ee', 'regular'),
(4, 'UserToo', 'f99ad17c14a07f2d4c84a767237796da', 'regular'),
(6, 'UserAdded', '1cae9307b32d7926afa7d4cd807e9103', 'regular'),
(7, 'AdminNew', 'd3698036132b78ae31c3f03d377758d8', 'admin');

--
-- Индексы сохранённых таблиц
--

--
-- Индексы таблицы `token`
--
ALTER TABLE `token`
  ADD PRIMARY KEY (`token`);

--
-- Индексы таблицы `user`
--
ALTER TABLE `user`
  ADD PRIMARY KEY (`id`);

--
-- AUTO_INCREMENT для сохранённых таблиц
--

--
-- AUTO_INCREMENT для таблицы `user`
--
ALTER TABLE `user`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=8;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
