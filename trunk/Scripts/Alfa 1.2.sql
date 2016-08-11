CREATE TABLE dbo.catProvincia
	(
	provId smallint Identity(1,1) NOT NULL,
	provNombre varchar(50) NOT NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.catProvincia ADD CONSTRAINT
	PK_catProvincia PRIMARY KEY CLUSTERED 
	(
	provId
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.catProvincia ADD CONSTRAINT
	UQ_catProvincia_provNombre UNIQUE NONCLUSTERED 
	(
	provNombre
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_catObraSocial_catProvincia]') AND parent_object_id = OBJECT_ID(N'[dbo].[catObraSocial]'))
ALTER TABLE [dbo].[catObraSocial] DROP CONSTRAINT [FK_catObraSocial_catProvincia]
GO

/****** Object:  Table [dbo].[catObraSocial]    Script Date: 07/17/2012 10:11:38 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[catObraSocial]') AND type in (N'U'))
DROP TABLE [dbo].[catObraSocial]
GO

/****** Object:  Table [dbo].[catObraSocial]    Script Date: 07/17/2012 10:11:38 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[catObraSocial](
	[osId] [smallint] IDENTITY(1,1) NOT NULL,
	[osCodigo] [int] NOT NULL,
	[osDenominacion] [varchar](255) NOT NULL,
	[osSigla] [varchar](25) NULL,
	[osDomicilio] [varchar](255) NULL,
	[osCodigoPostal] [smallint] NOT NULL,
	[provId] [smallint] NOT NULL,
	[osTelefono] [varchar](150) NULL,
	[osMail] [varchar](150) NULL,
	[osWeb] [varchar](255) NULL,
 CONSTRAINT [PK_catObraSocial] PRIMARY KEY CLUSTERED 
(
	[osId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
 CONSTRAINT [UQ_catObraSocial_osCodigo] UNIQUE NONCLUSTERED 
(
	[osId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
 CONSTRAINT [UQ_catObraSocial_osDenominacion] UNIQUE NONCLUSTERED 
(
	[osDenominacion] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[catObraSocial]  WITH CHECK ADD  CONSTRAINT [FK_catObraSocial_catProvincia] FOREIGN KEY([provId])
REFERENCES [dbo].[catProvincia] ([provId])
GO

ALTER TABLE [dbo].[catObraSocial] CHECK CONSTRAINT [FK_catObraSocial_catProvincia]
GO

Declare @mnuId Int
Insert Into sisMenu
Values('/Catalogos/Padron/ObrasSociales', 3, 'Obras Sociales', Null, 'Index', 'catObraSocial')
Set @mnuId = @@IDENTITY

Insert Into sisMenuAccion
Values(@mnuId, 1)

Insert Into sisMenuAccion
Values(@mnuId, 2)

Insert Into sisMenuAccion
Values(@mnuId, 3)
Go

Declare @mnuId Int
Insert Into sisMenu
Values('/Catalogos/Padron/CentrosDeSalud', 4, 'Centros de Salud', Null, 'Index', 'catCentrosDeSalud')
Set @mnuId = @@IDENTITY
Go

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_catFactura_catObraSocial]') AND parent_object_id = OBJECT_ID(N'[dbo].[catFactura]'))
ALTER TABLE [dbo].[catFactura] DROP CONSTRAINT [FK_catFactura_catObraSocial]
GO

/****** Object:  Table [dbo].[catFactura]    Script Date: 07/18/2012 08:27:21 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[catFactura]') AND type in (N'U'))
DROP TABLE [dbo].[catFactura]
GO

/****** Object:  Table [dbo].[catFactura]    Script Date: 07/18/2012 08:27:21 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[catFactura](
	[facId] [int] NOT NULL,
	[facNumero] [varchar](15) NOT NULL,
	[facPeriodoAnio] [smallint] NOT NULL,
	[facPeriodoMes] [tinyint] NOT NULL,
	[facFecha] [datetime] NOT NULL,
	[facFechaRecepcion] [datetime] NOT NULL,
	[facImporte] [decimal](18, 2) NOT NULL,
	[osId] [smallint] NOT NULL,
	[sucId] [int] NOT NULL,
 CONSTRAINT [PK_catFactura] PRIMARY KEY CLUSTERED 
(
	[facId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[catFactura]  WITH CHECK ADD  CONSTRAINT [FK_catFactura_catObraSocial] FOREIGN KEY([osId])
REFERENCES [dbo].[catObraSocial] ([osId])
GO

ALTER TABLE [dbo].[catFactura] CHECK CONSTRAINT [FK_catFactura_catObraSocial]
GO

Insert Into sisMenu
Values('/Contable', 0, 'Gestión Contable', Null, 'Index', 'catFactura')
Go
Declare @mnuId Int
Insert Into sisMenu
Values('/Contable/Facturacion', 1, 'Facturas', Null, 'Index', 'catFactura')
Set @mnuId = @@IDENTITY

Insert Into sisMenuAccion
Values(@mnuId, 1)

Insert Into sisMenuAccion
Values(@mnuId, 2)

Insert Into sisMenuAccion
Values(@mnuId, 3)
Go


--Select * From sisMenu
--Select * From sisMenuAccion
Select * From catProvincia
