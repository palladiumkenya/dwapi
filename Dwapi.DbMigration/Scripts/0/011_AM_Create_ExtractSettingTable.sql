CREATE TABLE extractSetting(
	Id char(38) NOT NULL,
	Destination nvarchar(500) NULL,
	Display nvarchar(500) NULL,
	EmrId char(38) NOT NULL,
	ExtractCsv nvarchar(5000) NULL,
	ExtractSql nvarchar(5000) NULL,
	IsActive bit NOT NULL,
	IsPriority bit NOT NULL,
	`Name` nvarchar(500) NULL,
	Rank decimal(18, 2) NOT NULL,
    
    primary key (Id),
    
    constraint FK_extractsetting_emrid_emr_id foreign key 
    (EmrId) references emr(Id)
    ON DELETE CASCADE ON UPDATE NO ACTION
);