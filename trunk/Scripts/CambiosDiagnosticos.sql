/****** Script for SelectTopNRows command from SSMS  ******/
SELECT TOP 1000 [DiagnosticoID]
      ,[Descripcion]
      ,[TipoDiagnosticoID]
      ,[Aud_UsuarioAlta]
      ,[Aud_FechaAlta]
      ,[Aud_UsuarioModi]
      ,[Aud_FechaModi]
      ,[Indice]
      ,[diagId]
  FROM [catDiagnostico]
Select * From catDiagnosticoAgrupamiento

Insert Into dbo.catDiagnosticoAgrupamiento
Select Distinct TipoDiagnosticoID From [MSP-GeDoc].dbo.catDiagnostico 

Insert Into dbo.catDiagnosticoTipo
Select CIE10TipoDiagnostico1ID, DesdeDiagnosticoID, HastaDiagnosticoID, Descripcion, 1 From [MSP-GeDoc].dbo.catCIE10TipoDiagnostico1

Insert Into dbo.catDiagnosticoSubTipo
Select CIE10TipoDiagnostico2ID, DesdeDiagnosticoID, HastaDiagnosticoID, Descripcion,
(Select Top 1 tdId From catDiagnosticoTipo t
	Where 
		Convert(VarChar(10),Ascii(Left(a.DesdeDiagnosticoID,1))) + Right(a.DesdeDiagnosticoID,2)
		BetWeen Convert(VarChar(10),Ascii(Left(t.tdCodigoInicial,1))) + Right(t.tdCodigoInicial,2)
		And Convert(VarChar(10),Ascii(Left(t.tdCodigoFinal,1))) + Right(t.tdCodigoFinal,2)
)
From [MSP-GeDoc].dbo.catCIE10TipoDiagnostico2 a

Select diagId, DiagnosticoID, Descripcion,
(Select Top 1 subId From catDiagnosticoSubTipo t
	Where 
		Convert(VarChar(10),Ascii(Left(a.DiagnosticoID,1))) + SubString(a.DiagnosticoID,2,2)
		BetWeen Convert(VarChar(10),Ascii(Left(t.subCodigoInicial,1))) + Right(t.subCodigoInicial,2)
		And Convert(VarChar(10),Ascii(Left(t.subCodigoFinal,1))) + Right(t.subCodigoFinal,2)
) As subId
Into #Diagnosticos
From [MSP-GeDoc].dbo.catDiagnostico a
Where TipoDiagnosticoID = 'CIE10' 

Insert Into dbo.catDiagnosticoPadron
Select * From #Diagnosticos Where Not subId Is Null


Insert Into dbo.catDiagnosticoTipo
Values(23, 'ZZZ', 'ZZZ', 'GENERAL CEPS_AP', 2)

Insert Into dbo.catDiagnosticoSubTipo
Values(261, 'ZZZ', 'ZZZ', 'GENERAL CEPS_AP', 23)

Insert Into dbo.catDiagnosticoPadron
Select diagId, DiagnosticoID, Descripcion, 261
From [MSP-GeDoc].dbo.catDiagnostico a
Where TipoDiagnosticoID = 'CEPS_AP' 


Update catPractica
Set pracUop = 1
Where pracUop Is Null
Go

Update catPractica
Set pracCosto = 0
Where pracCosto Is Null
Go

Alter Table catPractica Alter Column pracCodigo VarChar(10) Not Null
Go

Alter Table catPractica Alter Column pracDescripcion VarChar(250) Not Null
Go

Alter Table catPractica Alter Column pracCosto Decimal(15,2) Not Null
Go

Alter Table catPractica Alter Column pracUop Decimal(15,2) Not Null
Go

ALTER TABLE dbo.catPractica ADD CONSTRAINT
	UQ_catPractica_pracCodigo_nomId UNIQUE NONCLUSTERED 
	(
	pracCodigo,
	nomId
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO

Alter Table enlTurnoPractica Add pracCosto Decimal(15,2) Null
Go
Alter Table enlTurnoPractica Add pracUop Decimal(15,2) Null
Go
Alter Table enlTurnoPractica Add turpracCantidad SmallInt Null
Go

Update enlTurnoPractica Set turpracCantidad = 0, pracCosto = 0, pracUop = 0
Go


Select Max(tdId) From catDiagnosticoTipo
Select Max(subId) From catDiagnosticoSubTipo
Select * From catDiagnosticoPadron
Select Descripcion, Count(*) From #Diagnosticos Where Not subId Is Null Group By Descripcion Having Count(*) > 1
Select * From #Diagnosticos d Where Not Exists(Select 1 From catDiagnosticoSubTipo s Where s.subId = d.subId) And Not subId Is Null
Select * From #Diagnosticos Where Descripcion = 'ASFIXIA'
Select * From catDiagnosticoSubTipo

Select * From dbo.catDiagnosticoPadron
Select * From [MSP-GeDoc].dbo.catCIE10TipoDiagnostico2
Select * From [MSP-GeDoc].dbo.catDiagnostico Where TipoDiagnosticoID = 'CIE10'
Select * From catDiagnosticoAgrupamiento


 