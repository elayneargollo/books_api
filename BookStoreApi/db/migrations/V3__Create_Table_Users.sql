CREATE TABLE IF NOT EXISTS `user` (
  `id` bigint NOT NULL AUTO_INCREMENT,
  `user_name` varchar(100) NOT NULL,
  `password` VARCHAR(130) NOT NULL,
  `role` VARCHAR(130) NOT NULL,
  PRIMARY KEY (`id`)
);