CREATE PROCEDURE [dbo].[PersonTransferPROC]
AS

BEGIN
	INSERT INTO fct.BPO_PERSON
	(
		[person_id]
      ,[date_year]
      ,[gender]
      ,[person_namex]
      ,[sanctions_descr]
      ,[sanctions_references]
      ,[country_code]
	)

    -- Insert statements for procedure here
	SELECT
	sf.person_id
      ,CONVERT(INT,sf.date_year)
      ,sf.gender
      ,sf.person_namex
      ,sf.sanctions_descr
      ,sf.sanctions_references
      ,sf.country_code
		

	FROM data.BPO_PERSON sf

END

