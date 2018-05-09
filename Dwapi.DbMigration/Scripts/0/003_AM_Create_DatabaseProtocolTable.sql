CREATE TABLE `databaseprotocol`(
	`Id` CHAR(38) NOT NULL,
	`AdvancedProperties` nvarchar(100) NULL,
	`DatabaseName` nvarchar(100) NULL,
    `DatabaseType` int NOT NULL,
	`EmrSystemId` char(38) NOT NULL,
	`Host` nvarchar(100) NULL,
	`Password` nvarchar(100) NULL,
	`Port` int NULL,
	`Username` nvarchar(100) NULL,
    
    primary key (`Id`),
    key `FK_EmrSystems_Id_idx` (EmrSystemId),
    constraint `FK_DatabaseProtocol_EmrSystems_Id` 
    foreign key (EmrSystemId) references emrsystem(Id)
    ON DELETE CASCADE ON UPDATE NO ACTION
);
