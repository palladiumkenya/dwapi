CREATE TABLE MasterPatientIndexExtract
(
	RowId int auto_increment NOT NULL,
	Serial varchar(50) NULL,
	FirstName varchar(100) NULL,
	MiddleName varchar(100) NULL,
	LastName varchar(100) NULL,
	FirstName_Normalized varchar(100) NULL,
	MiddleName_Normalized varchar(100) NULL,
	LastName_Normalized varchar(100) NULL,
	PatientPhoneNumber varchar(100) NULL,
	PatientAlternatePhoneNumber varchar(100) NULL,
	Gender varchar(10) NOT NULL,
	DOB date NOT NULL,
	MaritalStatus varchar(100) NULL,
	PatientSource varchar(100) NULL,
	PatientCounty varchar(100) NULL,
	PatientSubCounty varchar(250) NULL,
	PatientVillage varchar(250) NULL,
	PatientID varchar(50) NULL,
	NationalID varchar(50) NULL,
	NHIFNumber varchar(50) NULL,
	BirthCertificate varchar(50) NULL,
	CCCNumber varchar(50) NULL,
	TBNumber varchar(50) NULL,
	ContactName varchar(100) NULL,
	ContactRelation varchar(250) NULL,
	ContactPhoneNumber varchar(50) NULL,
	ContactAddress varchar(250) NULL,
	DateConfirmedHIVPositive date NULL,
	StartARTDate date NULL,
	StartARTRegimenCode varchar(100) NULL,
	StartARTRegimenDesc varchar(100) NULL,
 CONSTRAINT MasterPatientIndexExtract PRIMARY KEY CLUSTERED 
(
	RowId ASC
)
);