/*
   viernes, 12 de febrero de 201618:00:48
   User: sa
   Server: 200.0.236.210,5000
   Database: MSP-GeDoc-Desarrollo
   Application: 
*/

/* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION
GO
EXECUTE sp_rename N'dbo.catHCAduPraPatologicasResul.aduPraPatFecha', N'Tmp_aduPraPatFecha1_8', 'COLUMN' 
GO
EXECUTE sp_rename N'dbo.catHCAduPraPatologicasResul.aduPraPatResul', N'Tmp_aduPraPatResul1_9', 'COLUMN' 
GO
EXECUTE sp_rename N'dbo.catHCAduPraPatologicasResul.Tmp_aduPraPatFecha1_8', N'aduPraPatFecha1', 'COLUMN' 
GO
EXECUTE sp_rename N'dbo.catHCAduPraPatologicasResul.Tmp_aduPraPatResul1_9', N'aduPraPatResul1', 'COLUMN' 
GO
ALTER TABLE dbo.catHCAduPraPatologicasResul ADD
	aduPraPatFecha2 datetime NULL,
	aduPraPatResul2 varchar(250) NULL,
	aduPraPatFecha3 datetime NULL,
	aduPraPatResul3 varchar(250) NULL,
	aduPraPatFecha4 datetime NULL,
	aduPraPatResul4 varchar(250) NULL,
	aduPraPatFecha5 datetime NULL,
	aduPraPatResul5 varchar(250) NULL,
	aduPraPatFecha6 datetime NULL,
	aduPraPatResul6 varchar(250) NULL
GO
ALTER TABLE dbo.catHCAduPraPatologicasResul SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
