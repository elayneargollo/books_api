CREATE TABLE IF NOT EXISTS `purchase` (
  `id_client` bigint NOT NULL, 
  `id_book`  bigint NOT NULL, 
  `id` bigint PRIMARY KEY AUTO_INCREMENT, 
  `address` varchar(100) NOT NULL,
  `smartphone` decimal NOT NULL,	
  `email` varchar(100) NOT NULL,
  FOREIGN KEY (id_book) REFERENCES books (id),
  FOREIGN KEY (id_client) REFERENCES user (id)
);