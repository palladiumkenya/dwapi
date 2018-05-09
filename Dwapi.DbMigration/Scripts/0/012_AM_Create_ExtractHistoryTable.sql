CREATE TABLE eventHistory(
	Id char(38) NOT NULL,
	Display nvarchar(500) NULL,
	ExtractSettingId char(38) NOT NULL,
	`Found` int NULL,
	FoundDate datetime(6) NULL,
	FoundStatus nvarchar(500) NULL,
	ImportDate datetime(6) NULL,
	ImportStatus nvarchar(500) NULL,
	Imported int NULL,
	IsFoundSuccess bit NULL,
	IsImportSuccess bit NULL,
	IsLoadSuccess bit NULL,
	IsSendSuccess bit NULL,
	LoadDate datetime(6) NULL,
	LoadStatus nvarchar(500) NULL,
	Loaded int NULL,
	NotImported int NULL,
	NotSent int NULL,
	Rejected int NULL,
	SendDate datetime(6) NULL,
	SendStatus nvarchar(500) NULL,
	Sent int NULL,
	SiteCode int NULL,
    
    primary key (Id),
    
    constraint FK_eventhistory_extractsettingid_extractsetting_id foreign key 
    (ExtractSettingId) references extractsetting(Id)
    ON DELETE CASCADE ON UPDATE NO ACTION
);