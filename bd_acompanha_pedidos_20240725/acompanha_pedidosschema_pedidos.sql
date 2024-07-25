-- MySQL dump 10.13  Distrib 8.0.36, for Win64 (x86_64)
--
-- Host: localhost    Database: acompanha_pedidosschema
-- ------------------------------------------------------
-- Server version	8.0.36

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `pedidos`
--

DROP TABLE IF EXISTS `pedidos`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `pedidos` (
  `numero_pedido` int NOT NULL AUTO_INCREMENT,
  `nome_cliente` varchar(50) DEFAULT NULL,
  `endereco` varchar(2000) DEFAULT NULL,
  `produtos_nome` varchar(2000) DEFAULT NULL,
  `observacoes` varchar(500) DEFAULT NULL,
  `hora_pedido` char(50) DEFAULT NULL,
  `valorTotal` double DEFAULT NULL,
  `formaPag` char(30) DEFAULT NULL,
  `valorLiq` double DEFAULT NULL,
  `usuario` varchar(200) DEFAULT NULL,
  `delivery` tinyint(1) DEFAULT '0',
  PRIMARY KEY (`numero_pedido`)
) ENGINE=InnoDB AUTO_INCREMENT=74 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `pedidos`
--

LOCK TABLES `pedidos` WRITE;
/*!40000 ALTER TABLE `pedidos` DISABLE KEYS */;
INSERT INTO `pedidos` VALUES (62,'teteeee','aqeulaa pracaaaa','1X suco,1X pizza,1X açai,','sem observações','15:29:07',3,'dinheiro',3,'desbravadores',0),(66,'ererb','endereço não cadastrado','2X saaabe,1X suco,1X pizza,','sem observações','15:29:32',3,'dinheiro',3,'desbravadores',0),(67,'erbrtn','endereço não cadastrado','1X pizza,1X bolo,1X suco,','sem observações','15:29:34',4.5,'dinheiro',4.5,'desbravadores',0),(68,'vsdvsd','endereço não cadastrado','1X açai,1X bolo,1X pizza,','sem observações','15:29:36',5,'dinheiro',5,'desbravadores',0),(69,'erberb','endereço não cadastrado','1X açai,1X suco,1X pizza,','sem observações','15:29:38',3,'dinheiro',3,'desbravadores',0),(70,'vrv','endereço não cadastrado','1X testeeee,1X bolo,1X suco,','sem observações','15:29:40',3.5,'dinheiro',3.5,'desbravadores',0),(71,'sdfsdf','endereço não cadastrado','1X açai,2X pizza,1X bolo,2X saaabe,','sem observações','15:29:43',7.5,'dinheiro',7.5,'desbravadores',0),(73,'tete','sfeteeetee','1X açai,1X bolo,','sem observações','14:36:07',3.5,'dinheiro',3.5,'desbravadores',1);
/*!40000 ALTER TABLE `pedidos` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2024-07-25 14:51:44
