CREATE TABLE IF NOT EXISTS `books` (
  `id` bigint NOT NULL AUTO_INCREMENT,
  `category` varchar(100) NOT NULL,
  `title` varchar(100) NOT NULL,
  `price` real NOT NULL,
  `isbn` varchar(100) NOT NULL UNIQUE,
  `amount` decimal NOT NULL,
   PRIMARY KEY (`id`)
);