CREATE TABLE  patientpharmacyextract (
	 Emr   nvarchar (150) NULL,
	 Project   nvarchar (150) NULL,
	 Processed   bit  NULL,
	 Id   char(38)  NOT NULL,
	 VisitID   int  NULL,
	 Drug   nvarchar (150) NULL,
	 Provider   nvarchar (150) NULL,
	 DispenseDate   datetime  NULL,
	 Duration   decimal (18, 2) NULL,
	 ExpectedReturn   datetime  NULL,
	 TreatmentType   nvarchar (150) NULL,
	 RegimenLine   nvarchar (150) NULL,
	 PeriodTaken   nvarchar (150) NULL,
	 ProphylaxisType   nvarchar (150) NULL,
	 PatientPK   int  NOT NULL,
	 PatientID   nvarchar (150) NULL,
	 SiteCode   int  NOT NULL,
	 QueueId   nvarchar (150) NULL,
	 Status   nvarchar (150) NULL,
	 StatusDate   datetime  NULL,
    
    primary key(Id),
    
    constraint FK_PatientPharmacyExtract_PatientExtract_PatientPK_SiteCode foreign key (PatientPK, SiteCode)
    references patientextract(PatientPK, SiteCode)
    ON DELETE CASCADE ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;