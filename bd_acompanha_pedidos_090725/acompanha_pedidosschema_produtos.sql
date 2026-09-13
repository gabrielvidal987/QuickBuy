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
-- Table structure for table `produtos`
--

DROP TABLE IF EXISTS `produtos`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `produtos` (
  `id_produto` int NOT NULL AUTO_INCREMENT,
  `nome` varchar(500) DEFAULT NULL,
  `valor` varchar(50) DEFAULT NULL,
  `valor_custo` decimal(10,2) NOT NULL DEFAULT '0.00' COMMENT 'Custo de aquisição do produto (unitário)',
  `valor_atacado` decimal(10,2) DEFAULT NULL COMMENT 'Preço de venda no atacado; NULL = produto sem preço de atacado',
  `qtd_min_atacado` int DEFAULT NULL COMMENT 'Quantidade mínima do item na venda para valer o preço de atacado',
  `caminho_foto` varchar(2000) DEFAULT NULL,
  `usuario` varchar(200) DEFAULT NULL,
  `qtd_vendido` int DEFAULT '0',
  `categoria` varchar(50) DEFAULT NULL,
  `qtd_estoque` int NOT NULL DEFAULT '999',
  `ativo` tinyint(1) NOT NULL DEFAULT '1',
  PRIMARY KEY (`id_produto`)
) ENGINE=InnoDB AUTO_INCREMENT=112 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `produtos`
--

LOCK TABLES `produtos` WRITE;
/*!40000 ALTER TABLE `produtos` DISABLE KEYS */;
INSERT INTO `produtos` VALUES (108,'Acendedor','2.50',2.23,NULL,NULL,'Acendedor.png','padilha',0,'Bebidas',1000,1),(109,'Carvao 2KG','9.50',7.27,NULL,NULL,'Carvao 2KG.png','padilha',0,'Bebidas',1321,1),(110,'Carvao 4KG','18.50',14.53,NULL,NULL,'Carvao 4KG.png','padilha',0,'Bebidas',152,1),(111,'Carvao 8KG','37.00',28.87,NULL,NULL,'Carvao 8KG.png','padilha',0,'Bebidas',40,1);
/*!40000 ALTER TABLE `produtos` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2026-09-07 10:16:41
