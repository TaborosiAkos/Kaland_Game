-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Gép: 127.0.0.1
-- Létrehozás ideje: 2025. Jan 29. 10:56
-- Kiszolgáló verziója: 10.4.28-MariaDB
-- PHP verzió: 8.2.4

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Adatbázis: `angol_gyakorlatok`
--
CREATE DATABASE IF NOT EXISTS `angol_gyakorlatok` DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci;
USE `angol_gyakorlatok`;

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `feladatok`
--

CREATE TABLE `feladatok` (
  `id` int(11) NOT NULL,
  `kerdes` varchar(255) DEFAULT NULL,
  `valasz` varchar(50) DEFAULT NULL,
  `tipus` varchar(50) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- A tábla adatainak kiíratása `feladatok`
--

INSERT INTO `feladatok` (`id`, `kerdes`, `valasz`, `tipus`) VALUES
(1, 'My … is John Doe', 'name', 'beginner_beiros'),
(2, 'I am 8 … old', 'years', 'beginner_beiros'),
(3, 'I … an apple', 'have', 'beginner_beiros'),
(4, 'I … 2 sisters', 'have', 'beginner_beiros'),
(5, 'My dad … tall', 'is', 'beginner_beiros'),
(6, 'The weather is really nice … .', 'today', 'intermediate_beiros'),
(7, 'I didn’t … the question', 'understand', 'intermediate_beiros'),
(8, 'He’s very good at … the guitar', 'playing', 'intermediate_beiros'),
(9, 'I enjoy playing football with my … .', 'friends', 'intermediate_beiros'),
(10, 'She bought a new book … .', 'buy', 'intermediate_beiros'),
(11, 'The manager was impressed by how quickly he … the problem', 'solved', 'polyglot_master_beiros'),
(12, 'If she followed the instructions carefully, she wouldn’t have made the … .', 'mistake', 'polyglot_master_beiros'),
(13, 'We were surprised when they … to the news so calmly', 'reacted', 'polyglot_master_beiros'),
(14, 'She … to the meeting as soon as she could', 'arrived', 'polyglot_master_beiros'),
(15, 'She didn’t … the opportunity to travel abroad', 'miss', 'polyglot_master_beiros');

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `kepek`
--

CREATE TABLE `kepek` (
  `id` int(11) NOT NULL,
  `kep_nev` varchar(50) DEFAULT NULL,
  `leiras` varchar(50) DEFAULT NULL,
  `tipus` varchar(50) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- A tábla adatainak kiíratása `kepek`
--

INSERT INTO `kepek` (`id`, `kep_nev`, `leiras`, `tipus`) VALUES
(1, 'apple.png', 'apple', 'beginner_kep'),
(2, 'mother.png', 'mom', 'beginner_kep'),
(3, 'dad.png', 'dad', 'beginner_kep'),
(4, 'child.png', 'child', 'beginner_kep'),
(5, 'dog.png', 'dog', 'beginner_kep'),
(6, 'friends.png', 'friends', 'intermediate_kep'),
(7, 'buy.png', 'buy', 'intermediate_kep'),
(8, 'play.png', 'play', 'intermediate_kep'),
(9, 'weather.png', 'weather', 'intermediate_kep'),
(10, 'book.png', 'book', 'intermediate_kep'),
(11, 'coding.png', 'coding', 'polyglot_master_kep'),
(12, 'agriculture.png', 'agriculture', 'polyglot_master_kep'),
(13, 'procrastinate.png', 'procrastinate', 'polyglot_master_kep'),
(14, 'gather.png', 'gather', 'polyglot_master_kep'),
(15, 'lament.png', 'lament', 'polyglot_master_kep');

--
-- Indexek a kiírt táblákhoz
--

--
-- A tábla indexei `feladatok`
--
ALTER TABLE `feladatok`
  ADD PRIMARY KEY (`id`);

--
-- A tábla indexei `kepek`
--
ALTER TABLE `kepek`
  ADD PRIMARY KEY (`id`);

--
-- A kiírt táblák AUTO_INCREMENT értéke
--

--
-- AUTO_INCREMENT a táblához `feladatok`
--
ALTER TABLE `feladatok`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=16;

--
-- AUTO_INCREMENT a táblához `kepek`
--
ALTER TABLE `kepek`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=16;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
