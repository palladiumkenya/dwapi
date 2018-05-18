CREATE TABLE ReportRunLog(
      Id char(38) NOT NULL,
      SiteCode int NULL,
      ReportRunDate date NULL,
      ReportName varchar(50) NULL,
      ReportADX varchar(5000) NULL,
      CreatedDate date NULL,
CONSTRAINT PK_ReportRunLog PRIMARY KEY CLUSTERED 
(
      Id ASC
)
);