create table emrsystem (
	Id char(38) NOT NULL,
	IsDefault bit NOT NULL,
	IsMiddleware bit NOT NULL,
	Name nvarchar(100) NULL,
	Version nvarchar(50) NULL,
    
    primary key(Id)
)
