Insert Into sisAccion
Values('Norma Legal', '-64px -190px;')
Go

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_proDecretoExpedientes_catZona]') AND parent_object_id = OBJECT_ID(N'[dbo].[proDecretoExpedientes]'))
ALTER TABLE [dbo].[proDecretoExpedientes] DROP CONSTRAINT [FK_proDecretoExpedientes_catZona]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_proDecretoExpedientes_proDecreto]') AND parent_object_id = OBJECT_ID(N'[dbo].[proDecretoExpedientes]'))
ALTER TABLE [dbo].[proDecretoExpedientes] DROP CONSTRAINT [FK_proDecretoExpedientes_proDecreto]
GO

/****** Object:  Table [dbo].[proDecreto]    Script Date: 07/19/2012 07:23:18 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proDecreto]') AND type in (N'U'))
DROP TABLE [dbo].[proDecreto]
GO

/****** Object:  Table [dbo].[proDecretoExpedientes]    Script Date: 07/19/2012 07:23:18 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proDecretoExpedientes]') AND type in (N'U'))
DROP TABLE [dbo].[proDecretoExpedientes]
GO

/****** Object:  Table [dbo].[proDecreto]    Script Date: 07/19/2012 07:23:18 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[proDecreto](
	[decId] [bigint] IDENTITY(1,1) NOT NULL,
	[decNumero] [smallint] NULL,
	[decFecha] [datetime] NULL,
	[decConsiderando] [varchar](max) NULL,
	[decResuelve] [varchar](max) NULL,
	[decLinkArchivo] [varchar](max) NULL,
 CONSTRAINT [PK_proDecreto] PRIMARY KEY CLUSTERED 
(
	[decId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
 CONSTRAINT [UQ_proDecreto_decId] UNIQUE NONCLUSTERED 
(
	[decId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

/****** Object:  Table [dbo].[proDecretoExpedientes]    Script Date: 07/19/2012 07:23:19 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[proDecretoExpedientes](
	[deceId] [bigint] IDENTITY(1,1) NOT NULL,
	[zonId] [int] NOT NULL,
	[deceExpedienteNumero] [int] NOT NULL,
	[deceExpedienteAnio] [smallint] NOT NULL,
	[decId] [bigint] NOT NULL,
 CONSTRAINT [PK_proDecretoExpedientes] PRIMARY KEY CLUSTERED 
(
	[deceId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
 CONSTRAINT [UQ_proDecretoExpedientes_deceId] UNIQUE NONCLUSTERED 
(
	[deceId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER TABLE [dbo].[proDecretoExpedientes]  WITH CHECK ADD  CONSTRAINT [FK_proDecretoExpedientes_catZona] FOREIGN KEY([zonId])
REFERENCES [dbo].[catZona] ([zonId])
GO

ALTER TABLE [dbo].[proDecretoExpedientes] CHECK CONSTRAINT [FK_proDecretoExpedientes_catZona]
GO

ALTER TABLE [dbo].[proDecretoExpedientes]  WITH CHECK ADD  CONSTRAINT [FK_proDecretoExpedientes_proDecreto] FOREIGN KEY([decId])
REFERENCES [dbo].[proDecreto] ([decId])
GO

ALTER TABLE [dbo].[proDecretoExpedientes] CHECK CONSTRAINT [FK_proDecretoExpedientes_proDecreto]
GO

/****** Object:  Table [dbo].[proDecretoResoluciones]    Script Date: 07/19/2012 07:23:19 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[proDecretoResoluciones](
	[drId] [bigint] IDENTITY(1,1) NOT NULL,
	[resId] [bigint] NOT NULL,
	[decId] [bigint] NOT NULL,
 CONSTRAINT [PK_proDecretoResoluciones] PRIMARY KEY CLUSTERED 
(
	[drId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
 CONSTRAINT [UQ_proDecretoResoluciones_drId] UNIQUE NONCLUSTERED 
(
	[drId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[proDecretoResoluciones]  WITH CHECK ADD  CONSTRAINT [FK_proDecretoResoluciones_proDecreto] FOREIGN KEY([decId])
REFERENCES [dbo].[proDecreto] ([decId])
GO

ALTER TABLE [dbo].[proDecretoResoluciones] CHECK CONSTRAINT [FK_proDecretoResoluciones_proDecreto]
GO

ALTER TABLE [dbo].[proDecretoResoluciones]  WITH CHECK ADD  CONSTRAINT [FK_proDecretoResoluciones_proResolucion] FOREIGN KEY([resId])
REFERENCES [dbo].[proResolucion] ([resId])
GO

ALTER TABLE [dbo].[proDecretoResoluciones] CHECK CONSTRAINT [FK_proDecretoResoluciones_proResolucion]
GO

Declare @mnuId Int
Insert Into sisMenu
Values('/Resoluciones/Decretos', 4, 'Decretos', Null, 'Index', 'proDecreto')
Set @mnuId = @@IDENTITY

Insert Into sisMenuAccion
Values(@mnuId, 1)

Insert Into sisMenuAccion
Values(@mnuId, 2)

Insert Into sisMenuAccion
Values(@mnuId, 3)

Insert Into sisMenuAccion
Values(@mnuId, 4)

Insert Into sisMenuAccion
Values(@mnuId, 5)

Insert Into sisMenuAccion
Values(@mnuId, 6)

Insert Into sisMenuAccion
Values(@mnuId, 7)
Go

Alter Table dbo.catFactura Alter Column facFechaRecepcion DateTime Null
Go


/****** Object:  Table [dbo].[AuxiliarDecretosExpedientes]    Script Date: 07/24/2012 07:53:10 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AuxiliarDecretosExpedientes]') AND type in (N'U'))
DROP TABLE [dbo].[AuxiliarDecretosExpedientes]
GO

/****** Object:  Table [dbo].[AuxiliarDecretosExpedientes]    Script Date: 07/24/2012 07:53:10 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[AuxiliarDecretosExpedientes](
	[zonId] [int] NOT NULL,
	[expNumero] [int] NOT NULL,
	[expAnio] [int] NOT NULL,
	[decId] [bigint] NOT NULL,
 CONSTRAINT [PK_AuxiliarDecretosExpedientes] PRIMARY KEY CLUSTERED 
(
	[zonId] ASC,
	[expNumero] ASC,
	[expAnio] ASC,
	[decId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

/****** Object:  Table [dbo].[AuxiliarDecretosResoluciones]    Script Date: 07/24/2012 08:10:57 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AuxiliarDecretosResoluciones]') AND type in (N'U'))
DROP TABLE [dbo].[AuxiliarDecretosResoluciones]
GO

/****** Object:  Table [dbo].[AuxiliarDecretosResoluciones]    Script Date: 07/24/2012 08:10:57 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[AuxiliarDecretosResoluciones](
	[decId] [bigint] NOT NULL,
	[resId] [bigint] NOT NULL,
 CONSTRAINT [PK_AuxiliarDecretosResoluciones] PRIMARY KEY CLUSTERED 
(
	[decId] ASC,
	[resId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

Insert Into proResolucion
(resNumero, resFecha, resEsImportante)
Select resNumero, resFecha, 0 From AuxiliarResolucionesViejas Where Not resNumero In(Select resNumero From proResolucion) Order By resFecha
Go

Insert Into proDecretoExpedientes
Select (Select Top 1 zonId From catZona z Where z.zonCodigo = de.zonId), expNumero, expAnio, (Select Top 1 decId From proDecreto d Where d.decNumero = de.decId) From dbo.AuxiliarDecretosExpedientes de
Go

Insert Into proDecretoResoluciones
Select (Select Top 1 resId From proResolucion z Where z.resNumero = de.resId), (Select Top 1 decId From proDecreto d Where d.decNumero = de.decId) From dbo.AuxiliarDecretosResoluciones de Where resId In(Select resNumero From proResolucion)
Go

Update proDecreto
Set decLinkArchivo = 'Decretos/' + Convert(VarChar(4), YEAR(decFecha)) + '/' + RIGHT('0000' + Convert(VarChar(5), decNumero), 4) + '.pdf'
Go

Alter Table proResolucion Drop FK_proResolucion_catTipoNormaLegal
Go

Alter Table proResolucion Drop Column tnlId
Go

Insert Into sisMenuAccion
Values(11, 7)
Go

Update proResolucion
Set resLinkArchivo = 'Resoluciones/' + resLinkArchivo
Go

Alter Table catFactura Alter Column facFechaRecepcion DateTime Null
Go

Select * From sisAccion
Select * From sisMenu

Select 'Decretos/' + Convert(VarChar(4), YEAR(decFecha)) + '/' + RIGHT('0000' + Convert(VarChar(5), decNumero), 4) + '.pdf' From proDecreto

Select * From proDecreto
Select * From proResolucion
Select * From dbo.AuxiliarDecretosResoluciones de Where resId In(Select resNumero From proResolucion Where Not resLinkArchivo Is Null) And decId = 218
Select * From proDecretoResoluciones Where decId = 219
Select Distinct zonId From AuxiliarDecretosExpedientes
Select * From catZona Order By zonCodigo
Select * From proResolucion Where resId In( 219, 302, 240)
Select * From proDecreto Where decId = 464
Select * From proDecreto Where decNumero = 218

Select * From proResolucion Where resEsImportante = 1 And resNotificacionVencimiento Is Null And resNotificacionDiaInicio Is Null
Update proResolucion
Set resEsImportante = 0
Where resEsImportante = 1 And resNotificacionVencimiento Is Null And resNotificacionDiaInicio Is Null