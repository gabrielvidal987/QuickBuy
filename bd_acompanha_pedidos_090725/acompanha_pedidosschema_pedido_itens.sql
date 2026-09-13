-- MySQL dump 10.13  Distrib 8.0.36, for Win64 (x86_64)
--
-- Host: localhost    Database: acompanha_pedidosschema
-- ------------------------------------------------------
-- Server version	8.0.37

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
-- Table structure for table `pedido_itens`
--

DROP TABLE IF EXISTS `pedido_itens`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `pedido_itens` (
  `id_item` int NOT NULL AUTO_INCREMENT,
  `id_pedido` int NOT NULL,
  `id_produto` int DEFAULT NULL COMMENT 'NULL quando o produto foi excluído do cadastro (o histórico da venda continua válido pelo nome)',
  `usuario` varchar(200) DEFAULT NULL,
  `nome_produto` varchar(500) NOT NULL COMMENT 'Nome do produto no momento da venda (snapshot)',
  `quantidade` int NOT NULL,
  `valor_unitario` decimal(10,2) DEFAULT NULL COMMENT 'Preço realmente cobrado no item (varejo ou atacado)',
  `valor_total` decimal(10,2) DEFAULT NULL,
  `atacado_aplicado` tinyint(1) NOT NULL DEFAULT '0',
  `id_combo` int DEFAULT NULL COMMENT 'Combo cujo preço foi aplicado neste item (NULL = preço normal ou atacado individual)',
  PRIMARY KEY (`id_item`),
  KEY `idx_pedido_itens_pedido` (`id_pedido`),
  KEY `idx_pedido_itens_produto` (`id_produto`),
  KEY `idx_pedido_itens_usuario` (`usuario`),
  KEY `idx_pedido_itens_combo` (`id_combo`),
  CONSTRAINT `fk_pedido_itens_combo` FOREIGN KEY (`id_combo`) REFERENCES `combos` (`id_combo`) ON DELETE SET NULL,
  CONSTRAINT `fk_pedido_itens_pedido` FOREIGN KEY (`id_pedido`) REFERENCES `pedidos` (`numero_pedido`) ON DELETE CASCADE,
  CONSTRAINT `fk_pedido_itens_produto` FOREIGN KEY (`id_produto`) REFERENCES `produtos` (`id_produto`) ON DELETE SET NULL
) ENGINE=InnoDB AUTO_INCREMENT=525 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `pedido_itens`
--

LOCK TABLES `pedido_itens` WRITE;
/*!40000 ALTER TABLE `pedido_itens` DISABLE KEYS */;
/*!40000 ALTER TABLE `pedido_itens` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2026-09-07 10:16:42
