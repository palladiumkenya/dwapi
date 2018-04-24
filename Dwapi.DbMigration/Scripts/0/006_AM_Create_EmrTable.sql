CREATE TABLE emr(
	Id char(38) NOT NULL,
	ConnectionKey nvarchar(500) NULL,
	IsDefault bit NOT NULL,
	`Name` nvarchar(500) NULL,
	ProjectId char(38) NOT NULL,
	Version nvarchar(500) NULL,
    
    primary key (Id),
    
    constraint fk_emr_project_id foreign key
    (ProjectId) references  project(Id)
    ON DELETE CASCADE ON UPDATE NO ACTION
);