CREATE TABLE `extract`(
	Id char(38) NOT NULL,
	Destination nvarchar(500) NULL,
	Display nvarchar(100) NULL,
	DocketId nvarchar(50) NULL,
	EmrSystemId char(38) NOT NULL,
	ExtractSql nvarchar(5000) NULL,
	IsPriority bit NOT NULL,
	`Name` nvarchar(100) NULL,
	`Rank` decimal(18, 2) NOT NULL,
    
    primary key (Id),
    
    constraint FK_extract_emrsystemId_emrsystem_Id foreign key 
    (EmrSystemId) references emrsystem(Id)
    ON DELETE CASCADE ON UPDATE NO ACTION,
    
    constraint FK_extract_docketid_docket_id foreign key
    (DocketId) references docket(Id)
    ON DELETE CASCADE ON UPDATE NO ACTION
);