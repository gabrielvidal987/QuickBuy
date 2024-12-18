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
  `pagamento_aprovado` tinyint(1) NOT NULL DEFAULT '0',
  PRIMARY KEY (`numero_pedido`)
) ENGINE=InnoDB AUTO_INCREMENT=126 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `pedidos`
--

LOCK TABLES `pedidos` WRITE;
/*!40000 ALTER TABLE `pedidos` DISABLE KEYS */;
INSERT INTO `pedidos` VALUES (120,'te','endereço não cadastrado','1X açai,1X suci,','sem observações','14:41:01',3.5,'dinheiro',3.5,'desbravadores',0,0),(122,'bnrtb','endereço não cadastrado','1X açai,1X pitiça,','sem observações','14:41:05',4,'dinheiro',4,'desbravadores',0,1),(124,'erverv','endereço não cadastrado','1X açai,1X suci,','sem observações','14:41:08',3.5,'dinheiro',3.5,'desbravadores',0,1),(125,'erver','endereço não cadastrado','1X açai,','sem observações','14:41:12',2.5,'dinheiro',2.5,'desbravadores',0,1);
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

-- Dump completed on 2024-10-21 16:01:26
