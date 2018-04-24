CREATE TABLE extractHistory(
	Id char(38) NOT NULL,
	ExtractId char(38) NOT NULL,
	Stats int NULL,
	`Status` int NOT NULL,
	StatusDate datetime(6) NULL,
	StatusInfo nvarchar(500) NULL,
    
    primary key (Id),
    
    constraint FK_extracthistory_extractId_extract_id foreign key
    (ExtractId) references `extract`(id)
    ON DELETE CASCADE ON UPDATE NO ACTION
);