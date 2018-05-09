CREATE TABLE patientstatusextract (
	 Emr   nvarchar (150) NULL,
	 Project   nvarchar (150) NULL,
	 Processed   bit  NULL,
	 Id   char(38)  NOT NULL,
	 ExitDescription   nvarchar (150) NULL,
	 ExitDate   datetime  NULL,
	 ExitReason   nvarchar (150) NULL,
	 PatientPK   int  NOT NULL,
	 PatientID   nvarchar (150) NULL,
	 SiteCode   int  NOT NULL,
	 QueueId   nvarchar (150) NULL,
	 `Status`   nvarchar (150) NULL,
	 StatusDate   datetime  NULL,
    
    primary key(Id),
    
    constraint FK_PatientStatusExtract_PatientExtract_PatientPK_SiteCode foreign key (PatientPK, SiteCode)
    references patientextract(PatientPK, SiteCode)
    ON DELETE CASCADE ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;