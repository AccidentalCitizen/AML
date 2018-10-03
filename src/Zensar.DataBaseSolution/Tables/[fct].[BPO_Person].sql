CREATE TABLE [fct].[BPO_Person](
	[ID] [int] IDENTITY(1,1) PRIMARY KEY NOT NULL,
	[person_id] [varchar](max) NOT NULL,
	[date_year] [int] NOT NULL,
	[gender] [varchar](max) NOT NULL,
	[person_namex] [varchar](max) NOT NULL,
	[sanctions_descr] [varchar](max) NOT NULL,
	[sanctions_references] [varchar](max) NOT NULL,
	[country_code] [varchar](max) NOT NULL
);