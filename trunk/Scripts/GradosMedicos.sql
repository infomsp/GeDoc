/****** Object:  Table [dbo].[catAgrupamientoGradosDeEscalafon] Script Date: 07/10/2014 09:44:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[catAgrupamientoGradosDeEscalafon](
	[ageId] [smallint] IDENTITY(1,1) NOT NULL,
	[ageDescripcion] [varchar](80) NOT NULL,
 CONSTRAINT [PK_catAgrupamientoGradosDeEscalafon] PRIMARY KEY CLUSTERED 
(
	[ageId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
 CONSTRAINT [UQ_catAgrupamientoGradosDeEscalafon_ageDescripcion] UNIQUE NONCLUSTERED 
(
	[ageId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

/****** Object:  Table [dbo].[catGrados] Script Date: 07/10/2014 09:42:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[catGrados](
	[graId] [smallint] IDENTITY(1,1) NOT NULL,
	[graDescripcion] [varchar](150) NOT NULL,
	[ageId] [smallint] NOT NULL,
 CONSTRAINT [PK_catGrados] PRIMARY KEY CLUSTERED 
(
	[graId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
 CONSTRAINT [UQ_catGrados_graDescripcion] UNIQUE NONCLUSTERED 
(
	[graDescripcion] ASC,
	[ageId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[catGrados]  WITH CHECK ADD  CONSTRAINT [FK_catGrados_catAgrupamientoGradosDeEscalafon] FOREIGN KEY([ageId])
REFERENCES [dbo].[catAgrupamientoGradosDeEscalafon] ([ageId])
GO

ALTER TABLE [dbo].[catGrados] CHECK CONSTRAINT [FK_catGrados_catAgrupamientoGradosDeEscalafon]
GO

/****** Object:  Table [dbo].[catGradosCategoria] Script Date: 07/10/2014 09:42:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[catGradosCategoria](
	[gracId] [smallint] IDENTITY(1,1) NOT NULL,
	[gracDescripcion] [varchar](150) NOT NULL,
	[graId] [smallint] NOT NULL,
 CONSTRAINT [PK_catGradosCategoria] PRIMARY KEY CLUSTERED 
(
	[gracId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
 CONSTRAINT [UQ_catGradosCategoria_gracDescripcion] UNIQUE NONCLUSTERED 
(
	[gracDescripcion] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[catGradosCategoria]  WITH CHECK ADD  CONSTRAINT [FK_catGradosCategoria_catGrados] FOREIGN KEY([graId])
REFERENCES [dbo].[catGrados] ([graId])
GO

ALTER TABLE [dbo].[catGradosCategoria] CHECK CONSTRAINT [FK_catGradosCategoria_catGrados]
GO


/****** Object:  Table [dbo].[catGradosPresupuestado] Script Date: 07/10/2014 12:13:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[catGradosPresupuestado](
	[psId] [smallint] IDENTITY(1,1) NOT NULL,
	[psFecha] [datetime] NOT NULL,
	[psCantidad] [smallint] NOT NULL,
	[psObservaciones] [varchar](max) NOT NULL,
	[graId] [smallint] NOT NULL,
 CONSTRAINT [PK_catGradosPresupuestado] PRIMARY KEY CLUSTERED 
(
	[psId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[catGradosPresupuestado]  WITH CHECK ADD  CONSTRAINT [FK_catGradosPresupuestado_catGrados] FOREIGN KEY([categId])
REFERENCES [dbo].[catCargoCategoria] ([graId])
GO

ALTER TABLE [dbo].[catGradosPresupuestado] CHECK CONSTRAINT [FK_catGradosPresupuestado_catGrados]
GO

ALTER TABLE [dbo].[catGradosPresupuestado] WITH CHECK ADD CONSTRAINT [FK_catGradosPresupuestado_proResolucion] FOREIGN KEY([resId])
REFERENCES [dbo].[proResolucion] ([resId])
GO

ALTER TABLE [dbo].[catGradosPresupuestado] CHECK CONSTRAINT [FK_catGradosPresupuestado_proResolucion]
GO
