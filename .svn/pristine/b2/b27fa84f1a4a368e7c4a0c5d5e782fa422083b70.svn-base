USE [GesLiMed_Desarrollo]
GO
/****** Object:  StoredProcedure [dbo].[getEstadisticaDCRM]    Script Date: 22/07/2016 02:58:52 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[getDatosTableroDeComandoDCRM]
/*-------------------------------------------------------------------------------
* Autor: MC
* Creado: 22/07/2016
* Descripción: Proporciona la informacion necesaria para el armado del tablero de comando
* Historial de Revisiones:
* Fecha       Autor       Descripción
* ---------------------------------------------------------------------------------
* 22/07/2016  MC         Implementación Inicial.
* 
* ---------------------------------------------------------------------------------*/
As

SET FMTONLY OFF

DECLARE @fecDesde DATETIME, @fecHasta DATETIME
SET @fecDesde=DATEADD(DAY, DATEDIFF(DAY, '19000101', GETDATE()), '19000101')
SET @fecHasta=DATEADD(DAY, DATEDIFF(DAY, '18991231', GETDATE()), '19000101')

Select c.cmedId, cmedFecha, espNombre
	, (Select Top 1 usrApellido + ' ' + usrNombre From gesCartaMedicaEstado es Inner Join sisUsuario us On(us.usrId = es.usrId And es.cmedId = c.cmedId And es.estEstado In('AA', 'PE'))) As Medico
	, (Select Top 1 es.usrId From gesCartaMedicaEstado es Inner Join sisUsuario us On(us.usrId = es.usrId And es.cmedId = c.cmedId And es.estEstado In('AA', 'PE'))) As UsuarioMedico
	, (Select Min(estFecha) From gesCartaMedicaEstado es Where(es.cmedId = c.cmedId And es.estEstado = 'EM')) As Emitida
	, (Select Min(estFecha) From gesCartaMedicaEstado es Where(es.cmedId = c.cmedId And es.estEstado = 'ES')) As EnEspera
	, (Select Min(estFecha) From gesCartaMedicaEstado es Where(es.cmedId = c.cmedId And es.estEstado = 'LA')) As ListaParaAtencion
	, (Select Min(estFecha) From gesCartaMedicaEstado es Where(es.cmedId = c.cmedId And es.estEstado = 'AM')) As AtencionMedica
	, (Select Max(estFecha) From gesCartaMedicaEstado es Where(es.cmedId = c.cmedId And es.estEstado In('AA', 'PE'))) As Atendido
	, u.estEstado
Into #Detalle
From gesCartaMedica c
	Inner Join gesCartaMedicaUltimoEstado u On(u.cmedId = c.cmedId)
	Inner Join catEspecialidad e On(e.espId = c.espId)
Where Convert(VarChar(10),cmedFecha,112) BetWeen @fecDesde And @fecHasta
	And u.estEstado != 'AN'


Select d.*, e.espNombre As EspecialidadMedico, (Select Top 1 AtencionMedica 
												From #Detalle d1 
												Where d1.UsuarioMedico = d.UsuarioMedico And d1.cmedId <> d.cmedId And Convert(Date,d1.cmedFecha) = Convert(Date,d.cmedFecha) And d1.Atendido < d.Atendido 
												Order By Atendido Desc) As Ocio
Into #DetalleCompleto
From #Detalle d
	Inner Join catMedicos m On(m.usrId = d.UsuarioMedico)
	Inner Join catEspecialidad e On(e.espId = m.espId)
Order By Medico, Atendido

Select d.*
	, Convert(Date,cmedFecha) As Fecha
	, DateDiff(minute,EnEspera, AtencionMedica) As TiempoDeEspera
	, DateDiff(minute,AtencionMedica, Atendido) As TiempoDeAtencion
	, DateDiff(minute, IsNull(Ocio,DateAdd(minute,-10,Atendido)),Atendido) As TiempoOcioso
--Into #Reporte
From #DetalleCompleto d
Order By Medico, Atendido

/*
Drop Table #Detalle
*/