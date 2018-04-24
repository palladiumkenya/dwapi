CREATE TABLE temppatientlaboratoryextract(
	Id int(11) NOT NULL auto_increment,
	FacilityName nvarchar(150) NULL,
	SatelliteName nvarchar(150) NULL,
	Emr nvarchar(150) NULL,
	Project nvarchar(150) NULL,
	VisitId int NULL,
	OrderedByDate datetime NULL,
	ReportedByDate datetime NULL,
	TestName nvarchar(150) NULL,
	EnrollmentTest int NULL,
	TestResult nvarchar(150) NULL,
	PatientPK int NULL,
	PatientID nvarchar(150) NULL,
	FacilityId int NULL,
	SiteCode int NULL,
	DateExtracted datetime NOT NULL,
	CheckError bit NOT NULL,
    
    primary key(Id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;