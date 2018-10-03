CREATE PROCEDURE [fct].[EntityTransferPROC]
AS

BEGIN
	INSERT INTO fct.BPO_Entity
	(
      [entity_id]
      ,[entity_name]
      ,[sanctions_descr]
      ,[sanctions_references]
      ,[company_country]
	)

    -- Insert statements for procedure here
	SELECT
      sf.entity_id
      ,sf.entity_name
      ,sf.sanctions_descr
      ,sf.sanctions_references
      ,sf.company_country
		

	FROM data.BPO_Entity sf

END
