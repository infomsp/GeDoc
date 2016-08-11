USE [MSP-GeDoc]
GO
/****** Object:  StoredProcedure [dbo].[spPases]    Script Date: 01/19/2015 20:38:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Drop Procedure [dbo].[getExpedienteUltimaUbicacion]
Go
Create Procedure [dbo].[getExpedienteUltimaUbicacion]
(@NroExpediente Int)
As
Begin
	Declare @Resultado Table(	origenAlias		VarChar(50)	Null,
								destinoAlias	VarChar(50)	Null,
								[Id] [int] NOT NULL,
								[expedienteId] [int] NULL,
								[fecha] [smalldatetime] NULL,
								[origenFecha] [datetime] NULL,
								[origenUsuario] [int] NULL,
								[origenCodigo] [int] NULL,
								[origenNombre] [nvarchar](100) NULL,
								[origenFolios] [int] NULL,
								[destinoFecha] [datetime] NULL,
								[destinoUsuario] [int] NULL,
								[destinoCodigo] [int] NULL,
								[destinoNombre] [nvarchar](100) NULL,
								[destinoFolios] [int] NULL,
								[ordenId] [int] NULL,
								[remitoId] [int] NULL	)

	Declare @Consulta VarChar(200)
	--Declare @NroExpediente Int
	--Set @NroExpediente = 108896
	Set @Consulta = 'Select Top 1 * From OpenQuery([10.64.65.235], ''Select * From MSP..fnPases(' + Convert(VarChar(20),@NroExpediente) + ')'') Order By origenFecha Desc'

	Insert Into @Resultado
	Execute(@Consulta)

	Select destinoNombre As Ubicacion,
		DateDiff(dd,destinoFecha, GetDate()) - (dbo.fnContarDias(destinoFecha, GetDate(), 'sÁbado') * 2) - (Select Count(*) From catFeriado Where Convert(Date,ferFecha) BetWeen Convert(Date, destinoFecha) And Convert(Date, GetDate())) As DiasHabiles,
		DateDiff(dd,destinoFecha, GetDate()) As DiasCorridos
	From @Resultado
	
	Return
End
Go

getExpedienteUltimaUbicacion 130967

Select dbo.fnContarDias('20150101', GetDate(), 'sÁbado')
Select DateDiff(dd,'20150107', GetDate())

Drop Function fnContarDias
Go
Create Function fnContarDias
(@FechaInicial DateTime, @FechaFinal DateTime, @DiaSemana VarChar(15))
Returns Int
As
Begin
	Declare @DiaDeLaSemana SmallInt
	Declare @Resultado Int
	
	Select @DiaDeLaSemana = 
			Case When Upper(@DiaSemana) = 'DOMINGO' Then 0
				When Upper(@DiaSemana) = 'LUNES' Then 1
				When Upper(@DiaSemana) = 'MARTES' Then 2
				When Upper(@DiaSemana) In('MIÉRCOLES', 'MIERCOLES') Then 3
				When Upper(@DiaSemana) = 'JUEVES' Then 4
				When Upper(@DiaSemana) = 'VIERNES' Then 5
				When Upper(@DiaSemana) In('SÁBADO', 'SABADO') Then 6
			End
	
	Set @Resultado = Convert(Int,(DateDiff(dd, @FechaInicial, @FechaFinal) - DatePart(dw,@FechaFinal - @DiaDeLaSemana) + 8) / 7)

	Return @Resultado
End


Select * From catFeriado


Drop Procedure getExpediente
Go
Create Procedure getExpediente
(@prefijo Int, @sufijo Int, @ciclo Int, @IdExpediente Int)
As
	Begin
		Declare @SQL VarChar(Max)
		--Declare @prefijo Int, @sufijo Int, @ciclo Int
		--Select @prefijo = 0, @sufijo = 16009, @ciclo = 2010
		
		If @IdExpediente <= 0
			Begin
				Set @SQL = 'SELECT Id, tipo, prefijo, sufijo, ciclo, clave, fecha, iniciadorCodigo, iniciadorNombre, extracto, comentarios, acceso, folios, nombredestino, codigodestino
							FROM OPENQUERY([10.64.65.235], ''Select * From MSP..expExpedientes Where prefijo = ' + Convert(VarChar(10),@prefijo) + ' And sufijo = ' + Convert(VarChar(10),@sufijo) + ' And ciclo = ' + Convert(VarChar(10),@ciclo) + ''')'
			End
		Else
			Begin
				Set @SQL = 'SELECT Id, tipo, prefijo, sufijo, ciclo, clave, fecha, iniciadorCodigo, iniciadorNombre, extracto, comentarios, acceso, folios, nombredestino, codigodestino
							FROM OPENQUERY([10.64.65.235], ''Select * From MSP..expExpedientes Where id = ' + Convert(VarChar(10),@IdExpediente) + ''')'
			End

		Execute(@SQL)
	End
Go

Drop Procedure getContarDiasHabiles
Go
Create Procedure getContarDiasHabiles
(@FechaInicial DateTime, @FechaFinal DateTime)
As
Begin
	Select DateDiff(dd,@FechaInicial, @FechaFinal) - (dbo.fnContarDias(@FechaInicial, @FechaFinal, 'sÁbado') * 2) - (Select Count(*) From catFeriado Where Convert(Date,ferFecha) BetWeen Convert(Date, @FechaInicial) And Convert(Date, @FechaInicial))
End
Go

Select dbo.fnContarDiasHabiles('20150107', GetDate())

Select * From proCompra

Execute procActualizaUltimaUbicacionExpediente

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Drop Procedure [dbo].[procActualizaUltimaUbicacionExpediente]
Go
Create Procedure [dbo].[procActualizaUltimaUbicacionExpediente]
As
Begin
	SET NOCOUNT ON
	Declare @Resultado Table(	origenAlias		VarChar(50)	Null,
								destinoAlias	VarChar(50)	Null,
								[Id] [int] NOT NULL,
								[expedienteId] [int] NULL,
								[fecha] [smalldatetime] NULL,
								[origenFecha] [datetime] NULL,
								[origenUsuario] [int] NULL,
								[origenCodigo] [int] NULL,
								[origenNombre] [nvarchar](100) NULL,
								[origenFolios] [int] NULL,
								[destinoFecha] [datetime] NULL,
								[destinoUsuario] [int] NULL,
								[destinoCodigo] [int] NULL,
								[destinoNombre] [nvarchar](100) NULL,
								[destinoFolios] [int] NULL,
								[ordenId] [int] NULL,
								[remitoId] [int] NULL	)
	
	Declare @Ubicacion		VarChar(250),
			@DiasHabiles	Int			,
			@DiasCorridos	Int			,
			@IdExpediente	Int			,
			@comId			BigInt
	Declare @Consulta VarChar(200)
	
	Declare curActualizaExpedientes Cursor For Select comId, IdExpediente, expUbicacion, expDiasCorridos, expDiasHabiles
											From proCompra
	Open curActualizaExpedientes
	Fetch curActualizaExpedientes Into @comId, @IdExpediente, @Ubicacion, @DiasCorridos, @DiasHabiles
	While @@fetch_status = 0
		Begin
			Delete @Resultado
			
			Set @Consulta = 'Select Top 1 * From OpenQuery([10.64.65.235], ''Select * From MSP..fnPases(' + Convert(VarChar(20),@IdExpediente) + ')'') Order By origenFecha Desc'

			Insert Into @Resultado
			Execute(@Consulta)

			Update proCompra
			Set expUbicacion = destinoNombre,
				expDiasHabiles = DateDiff(dd,destinoFecha, GetDate()) - (dbo.fnContarDias(destinoFecha, GetDate(), 'sÁbado') * 2) - (Select Count(*) From catFeriado Where Convert(Date,ferFecha) BetWeen Convert(Date, destinoFecha) And Convert(Date, GetDate())),
				expDiasCorridos = DateDiff(dd,destinoFecha, GetDate())
			From @Resultado
			Where comId = @comId

			Fetch curActualizaExpedientes Into @comId, @IdExpediente, @Ubicacion, @DiasCorridos, @DiasHabiles
		End
	Close curActualizaExpedientes
	Deallocate curActualizaExpedientes
	
	Return
End
Go

---- Corrige datos existentes ----
Select * From vwExpedientes e, proCompra c Where Convert(VarChar(10),prefijo) + '-' + Right('00000' + Convert(VarChar(10),sufijo),5) + '-' + Convert(VarChar(10),ciclo) = comComprobante

Update proCompra
Set idExpediente = Id
From vwExpedientes e Where Right('000' + Convert(VarChar(10),prefijo),3) + '-' + Right('00000' + Convert(VarChar(10),sufijo),5) + '-' + Convert(VarChar(10),ciclo) = comComprobante

Select * From proCompra

spPases 118637
getExpedienteUltimaUbicacion 131195
ActualizaUbicacionExpedientes
Alter Table proCompra
	Add idExpediente Int Null,
		expUbicacion VarChar(Max) Null,
		expDiasHabiles Int Null,
		expDiasCorridos Int Null
Go
