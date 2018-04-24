CREATE TABLE  patientartextract (
	 Emr   nvarchar (150) NULL,
	 Project   nvarchar (150) NULL,
	 Processed   bit  NULL,
	 Id   char(38)  NOT NULL,
	 DOB   datetime  NULL,
	 AgeEnrollment   decimal (18, 2) NULL,
	 AgeARTStart   decimal (18, 2) NULL,
	 AgeLastVisit   decimal (18, 2) NULL,
	 RegistrationDate   datetime  NULL,
	 Gender   nvarchar (150) NULL,
	 PatientSource   nvarchar (150) NULL,
	 StartARTDate   datetime  NULL,
	 PreviousARTStartDate   datetime  NULL,
	 PreviousARTRegimen   nvarchar (150) NULL,
	 StartARTAtThisFacility   datetime  NULL,
	 StartRegimen   nvarchar (150) NULL,
	 StartRegimenLine   nvarchar (150) NULL,
	 LastARTDate   datetime  NULL,
	 LastRegimen   nvarchar (150) NULL,
	 LastRegimenLine   nvarchar (150) NULL,
	 Duration   decimal (18, 2) NULL,
	 ExpectedReturn   datetime  NULL,
	 Provider   nvarchar (150) NULL,
	 LastVisit   datetime  NULL,
	 ExitReason   nvarchar (150) NULL,
	 ExitDate   datetime  NULL,
	 PatientPK   int  NOT NULL,
	 PatientID   nvarchar (150) NULL,
	 SiteCode   int  NOT NULL,
	 QueueId   nvarchar (150) NULL,
	 Status   nvarchar (150) NULL,
	 StatusDate   datetime  NULL,
    
    primary key (Id),
    
    constraint FK_PatientArtExtract_PatientExtract_PatientPK_SiteCode foreign key (PatientPK, SiteCode)
    references patientextract(PatientPK, SiteCode)
    ON DELETE CASCADE ON UPDATE NO ACTION
    
) ENGINE=InnoDB DEFAULT CHARSET=utf8;