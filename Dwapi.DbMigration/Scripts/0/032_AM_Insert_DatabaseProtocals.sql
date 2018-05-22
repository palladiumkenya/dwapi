INSERT INTO databaseprotocol (Id, AdvancedProperties, DatabaseName, DatabaseType, EmrSystemId, host, Password, Port, Username) 
VALUES 
('f01a84c9-58b1-4b80-999b-12b271abf1c1','','IQTools_KeHMIS',1,(SELECT Id FROM emrsystem WHERE Name = 'IQCare' LIMIT 1),'192.168.1.235\\sqlexpress','c0nstella',null,'sa'),
('abd8dde9-d77b-428b-b058-042652abe114','','openmrs',2,(SELECT Id FROM emrsystem WHERE Name = 'KenyaEMR' LIMIT 1),'192.168.1.45','thisistest',3306,'dwapi');