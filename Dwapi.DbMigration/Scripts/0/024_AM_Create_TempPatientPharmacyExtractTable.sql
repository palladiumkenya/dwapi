CREATE TABLE temppatientpharmacyextract(
	Id int(11) NOT NULL auto_increment,
	VisitID int NULL,
	Drug nvarchar(150) NULL,
	Provider nvarchar(150) NULL,
	DispenseDate datetime NULL,
	Duration decimal(18, 2) NULL,
	ExpectedReturn datetime NULL,
	TreatmentType nvarchar(150) NULL,
	RegimenLine nvarchar(150) NULL,
	PeriodTaken nvarchar(150) NULL,
	ProphylaxisType nvarchar(150) NULL,
	Emr nvarchar(150) NULL,
	Project nvarchar(150) NULL,
	PatientPK int NULL,
	PatientID nvarchar(150) NULL,
	FacilityId int NULL,
	SiteCode int NULL,
	DateExtracted datetime NOT NULL,
	CheckError bit NOT NULL,
    
    primary key(Id)
    
) ENGINE=InnoDB DEFAULT CHARSET=utf8;