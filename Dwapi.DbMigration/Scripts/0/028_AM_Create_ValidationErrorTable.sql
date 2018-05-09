CREATE TABLE validationError(
	Id char(38) NOT NULL,
	EntityName nvarchar(500) NULL,
	ErrorMessage nvarchar(1000) NULL,
	FieldName nvarchar(500) NULL,
	ReferencedEntityId nvarchar(500) NULL,
	ValidatorId char(38) NOT NULL,
    
    primary key (Id),
    
    constraint FK_ValidationError_ValidatorId_Validator_Id foreign key
    (ValidatorId) references Validator(Id)
    
) ENGINE=InnoDB DEFAULT CHARSET=utf8;