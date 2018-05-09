CREATE TABLE psmartStage(
	EId char(38) NOT NULL,
	DateExtracted datetime(6) NULL,
	DateSent datetime(6) NULL,
	DateStaged datetime(6) NOT NULL,
	Date_Created datetime(6) NULL,
	Emr nvarchar(500) NULL,
	Id int NULL,
	RequestId nvarchar(500) NULL,
	Shr nvarchar(500) NULL,
	Status nvarchar(100) NULL,
	Status_Date datetime(6) NULL,
	Uuid nvarchar(500) NULL,
	
    primary key (EId)
    
);