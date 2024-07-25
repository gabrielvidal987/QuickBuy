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
-- Table structure for table `pedidos_prontos`
--

DROP TABLE IF EXISTS `pedidos_prontos`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `pedidos_prontos` (
  `numero_pedido` int NOT NULL,
  `nome_cliente` varchar(50) DEFAULT NULL,
  `endereco` varchar(2000) DEFAULT NULL,
  `produtos_nome` varchar(2000) DEFAULT NULL,
  `observacoes` varchar(500) DEFAULT NULL,
  `hora_pedido` char(50) DEFAULT NULL,
  `hora_ficou_pronto` char(50) DEFAULT NULL,
  `valorTotal` double DEFAULT NULL,
  `formaPag` char(30) DEFAULT NULL,
  `valorLiq` double DEFAULT NULL,
  `usuario` varchar(200) DEFAULT NULL,
  `delivery` tinyint(1) DEFAULT '0',
  PRIMARY KEY (`numero_pedido`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `pedidos_prontos`
--

LOCK TABLES `pedidos_prontos` WRITE;
/*!40000 ALTER TABLE `pedidos_prontos` DISABLE KEYS */;
INSERT INTO `pedidos_prontos` VALUES (61,'teste','endereço não cadastrado','1X pizza,1X bolo,2X açai,','sem observações','15:28:58','15:29:23',6,'dinheiro',6,'desbravadores',0),(63,'tetttt','endereço não cadastrado','1X bolo,1X testeeee,1X suco,1X saaabe,','sem observações','15:29:13','15:29:20',4,'dinheiro',4,'desbravadores',0),(64,'ergerg','endereço não cadastrado','1X pizza,1X bolo,1X açai,','sem observações','15:29:27','15:29:50',5,'dinheiro',5,'desbravadores',0),(65,'ver','endereço não cadastrado','1X açai,1X suco,1X pizza,2X bolo,','sem observações','15:29:29','15:29:57',8,'dinheiro',8,'desbravadores',0),(72,'erbtn','endereço não cadastrado','1X saaabe,1X suco,','sem observações','15:29:45','15:29:53',1,'dinheiro',1,'desbravadores',0),(99,'robson rodovsk',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,0),(105,'robson rodovsk',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'desbravadores',0),(190,'robson test',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'desbravadores',1),(1890,'robson test',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'desbravadores',1);
/*!40000 ALTER TABLE `pedidos_prontos` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2024-07-25 14:51:45
