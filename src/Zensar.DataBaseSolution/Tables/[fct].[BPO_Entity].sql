CREATE TABLE [fct].[BPO_Entity](
	[ID] [int] IDENTITY(1,1) PRIMARY KEY NOT NULL,
	[entity_id] [varchar](max) NOT NULL,
	[entity_name] [varchar](max) NOT NULL,
	[sanctions_descr] [varchar](max) NOT NULL,
	[sanctions_references] [varchar](max) NOT NULL,
	[company_country] [varchar](max) NOT NULL
);