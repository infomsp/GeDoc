USE [MSP-GeDoc-Desarrollo]
GO
/****** Object:  StoredProcedure [dbo].[getListadoHCAdultos]    Script Date: 13/01/2016 17:32:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Procedure [dbo].[getListadoHCAdultos]
(@pacId BigInt)
/*-------------------------------------------------------------------------------
* Autor: 
* Creado: 13/01/2016
* Descripción: Carga los datos HC Adultos de un paciente.
* Historial de Revisiones:
* Fecha       Autor     Descripción
* ---------------------------------------------------------------------------------
* 13/01/2016         Implementación Inicial.
* ---------------------------------------------------------------------------------
* Execute getListadoHCPerinatales
* ---------------------------------------------------------------------------------*/
As
	Begin
		Select	hcaduid, pacid, Fecha --activa, 
		From catHCAdulto
		Where pacId = @pacId
	End
