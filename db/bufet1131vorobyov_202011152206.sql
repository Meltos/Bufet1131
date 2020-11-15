--
-- ������ ������������ Devart dbForge Studio 2020 for MySQL, ������ 9.0.435.0
-- �������� �������� ��������: http://www.devart.com/ru/dbforge/mysql/studio
-- ���� �������: 15.11.2020 22:06:56
-- ������ �������: 8.0.20
-- ������ �������: 4.1
--

-- 
-- ���������� ������� ������
-- 
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;

-- 
-- ���������� ����� SQL (SQL mode)
-- 
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;

DROP DATABASE IF EXISTS bufet1131vorobyov;

CREATE DATABASE bufet1131vorobyov
CHARACTER SET utf8mb4
COLLATE utf8mb4_general_ci;

--
-- ��������� ���� ������ �� ���������
--
USE bufet1131vorobyov;

--
-- ������� ������� `post`
--
CREATE TABLE post (
  id int NOT NULL AUTO_INCREMENT,
  name varchar(50) NOT NULL DEFAULT '',
  salary int NOT NULL,
  PRIMARY KEY (id)
)
ENGINE = INNODB,
CHARACTER SET utf8mb4,
COLLATE utf8mb4_general_ci;

--
-- ������� ������� `staff`
--
CREATE TABLE staff (
  id int NOT NULL AUTO_INCREMENT,
  firstname varchar(50) NOT NULL DEFAULT '',
  surname varchar(255) NOT NULL DEFAULT '',
  lastname varchar(255) NOT NULL DEFAULT '',
  id_post int NOT NULL,
  PRIMARY KEY (id)
)
ENGINE = INNODB,
CHARACTER SET utf8mb4,
COLLATE utf8mb4_general_ci;

--
-- ������� ������� ����
--
ALTER TABLE staff
ADD CONSTRAINT FK_staff_id_post FOREIGN KEY (id_post)
REFERENCES post (id);

--
-- ������� ������� `menu`
--
CREATE TABLE menu (
  id int NOT NULL AUTO_INCREMENT,
  name varchar(255) NOT NULL DEFAULT '',
  status int NOT NULL DEFAULT 1,
  PRIMARY KEY (id)
)
ENGINE = INNODB,
AUTO_INCREMENT = 25,
AVG_ROW_LENGTH = 2730,
CHARACTER SET utf8mb4,
COLLATE utf8mb4_general_ci;

--
-- ������� ������� `provider`
--
CREATE TABLE provider (
  id int NOT NULL AUTO_INCREMENT,
  name varchar(50) NOT NULL DEFAULT '',
  phone varchar(255) NOT NULL DEFAULT '',
  address varchar(255) NOT NULL DEFAULT '',
  mail varchar(255) NOT NULL DEFAULT '',
  status int NOT NULL DEFAULT 1,
  PRIMARY KEY (id)
)
ENGINE = INNODB,
AUTO_INCREMENT = 10,
AVG_ROW_LENGTH = 5461,
CHARACTER SET utf8mb4,
COLLATE utf8mb4_general_ci;

--
-- ������� ������� `food`
--
CREATE TABLE food (
  id int NOT NULL AUTO_INCREMENT,
  name varchar(50) NOT NULL DEFAULT '',
  count int NOT NULL DEFAULT 0,
  price int NOT NULL DEFAULT 0,
  img text NOT NULL,
  description varchar(255) NOT NULL DEFAULT '',
  status int NOT NULL DEFAULT 1,
  PRIMARY KEY (id)
)
ENGINE = INNODB,
AUTO_INCREMENT = 24,
AVG_ROW_LENGTH = 1489,
CHARACTER SET utf8mb4,
COLLATE utf8mb4_general_ci;

--
-- ������� ������� `orderfood`
--
CREATE TABLE orderfood (
  id int NOT NULL AUTO_INCREMENT,
  date bigint NOT NULL,
  count int NOT NULL,
  cost int NOT NULL,
  id_provider int NOT NULL,
  id_food int NOT NULL,
  id_menu int NOT NULL,
  PRIMARY KEY (id)
)
ENGINE = INNODB,
AUTO_INCREMENT = 29,
AVG_ROW_LENGTH = 2048,
CHARACTER SET utf8mb4,
COLLATE utf8mb4_general_ci;

--
-- ������� ������� ����
--
ALTER TABLE orderfood
ADD CONSTRAINT FK_order_id_food FOREIGN KEY (id_food)
REFERENCES food (id);

--
-- ������� ������� ����
--
ALTER TABLE orderfood
ADD CONSTRAINT FK_order_id_menu FOREIGN KEY (id_menu)
REFERENCES menu (id);

--
-- ������� ������� ����
--
ALTER TABLE orderfood
ADD CONSTRAINT FK_order_id_provider FOREIGN KEY (id_provider)
REFERENCES provider (id);

--
-- ������� ������� `idproviderfood`
--
CREATE TABLE idproviderfood (
  id int NOT NULL AUTO_INCREMENT,
  id_provider int NOT NULL,
  id_food int NOT NULL,
  PRIMARY KEY (id)
)
ENGINE = INNODB,
AUTO_INCREMENT = 16,
AVG_ROW_LENGTH = 2730,
CHARACTER SET utf8mb4,
COLLATE utf8mb4_general_ci;

--
-- ������� ������� ����
--
ALTER TABLE idproviderfood
ADD CONSTRAINT FK_idproviderfood_id_food FOREIGN KEY (id_food)
REFERENCES food (id);

--
-- ������� ������� ����
--
ALTER TABLE idproviderfood
ADD CONSTRAINT FK_idproviderfood_id_provider FOREIGN KEY (id_provider)
REFERENCES provider (id);

--
-- ������� ������� `idmenufood`
--
CREATE TABLE idmenufood (
  id int NOT NULL AUTO_INCREMENT,
  id_menu int NOT NULL DEFAULT 0,
  id_food int NOT NULL DEFAULT 0,
  PRIMARY KEY (id)
)
ENGINE = INNODB,
AUTO_INCREMENT = 63,
AVG_ROW_LENGTH = 1260,
CHARACTER SET utf8mb4,
COLLATE utf8mb4_general_ci;

--
-- ������� ������� ����
--
ALTER TABLE idmenufood
ADD CONSTRAINT FK_idmenufood_id_food FOREIGN KEY (id_food)
REFERENCES food (id);

--
-- ������� ������� ����
--
ALTER TABLE idmenufood
ADD CONSTRAINT FK_idmenufood_id_menu FOREIGN KEY (id_menu)
REFERENCES menu (id);

--
-- ������� ������� `accountingprovider`
--
CREATE TABLE accountingprovider (
  id int NOT NULL AUTO_INCREMENT,
  count int NOT NULL,
  date bigint NOT NULL,
  id_provider int NOT NULL,
  id_food int NOT NULL,
  PRIMARY KEY (id)
)
ENGINE = INNODB,
AUTO_INCREMENT = 33,
AVG_ROW_LENGTH = 16384,
CHARACTER SET utf8mb4,
COLLATE utf8mb4_general_ci;

--
-- ������� ������� ����
--
ALTER TABLE accountingprovider
ADD CONSTRAINT FK_accountingprovider_id_food FOREIGN KEY (id_food)
REFERENCES food (id);

--
-- ������� ������� ����
--
ALTER TABLE accountingprovider
ADD CONSTRAINT FK_accountingprovider_id_provider FOREIGN KEY (id_provider)
REFERENCES provider (id);

-- 
-- ����� ������ ��� ������� post
--
-- ������� bufet1131vorobyov.post �� �������� ������

-- 
-- ����� ������ ��� ������� menu
--
INSERT INTO menu VALUES
(1, 'Осеннее меню', 2),
(2, 'Зимнее меню', 2),
(3, 'Весеннее меню', 2),
(4, 'Летнее меню', 2),
(15, 'Простое меню', 2),
(17, 'gvbcbdgf', 0),
(18, 'Пустое меню', 1),
(19, 'ываываы', 0),
(20, 'аыаывавыаы', 0),
(21, 'аыаывавыаыыавы', 0),
(22, 'итарвпывв', 0),
(23, 'ьтапвпыы', 0),
(24, 'исивапрарарап', 0);

-- 
-- ����� ������ ��� ������� provider
--
INSERT INTO provider VALUES
(0, '', '', '', '', -1),
(1, 'Сады Ивановки', '88005553535', 'деревня Ивановка, дом 3', 'sadyivanovki@mail.ru', 1),
(2, 'Охотничий привал', '89003322332', 'деревня Ивановка, дом 15', 'ohotnichiyprival@gamil.com', 1),
(3, 'Пасека Копатыча', '+79038887733', 'деревня Смешариков, круглый дом с огородом', 'pasekakopatycha@bk.ru', 1),
(4, '', '+5656465465', '', '', 0),
(5, 'Носатая ферма', '89003335353', 'деревня Матвеевка', 'nosatayaferma@derevnya.ru', 1),
(7, '', '', '', '', 1),
(8, 'Тест поставщик', '+79003337575', 'деревня тестеров', 'test@mail.ru', 0),
(9, 'Тест поставщик 2', '', '', '', 1);

-- 
-- ����� ������ ��� ������� food
--
INSERT INTO food VALUES
(1, 'Жаренная картошка', 40, 25, 'C:\\Users\\User\\source\\repos\\Bufet1131Vorobyov\\img\\Baked Potato.png', '+2.5 к сытости', 1),
(2, 'Тушёные грибы', 30, 35, 'C:\\Users\\User\\source\\repos\\Bufet1131Vorobyov\\img\\Mushroom Stew.png', '', 1),
(3, 'Жаренная говядина', 75, 48, 'C:\\Users\\User\\source\\repos\\Bufet1131Vorobyov\\img\\Cooked Beef.png', '', 1),
(4, 'Жаренная свинина', 33, 42, 'C:\\Users\\User\\source\\repos\\Bufet1131Vorobyov\\img\\Cooked Porkchop.png', '', 1),
(5, 'Сладкие ягоды', 87, 30, 'C:\\Users\\User\\source\\repos\\Bufet1131Vorobyov\\img\\Sweet Berries.png', '', 1),
(6, 'Тыквенный пирог', 8, 100, 'C:\\Users\\User\\source\\repos\\Bufet1131Vorobyov\\img\\Pumpkin Pie.png', '', 1),
(7, 'Торт', 10, 150, 'C:\\Users\\User\\source\\repos\\Bufet1131Vorobyov\\img\\Cake.png', 'Просто торт', 1),
(8, 'Бутылочка мёда', 38, 30, 'C:\\Users\\User\\source\\repos\\Bufet1131Vorobyov\\img\\Honey Bottle.png', '', 1),
(9, 'Свекольный суп', 24, 56, 'C:\\Users\\User\\source\\repos\\Bufet1131Vorobyov\\img\\Beetroot Soup.png', '', 1),
(10, 'Жареный лосось', 27, 26, 'C:\\Users\\User\\source\\repos\\Bufet1131Vorobyov\\img\\Cooked salmon.png', '', 1),
(14, 'dsgffgv', 0, 0, 'C:\\Users\\User\\source\\repos\\Bufet1131Vorobyov\\img\\Beetroot Soup.png', 'fsdfdsf', 0),
(17, 'ававва', 0, 0, '', '', 0),
(18, 'Тест Еда', 55, 55, '', 'Тест Еда', 0),
(19, '', 0, 0, '', '', 0),
(20, '', 0, 0, '', '', 0),
(21, '', 0, 0, '', '', 0),
(22, 'Тест Блюдо', 30, 5, '', 'Тест Блюдо', 0),
(23, 'Тест блюдо 2', 90, 10, 'C:\\Users\\AERO\\Desktop\\Учёба\\Курсовая 2\\Bufet1131VorobyovProga\\img\\Honey Bottle.png', 'Тест блюдо 2', 1);

-- 
-- ����� ������ ��� ������� staff
--
-- ������� bufet1131vorobyov.staff �� �������� ������

-- 
-- ����� ������ ��� ������� orderfood
--
INSERT INTO orderfood VALUES
(18, -8585965900854775808, 5, 125, 5, 1, 1),
(20, -8585964172854775808, 0, 0, 5, 1, 1),
(22, -8585963684665024724, 0, 0, 9, 23, 1),
(23, -8585963684064596721, 0, 0, 9, 23, 1),
(24, -8585963683448650719, 0, 0, 0, 23, 1),
(25, -8585963676653964681, 0, 0, 5, 1, 1),
(26, -8585963676272938680, 0, 0, 0, 1, 1);

-- 
-- ����� ������ ��� ������� idproviderfood
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
-- ����� ������ ��� ������� idmenufood
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
(54, 17, 1),
(55, 19, 1),
(56, 20, 1),
(57, 21, 1),
(58, 22, 1),
(59, 23, 1),
(60, 24, 1),
(61, 15, 22),
(62, 15, 23);

-- 
-- ����� ������ ��� ������� accountingprovider
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
(29, 50, -8585965900854775808, 9, 23),
(30, 20, -8585964172854775808, 9, 23);

-- 
-- ������������ ���������� ����� SQL (SQL mode)
--
/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;

-- 
-- ��������� ������� ������
-- 
/*!40014 SET FOREIGN_KEY_CHECKS = @OLD_FOREIGN_KEY_CHECKS */;