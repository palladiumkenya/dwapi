CREATE TABLE `centralregistry` (
  `Id` char(38) NOT NULL,
  `AuthToken` varchar(100) DEFAULT NULL,
  `Name` varchar(100) DEFAULT NULL,
  `SubscriberId` varchar(50) DEFAULT NULL,
  `Url` varchar(100) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

