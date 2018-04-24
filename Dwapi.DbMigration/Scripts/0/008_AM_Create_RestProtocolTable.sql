CREATE TABLE restprotocol(
	Id char(38) NOT NULL,
	AuthToken nvarchar(100) NULL,
	EmrSystemId char(38) NOT NULL,
	Url nvarchar(100) NULL,
    
    primary key(Id),
    
    constraint FK_RestProtocol_EmrSystem_Id 
    foreign key (EmrSystemId) references emrsystem(Id)
    ON DELETE CASCADE ON UPDATE NO ACTION
    
);