CREATE TABLE validator(
	Id char(38) NOT NULL,
	Extract nvarchar(500) NULL,
	Field nvarchar(500) NULL,
	Logic nvarchar(500) NULL,
	Summary nvarchar(500) NULL,
	Type nvarchar(500) NULL,
    
    primary key(Id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;