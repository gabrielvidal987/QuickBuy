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
  `caminho_foto` varchar(2000) DEFAULT NULL,
  `usuario` varchar(200) DEFAULT NULL,
  `qtd_vendido` int DEFAULT '0',
  `categoria` varchar(50) DEFAULT NULL,
  `qtd_estoque` int NOT NULL DEFAULT '999',
  `ativo` tinyint(1) NOT NULL DEFAULT '1',
  PRIMARY KEY (`id_produto`)
) ENGINE=InnoDB AUTO_INCREMENT=70 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `produtos`
--

LOCK TABLES `produtos` WRITE;
/*!40000 ALTER TABLE `produtos` DISABLE KEYS */;
INSERT INTO `produtos` VALUES (26,'açai','2.50','açai.png','desbravadores',1,'Gelados',999,1),(28,'suci','1.00','suci.png','desbravadores',2,'Bebidas',999,1),(42,'pitiça','1.50','pitiça.png','desbravadores',0,'Pizzas',999,1),(43,'hamburguer','1.00','lanche.png','desbravadores',2,'Hamburgueres',999,1),(55,' açai pequeno','7.00',' açai pequeno.png','campestrejunior',0,'Gelados',999,1),(56,'açai medio','10.00','açai medio.png','campestrejunior',70,'Gelados',999,1),(57,'trufa','5.00','trufa.png','campestrejunior',15,'Hamburgueres',999,1),(58,'brincadeira','2.00','brincadeira.png','campestrejunior',86,'Hamburgueres',999,1),(59,'pastel queijo','10.00','pastel queijo.png','campestrejunior',58,'Pizzas',999,1),(60,'pastel pizza','10.00','pastel pizza.png','campestrejunior',40,'Hamburgueres',999,1),(61,'pastel carne c queijo','10.00','pastel carne c queijo.png','campestrejunior',17,'Hamburgueres',999,1),(62,'carne c ovo','10.00','carne c ovo.png','campestrejunior',12,'Hamburgueres',999,1),(63,'suco','0.01','suco.png','campestrejunior',14,'Hamburgueres',999,1),(64,'pastel de queijo','10.00','pastel de queijo.png','campestre',36,'Pizzas',999,1),(65,'pastel pizza','10.00','pastel pizza.png','campestre',21,'Pizzas',999,1),(66,'pastel dois queijos','12.00','pastel dois queijos.png','campestre',62,'Pizzas',999,1),(67,'pastel chocolate','12.00','pastel chocolate.png','campestre',0,'Hamburgueres',999,1),(68,'bolo','10.00','bolo.png','campestre',43,'Gelados',999,1),(69,'açaí','10.00','açaí.png','campestre',146,'Gelados',999,1);
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

-- Dump completed on 2025-07-09 22:20:13
