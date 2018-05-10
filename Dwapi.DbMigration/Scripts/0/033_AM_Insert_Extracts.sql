INSERT INTO extract (Id, Destination, Display, DocketId, EmrSystemId, ExtractSql, Ispriority, Name, Rank) 
VALUES 
('d40f5576-9cee-4af2-84a6-4a62d89921fd','PSmartStage','Smart Card','PSMART',(SELECT Id FROM emrsystem WHERE Name = 'IQCare' LIMIT 1),'select [Id],[shr],[date_created],[status],[status_date],[uuid] FROM [psmart_store] WHERE UPPER(Status) = ''PENDING''',1,'pSmart',1.00), 
('329bfbf4-d404-4dfd-88eb-6fb398c64249','dwhStage','All Patients','NDWH',(SELECT Id FROM emrsystem WHERE Name = 'IQCare' LIMIT 1),'SELECT 
	PatientID, PatientPK, FacilityID, SiteCode, FacilityName, SatelliteName, Gender, DOB, RegistrationDate, RegistrationAtCCC, RegistrationAtPMTCT, RegistrationAtTBClinic, PatientSource, Region, District, Village, 
	ContactRelation, LastVisit, MaritalStatus, EducationLevel, DateConfirmedHIVPositive, PreviousARTExposure, PreviousARTStartDate, StatusAtCCC, StatusAtPMTCT, StatusAtTBClinic, ''IQCare'' AS EMR, 
	''Kenya HMIS II'' AS Project, CAST(GETDATE() AS DATE) AS DateExtracted,newid() as ID
FROM            
	tmp_PatientMaster AS a WHERE a.RegistrationAtCCC IS NOT NULL',1,'PatientExtract',1.00);