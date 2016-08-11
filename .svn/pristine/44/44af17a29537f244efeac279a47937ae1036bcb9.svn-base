ALTER TABLE dbo.proCompra ADD
	comHoraApertura char(5) NULL
Go

Update proCompra
Set comHoraApertura = '00:00'
Go

/****** Object:  StoredProcedure [dbo].[getListadoDeCompras]    Script Date: 25/08/2015 06:15:20 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER Procedure [dbo].[getListadoDeCompras]
(@usrId Int, @Estados VarChar(200))
/* ----------------------------------------------------------------------
* Nombre:			getListadoDeCompras
* Descricpión:		Listado para la grilla de compras.
* Creado por:		Gustavo N. Tripolone
* Modificado por:	
* Historial de revisiones:
* Fecha			Autor	Descripción
* ----------------------------------------------------------------------
* 10/06/2015	GNT		Implementación inicial.
* 25/08/2015	GNT		Se agrega campo Hora de Apertura.
* ----------------------------------------------------------------------*/
--getListadoDeCompras 1, 'AN,SD'
As
	--Declare @usrId Int, @Estados VarChar(200)
	--Set @usrId = 1
	--Set @Estados = 'AN,SD'

	Select comComprobante,
		comDescripcion,
		comDestino,
		comFecha,
		comFechaApertura,
		comFechaIngreso,
		comId,
		IsNull(comLugarDeEntrega,'') As comLugarDeEntrega,
		tc.tipoDescripcion TipoComprobante,
		te.tipoDescripcion Estado,
		c.perId,
		pe.perApellidoyNombre Solicitante,
		c.tipoId,
		c.tipoIdEncuadreLegal,
		IsNull(el.tipoDescripcion,'') EncuadreLegal,
		c.tipoIdEstado,
		Case When te.tipoCodigo = 'CD' Then 1 Else 0 End TieneDetalle,
		te.tipoImagen EstadoImagen,
		Case When te.tipoCodigo = 'AN' Then 1 Else 0 End CompraAnulada,
		Case When Exists(Select 1
							From	sisUsuario u
									Inner Join sisUsuarioPermiso up On(up.usrId = u.usrId)
									Inner Join sisAccion a On(a.acId = up.acId)
							Where u.usrId = @usrId And acDescripcion = 'Encuadre Legal') Then 1 Else 0 End AutorizadoEncuadreLegal,
		IsNull((Select Sum(comdCantidad * comdPrecioEstimado) From proCompraDetalle d Where d.comId = c.comId And comdActivo = 1),0) ImporteEstimado,
		c.expDiasCorridos,
		c.expDiasHabiles,
		c.expUbicacion,
		c.idExpediente,
		comHoraApertura
	From proCompra c
		Inner Join catPersona pe On(pe.perId = c.perId)
		Inner Join sisTipo tc On(tc.tipoId = c.tipoId)
		Inner Join sisTipo te On(te.tipoId = c.tipoIdEstado)
		Left Outer Join sisTipo el On(el.tipoId = c.tipoIdEncuadreLegal)
	Where CharIndex(te.tipoCodigo,@Estados) > 0
Go

/****** Object:  Table [dbo].[catProveedorRubro]    Script Date: 26/08/2015 06:05:14 p.m. ******/
CREATE TABLE [dbo].[catProveedorRubro](
	[prId] [int] IDENTITY(1,1) NOT NULL,
	[prRubro] [varchar](250) NOT NULL,
	[provId] [int] NOT NULL,
 CONSTRAINT [PK_catProveedorRubro] PRIMARY KEY CLUSTERED 
(
	[prId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[catProveedorRubro]  WITH NOCHECK ADD  CONSTRAINT [FK_catProveedorRubro_catProveedor] FOREIGN KEY([provId])
REFERENCES [dbo].[catProveedor] ([provId])
GO

ALTER TABLE [dbo].[catProveedorRubro] NOCHECK CONSTRAINT [FK_catProveedorRubro_catProveedor]
GO

--- Adjunto a las compras --
CREATE TABLE [dbo].[proCompraAdjunto](
	[comaId] [int] IDENTITY(1,1) NOT NULL,
	[comaFecha] [datetime] NOT NULL,
	[usrId] [smallint] NOT NULL,
	[comaArchivo] [varchar](255) NOT NULL,
	[comaObservaciones] [varchar](255) NOT NULL,
	[comId] [int] NOT NULL,
 CONSTRAINT [PK_proCompraAdjunto] PRIMARY KEY CLUSTERED 
(
	[comaId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[proCompraAdjunto]  WITH CHECK ADD  CONSTRAINT [FK_proCompraAdjunto_proCompra] FOREIGN KEY([comId])
REFERENCES [dbo].[proCompra] ([comId])
GO

ALTER TABLE [dbo].[proCompraAdjunto] CHECK CONSTRAINT [FK_proCompraAdjunto_proCompra]
GO

ALTER TABLE [dbo].[proCompraAdjunto]  WITH CHECK ADD  CONSTRAINT [FK_proCompraAdjunto_sisUsuario] FOREIGN KEY([usrId])
REFERENCES [dbo].[sisUsuario] ([usrId])
GO

ALTER TABLE [dbo].[proCompraAdjunto] CHECK CONSTRAINT [FK_proCompraAdjunto_sisUsuario]
GO

Alter Table catProveedor Alter Column provRubro VarChar(max) Not Null
Go