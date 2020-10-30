--
-- Скрипт сгенерирован Devart dbForge Studio 2020 for MySQL, Версия 9.0.391.0
-- Домашняя страница продукта: http://www.devart.com/ru/dbforge/mysql/studio
-- Дата скрипта: 28.10.2020 15:13:36
-- Версия сервера: 10.3.25
-- Версия клиента: 4.1
--

-- 
-- Отключение внешних ключей
-- 
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;

-- 
-- Установить режим SQL (SQL mode)
-- 
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;

-- 
-- Установка кодировки, с использованием которой клиент будет посылать запросы на сервер
--
SET NAMES 'utf8';

DROP DATABASE IF EXISTS bufet1131vorobyov;

CREATE DATABASE bufet1131vorobyov
CHARACTER SET utf8mb4
COLLATE utf8mb4_general_ci;

--
-- Установка базы данных по умолчанию
--
USE bufet1131vorobyov;

--
-- Создать таблицу `post`
--
CREATE TABLE post (
  id int(11) NOT NULL AUTO_INCREMENT,
  name varchar(50) NOT NULL DEFAULT '',
  salary int(11) NOT NULL,
  PRIMARY KEY (id)
)
ENGINE = INNODB,
CHARACTER SET utf8mb4,
COLLATE utf8mb4_general_ci;

--
-- Создать таблицу `staff`
--
CREATE TABLE staff (
  id int(11) NOT NULL AUTO_INCREMENT,
  firstname varchar(50) NOT NULL DEFAULT '',
  surname varchar(255) NOT NULL DEFAULT '',
  lastname varchar(255) NOT NULL DEFAULT '',
  id_post int(11) NOT NULL,
  PRIMARY KEY (id)
)
ENGINE = INNODB,
CHARACTER SET utf8mb4,
COLLATE utf8mb4_general_ci;

--
-- Создать внешний ключ
--
ALTER TABLE staff
ADD CONSTRAINT FK_staff_id_post FOREIGN KEY (id_post)
REFERENCES post (id) ON DELETE NO ACTION;

--
-- Создать таблицу `table`
--
CREATE TABLE `table` (
  id int(11) NOT NULL AUTO_INCREMENT,
  number int(11) NOT NULL,
  PRIMARY KEY (id)
)
ENGINE = INNODB,
CHARACTER SET utf8mb4,
COLLATE utf8mb4_general_ci;

--
-- Создать таблицу `client`
--
CREATE TABLE client (
  id int(11) NOT NULL AUTO_INCREMENT,
  firstname varchar(255) NOT NULL DEFAULT '',
  phonenumber int(11) NOT NULL,
  time datetime NOT NULL,
  PRIMARY KEY (id)
)
ENGINE = INNODB,
CHARACTER SET utf8mb4,
COLLATE utf8mb4_general_ci;

--
-- Создать таблицу `order`
--
CREATE TABLE `order` (
  id int(11) NOT NULL AUTO_INCREMENT,
  id_client int(11) DEFAULT NULL,
  id_table int(11) DEFAULT NULL,
  id_staff int(11) DEFAULT NULL,
  time datetime DEFAULT NULL,
  PRIMARY KEY (id)
)
ENGINE = INNODB,
CHARACTER SET utf8mb4,
COLLATE utf8mb4_general_ci;

--
-- Создать внешний ключ
--
ALTER TABLE `order`
ADD CONSTRAINT FK_order_id_client FOREIGN KEY (id_client)
REFERENCES client (id) ON DELETE NO ACTION;

--
-- Создать внешний ключ
--
ALTER TABLE `order`
ADD CONSTRAINT FK_order_id_staff FOREIGN KEY (id_staff)
REFERENCES staff (id) ON DELETE NO ACTION;

--
-- Создать внешний ключ
--
ALTER TABLE `order`
ADD CONSTRAINT FK_order_id_table FOREIGN KEY (id_table)
REFERENCES `table` (id) ON DELETE NO ACTION;

--
-- Создать таблицу `menu`
--
CREATE TABLE menu (
  id int(11) NOT NULL AUTO_INCREMENT,
  name varchar(255) NOT NULL DEFAULT '',
  status int(11) NOT NULL DEFAULT 1,
  PRIMARY KEY (id)
)
ENGINE = INNODB,
AUTO_INCREMENT = 17,
AVG_ROW_LENGTH = 3276,
CHARACTER SET utf8mb4,
COLLATE utf8mb4_general_ci;

--
-- Создать таблицу `food`
--
CREATE TABLE food (
  id int(11) NOT NULL AUTO_INCREMENT,
  name varchar(50) NOT NULL DEFAULT '',
  count varchar(50) NOT NULL DEFAULT 'Нет в наличии',
  price int(11) NOT NULL,
  img text NOT NULL,
  description varchar(255) NOT NULL DEFAULT '',
  status int(11) NOT NULL DEFAULT 1,
  PRIMARY KEY (id)
)
ENGINE = INNODB,
AUTO_INCREMENT = 11,
AVG_ROW_LENGTH = 1638,
CHARACTER SET utf8mb4,
COLLATE utf8mb4_general_ci;

--
-- Создать таблицу `idmenufood`
--
CREATE TABLE idmenufood (
  id int(11) NOT NULL AUTO_INCREMENT,
  id_menu int(11) NOT NULL DEFAULT 0,
  id_food int(11) NOT NULL DEFAULT 0,
  PRIMARY KEY (id)
)
ENGINE = INNODB,
AUTO_INCREMENT = 53,
AVG_ROW_LENGTH = 1489,
CHARACTER SET utf8mb4,
COLLATE utf8mb4_general_ci;

--
-- Создать внешний ключ
--
ALTER TABLE idmenufood
ADD CONSTRAINT FK_idmenufood_id_food FOREIGN KEY (id_food)
REFERENCES food (id) ON DELETE NO ACTION;

--
-- Создать внешний ключ
--
ALTER TABLE idmenufood
ADD CONSTRAINT FK_idmenufood_id_menu FOREIGN KEY (id_menu)
REFERENCES menu (id) ON DELETE NO ACTION;

--
-- Создать таблицу `order-menu`
--
CREATE TABLE `order-menu` (
  id_order int(11) NOT NULL,
  id_menufood int(11) NOT NULL,
  count int(11) DEFAULT NULL
)
ENGINE = INNODB,
CHARACTER SET utf8mb4,
COLLATE utf8mb4_general_ci;

--
-- Создать внешний ключ
--
ALTER TABLE `order-menu`
ADD CONSTRAINT `FK_order-menu_id_menufood` FOREIGN KEY (id_menufood)
REFERENCES idmenufood (id) ON DELETE NO ACTION;

--
-- Создать внешний ключ
--
ALTER TABLE `order-menu`
ADD CONSTRAINT `FK_order-menu_id_order` FOREIGN KEY (id_order)
REFERENCES `order` (id) ON DELETE NO ACTION;

-- 
-- Вывод данных для таблицы post
--
-- Таблица bufet1131vorobyov.post не содержит данных

-- 
-- Вывод данных для таблицы `table`
--
-- Таблица bufet1131vorobyov.`table` не содержит данных

-- 
-- Вывод данных для таблицы staff
--
-- Таблица bufet1131vorobyov.staff не содержит данных

-- 
-- Вывод данных для таблицы client
--
-- Таблица bufet1131vorobyov.client не содержит данных

-- 
-- Вывод данных для таблицы menu
--
INSERT INTO menu VALUES
(1, 'Осеннее меню', 1),
(2, 'Зимнее меню', 1),
(3, 'Весеннее меню', 1),
(4, 'Летнее меню', 1),
(15, 'Простое меню', 1);

-- 
-- Вывод данных для таблицы food
--
INSERT INTO food VALUES
(1, 'Жаренная картошка', '20', 25, 'C:UsersUsersource\reposBufet1131VorobyovimgBaked Potato.png', '+2.5 к сытости', 1),
(2, 'Тушёные грибы', '30', 35, 'C:UsersUsersource\reposBufet1131VorobyovimgMushroom Stew.png', '', 1),
(3, 'Жаренная говядина', '15', 48, 'C:UsersUsersource\reposBufet1131VorobyovimgCooked Beef.png', '', 1),
(4, 'Жаренная свинина', '33', 42, 'C:UsersUsersource\reposBufet1131VorobyovimgCooked Porkchop.png', '', 1),
(5, 'Сладкие ягоды', '13', 30, 'C:\\Users\\User\\source\\repos\\Bufet1131Vorobyov\\img\\Sweet Berries.png', '', 1),
(6, 'Тыквенный пирог', '8', 100, 'C:\\Users\\User\\source\\repos\\Bufet1131Vorobyov\\img\\Pumpkin Pie.png', '', 1),
(7, 'Торт', '10', 150, 'C:\\Users\\User\\source\\repos\\Bufet1131Vorobyov\\img\\Cake.png', 'Просто торт', 1),
(8, 'Бутылочка мёда', '38', 30, 'C:\\Users\\User\\source\\repos\\Bufet1131Vorobyov\\img\\Honey Bottle.png', '', 1),
(9, 'Свекольный суп', '24', 56, 'C:\\Users\\User\\source\\repos\\Bufet1131Vorobyov\\img\\Beetroot Soup.png', '', 1),
(10, 'Жареный лосось', '27', 26, 'C:\\Users\\User\\source\\repos\\Bufet1131Vorobyov\\img\\Cooked salmon.png', '', 1);

-- 
-- Вывод данных для таблицы `order`
--
-- Таблица bufet1131vorobyov.`order` не содержит данных

-- 
-- Вывод данных для таблицы idmenufood
--
INSERT INTO idmenufood VALUES
(1, 1, 1),
(2, 1, 2),
(3, 1, 5),
(4, 2, 1),
(5, 3, 7),
(6, 3, 9),
(7, 3, 8),
(8, 4, 6),
(9, 1, 6),
(11, 1, 8),
(27, 4, 10);

-- 
-- Вывод данных для таблицы `order-menu`
--
-- Таблица bufet1131vorobyov.`order-menu` не содержит данных

-- 
-- Восстановить предыдущий режим SQL (SQL mode)
--
/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;

-- 
-- Включение внешних ключей
-- 
/*!40014 SET FOREIGN_KEY_CHECKS = @OLD_FOREIGN_KEY_CHECKS */;