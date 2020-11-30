--
-- Скрипт сгенерирован Devart dbForge Studio 2020 for MySQL, Версия 9.0.391.0
-- Домашняя страница продукта: http://www.devart.com/ru/dbforge/mysql/studio
-- Дата скрипта: 17.11.2020 14:49:59
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
-- Создать таблицу `menu`
--
CREATE TABLE menu (
  id int(11) NOT NULL AUTO_INCREMENT,
  name varchar(255) NOT NULL DEFAULT '',
  status int(11) NOT NULL DEFAULT 1,
  PRIMARY KEY (id)
)
ENGINE = INNODB,
AUTO_INCREMENT = 26,
AVG_ROW_LENGTH = 2730,
CHARACTER SET utf8mb4,
COLLATE utf8mb4_general_ci;

--
-- Создать таблицу `provider`
--
CREATE TABLE provider (
  id int(11) NOT NULL AUTO_INCREMENT,
  name varchar(50) NOT NULL DEFAULT '',
  phone varchar(255) NOT NULL DEFAULT '',
  address varchar(255) NOT NULL DEFAULT '',
  mail varchar(255) NOT NULL DEFAULT '',
  status int(11) NOT NULL DEFAULT 1,
  PRIMARY KEY (id)
)
ENGINE = INNODB,
AUTO_INCREMENT = 11,
AVG_ROW_LENGTH = 5461,
CHARACTER SET utf8mb4,
COLLATE utf8mb4_general_ci;

--
-- Создать таблицу `food`
--
CREATE TABLE food (
  id int(11) NOT NULL AUTO_INCREMENT,
  name varchar(50) NOT NULL DEFAULT '',
  count int(11) NOT NULL DEFAULT 0,
  price int(11) NOT NULL DEFAULT 0,
  img text NOT NULL,
  description varchar(255) NOT NULL DEFAULT '',
  status int(11) NOT NULL DEFAULT 1,
  PRIMARY KEY (id)
)
ENGINE = INNODB,
AUTO_INCREMENT = 26,
AVG_ROW_LENGTH = 1489,
CHARACTER SET utf8mb4,
COLLATE utf8mb4_general_ci;

--
-- Создать таблицу `orderfood`
--
CREATE TABLE orderfood (
  id int(11) NOT NULL AUTO_INCREMENT,
  date bigint(20) NOT NULL,
  count int(11) NOT NULL,
  cost int(11) NOT NULL,
  id_provider int(11) NOT NULL,
  id_food int(11) NOT NULL,
  id_menu int(11) NOT NULL,
  PRIMARY KEY (id)
)
ENGINE = INNODB,
AUTO_INCREMENT = 51,
AVG_ROW_LENGTH = 2048,
CHARACTER SET utf8mb4,
COLLATE utf8mb4_general_ci;

--
-- Создать внешний ключ
--
ALTER TABLE orderfood
ADD CONSTRAINT FK_order_id_food FOREIGN KEY (id_food)
REFERENCES food (id);

--
-- Создать внешний ключ
--
ALTER TABLE orderfood
ADD CONSTRAINT FK_order_id_menu FOREIGN KEY (id_menu)
REFERENCES menu (id);

--
-- Создать внешний ключ
--
ALTER TABLE orderfood
ADD CONSTRAINT FK_order_id_provider FOREIGN KEY (id_provider)
REFERENCES provider (id);

--
-- Создать таблицу `idproviderfood`
--
CREATE TABLE idproviderfood (
  id int(11) NOT NULL AUTO_INCREMENT,
  id_provider int(11) NOT NULL,
  id_food int(11) NOT NULL,
  PRIMARY KEY (id)
)
ENGINE = INNODB,
AUTO_INCREMENT = 19,
AVG_ROW_LENGTH = 2730,
CHARACTER SET utf8mb4,
COLLATE utf8mb4_general_ci;

--
-- Создать внешний ключ
--
ALTER TABLE idproviderfood
ADD CONSTRAINT FK_idproviderfood_id_food FOREIGN KEY (id_food)
REFERENCES food (id);

--
-- Создать внешний ключ
--
ALTER TABLE idproviderfood
ADD CONSTRAINT FK_idproviderfood_id_provider FOREIGN KEY (id_provider)
REFERENCES provider (id);

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
AUTO_INCREMENT = 68,
AVG_ROW_LENGTH = 1260,
CHARACTER SET utf8mb4,
COLLATE utf8mb4_general_ci;

--
-- Создать внешний ключ
--
ALTER TABLE idmenufood
ADD CONSTRAINT FK_idmenufood_id_food FOREIGN KEY (id_food)
REFERENCES food (id);

--
-- Создать внешний ключ
--
ALTER TABLE idmenufood
ADD CONSTRAINT FK_idmenufood_id_menu FOREIGN KEY (id_menu)
REFERENCES menu (id);

--
-- Создать таблицу `accountingprovider`
--
CREATE TABLE accountingprovider (
  id int(11) NOT NULL AUTO_INCREMENT,
  count int(11) NOT NULL,
  date bigint(20) NOT NULL,
  id_provider int(11) NOT NULL,
  id_food int(11) NOT NULL,
  PRIMARY KEY (id)
)
ENGINE = INNODB,
AUTO_INCREMENT = 43,
AVG_ROW_LENGTH = 16384,
CHARACTER SET utf8mb4,
COLLATE utf8mb4_general_ci;

--
-- Создать внешний ключ
--
ALTER TABLE accountingprovider
ADD CONSTRAINT FK_accountingprovider_id_food FOREIGN KEY (id_food)
REFERENCES food (id);

--
-- Создать внешний ключ
--
ALTER TABLE accountingprovider
ADD CONSTRAINT FK_accountingprovider_id_provider FOREIGN KEY (id_provider)
REFERENCES provider (id);

-- 
-- Вывод данных для таблицы menu
--
INSERT INTO menu VALUES
(1, 'Осеннее меню', 2),
(2, 'Зимнее меню', 2),
(3, 'Весеннее меню', 2),
(4, 'Летнее меню', 2),
(15, 'Простое меню', 0),
(18, 'Тест меню', 2),
(25, 'fsdfsdds', 0);

-- 
-- Вывод данных для таблицы provider
--
INSERT INTO provider VALUES
(0, '', '', '', '', -1),
(1, 'Сады Ивановки', '88005553535', 'РґРµСЂРµРІРЅСЏ РРІР°РЅРѕРІРєР°, РґРѕРј 3', 'sadyivanovki@mail.ru', 1),
(2, 'Охотничий привал', '89003322332', 'РґРµСЂРµРІРЅСЏ РРІР°РЅРѕРІРєР°, РґРѕРј 15', 'ohotnichiyprival@gamil.com', 1),
(3, 'Пасека Копатыча', '+79038887733', 'РґРµСЂРµРІРЅСЏ РЎРјРµС€Р°СЂРёРєРѕРІ, РєСЂСѓРіР»С‹Р№ РґРѕРј СЃ РѕРіРѕСЂРѕРґРѕРј', 'pasekakopatycha@bk.ru', 1),
(4, '', '+5656465465', '', '', 0),
(5, 'Носатая ферма', '89003335353', 'РґРµСЂРµРІРЅСЏ РњР°С‚РІРµРµРІРєР°', 'nosatayaferma@derevnya.ru', 1),
(7, 'Какой-то поставщик', '+79034346756', 'деревня Матвеевка, улица Единственная, дом 3', 'kakoytopostavshik@mail.ru', 1),
(8, 'Тест поставщик 1', '+79003337575', 'РґРµСЂРµРІРЅСЏ С‚РµСЃС‚РµСЂРѕРІ', 'test@mail.ru', 0),
(9, 'Тест поставщик 2', '', '', '', 0),
(10, '', '', '', '', 0);

-- 
-- Вывод данных для таблицы food
--
INSERT INTO food VALUES
(1, 'Жаренная картошка', 40, 25, 'C:\\Users\\User\\source\\repos\\Bufet1131Vorobyov\\img\\Baked Potato.png', '+2.5 Рє СЃС‹С‚РѕСЃС‚Рё', 1),
(2, 'Тушёные грибы', 30, 35, 'C:\\Users\\User\\source\\repos\\Bufet1131Vorobyov\\img\\Mushroom Stew.png', '', 1),
(3, 'Жареная говядина', 75, 48, 'C:\\Users\\User\\source\\repos\\Bufet1131Vorobyov\\img\\Cooked Beef.png', '', 1),
(4, 'Жареная свинина', 33, 42, 'C:\\Users\\User\\source\\repos\\Bufet1131Vorobyov\\img\\Cooked Porkchop.png', '', 1),
(5, 'Сладкие ягоды', 97, 30, 'C:\\Users\\User\\source\\repos\\Bufet1131Vorobyov\\img\\Sweet Berries.png', '', 1),
(6, 'Тыквенный пирог', 8, 100, 'C:\\Users\\User\\source\\repos\\Bufet1131Vorobyov\\img\\Pumpkin Pie.png', '', 1),
(7, 'Торт', 10, 150, 'C:\\Users\\User\\source\\repos\\Bufet1131Vorobyov\\img\\Cake.png', 'РџСЂРѕСЃС‚Рѕ С‚РѕСЂС‚', 1),
(8, 'Бутылочка мёда', 38, 30, 'C:\\Users\\User\\source\\repos\\Bufet1131Vorobyov\\img\\Honey Bottle.png', '', 1),
(9, 'Свекольный суп', 24, 56, 'C:\\Users\\User\\source\\repos\\Bufet1131Vorobyov\\img\\Beetroot Soup.png', '', 1),
(10, 'Жареный лосось', 27, 26, 'C:\\Users\\User\\source\\repos\\Bufet1131Vorobyov\\img\\Cooked salmon.png', '', 1),
(14, 'dsgffgv', 0, 0, 'C:\\Users\\User\\source\\repos\\Bufet1131Vorobyov\\img\\Beetroot Soup.png', 'fsdfdsf', 0),
(17, 'Р°РІР°РІРІР°', 0, 0, '', '', 0),
(18, 'РўРµСЃС‚ Р•РґР°', 55, 55, '', 'РўРµСЃС‚ Р•РґР°', 0),
(19, '', 0, 0, '', '', 0),
(20, '', 0, 0, '', '', 0),
(21, '', 0, 0, '', '', 0),
(22, 'РўРµСЃС‚ Р‘Р»СЋРґРѕ', 30, 5, '', 'РўРµСЃС‚ Р‘Р»СЋРґРѕ', 0),
(23, 'Тест блюдо 2', 110, 10, 'C:\\Users\\User\\source\\repos\\Bufet1131Vorobyov\\img\\Honey Bottle.png', 'РўРµСЃС‚ Р±Р»СЋРґРѕ 2', 1),
(24, 'Тест блюдо 3', 0, 0, '', '', 1),
(25, 'Тест блюдо 4', 50, 0, '', '', 1);

-- 
-- Вывод данных для таблицы orderfood
--
INSERT INTO orderfood VALUES
(39, -8585961580854775808, 1000, 25000, 5, 1, 2),
(40, -8585961580854775808, 0, 0, 0, 24, 15),
(41, -8585961580854775808, 0, 0, 9, 23, 15),
(42, -8585961580854775808, 0, 0, 9, 23, 15);

-- 
-- Вывод данных для таблицы idproviderfood
--
INSERT INTO idproviderfood VALUES
(1, 1, 5),
(2, 1, 8),
(3, 1, 9),
(4, 2, 3),
(5, 2, 4),
(9, 5, 1),
(10, 5, 5),
(11, 5, 9),
(12, 5, 8),
(13, 3, 8),
(14, 8, 22),
(15, 9, 23);

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
(27, 4, 10),
(53, 15, 14),
(61, 15, 22),
(62, 15, 23),
(63, 15, 24),
(65, 18, 25),
(66, 18, 23),
(67, 18, 24);

-- 
-- Вывод данных для таблицы accountingprovider
--
INSERT INTO accountingprovider VALUES
(20, 70, -8585971084854775808, 5, 5),
(21, 10, -8585971084854775808, 2, 3),
(22, 50, -8585971084854775808, 5, 5),
(23, 20, -8585971084854775808, 2, 4),
(24, 20, -8585965900854775808, 8, 22),
(25, 10, -8585965900854775808, 8, 22),
(27, 10, -8585965900854775808, 9, 23),
(28, 10, -8585965900854775808, 9, 23),
(33, 0, -8585961580854775808, 5, 1),
(34, 0, -8585961580854775808, 5, 9),
(35, 0, -8585961580854775808, 1, 8),
(36, 0, -8585961580854775808, 3, 8),
(38, 0, -8585961580854775808, 1, 5),
(39, 0, -8585961580854775808, 3, 8),
(40, 100, -8585960716854775808, 7, 25),
(41, 10, -8585960716854775808, 1, 5);

-- 
-- Восстановить предыдущий режим SQL (SQL mode)
--
/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;

-- 
-- Включение внешних ключей
-- 
/*!40014 SET FOREIGN_KEY_CHECKS = @OLD_FOREIGN_KEY_CHECKS */;