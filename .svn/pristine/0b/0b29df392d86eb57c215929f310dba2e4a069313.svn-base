CREATE TABLE dbo.catRequerimiento
	(
	reqId int NOT NULL IDENTITY (1, 1),
	tipoIdTipo smallint NOT NULL,
	reqAsunto varchar(80) NOT NULL,
	reqDescripcion varchar(255) NOT NULL,
	perIdSolicitante int NOT NULL,
	reqTiempoEstimado int NULL,
	reqFechaEstimado DateTime NULL,
	usrId int Not NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.catRequerimiento ADD CONSTRAINT
	PK_catRequerimiento PRIMARY KEY CLUSTERED 
	(
	reqId
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.catRequerimiento ADD CONSTRAINT
	FK_catRequerimiento_sisTipo FOREIGN KEY
	(
	tipoIdTipo
	) REFERENCES dbo.sisTipo
	(
	tipoId
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.catRequerimiento ADD CONSTRAINT
	FK_catRequerimiento_sisUsuario FOREIGN KEY
	(
	usrId
	) REFERENCES dbo.sisUsuario
	(
	usrId
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.catRequerimiento ADD CONSTRAINT
	FK_catRequerimiento_catPersona FOREIGN KEY
	(
	perIdSolicitante
	) REFERENCES dbo.catPersona
	(
	perId
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO

--- Estados ---
CREATE TABLE dbo.catRequerimientoEstado
	(
	reqeId int NOT NULL IDENTITY (1, 1),
	tipoIdEstado smallint NOT NULL,
	reqeFecha datetime NOT NULL,
	usrId smallint NOT NULL,
	reqeObservaciones varchar(255) NOT NULL,
	reqId int NOT NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.catRequerimientoEstado ADD CONSTRAINT
	PK_catRequerimientoEstado PRIMARY KEY CLUSTERED 
	(
	reqeId
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.catRequerimientoEstado ADD CONSTRAINT
	FK_catRequerimientoEstado_sisTipo FOREIGN KEY
	(
	tipoIdEstado
	) REFERENCES dbo.sisTipo
	(
	tipoId
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.catRequerimientoEstado ADD CONSTRAINT
	FK_catRequerimientoEstado_sisUsuario FOREIGN KEY
	(
	usrId
	) REFERENCES dbo.sisUsuario
	(
	usrId
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.catRequerimientoEstado ADD CONSTRAINT
	FK_catRequerimientoEstado_catRequerimiento FOREIGN KEY
	(
	reqId
	) REFERENCES dbo.catRequerimiento
	(
	reqId
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO

--- Archivos Adjuntos ---
CREATE TABLE dbo.catRequerimientoAdjunto
	(
	reqaId int NOT NULL IDENTITY (1, 1),
	reqaFecha datetime NOT NULL,
	usrId smallint NOT NULL,
	reqaArchivo varchar(255) NOT NULL,
	reqaObservaciones varchar(255) NOT NULL,
	reqId int NOT NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.catRequerimientoAdjunto ADD CONSTRAINT
	PK_catRequerimientoAdjunto PRIMARY KEY CLUSTERED 
	(
	reqaId
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.catRequerimientoAdjunto ADD CONSTRAINT
	FK_catRequerimientoAdjunto_sisUsuario FOREIGN KEY
	(
	usrId
	) REFERENCES dbo.sisUsuario
	(
	usrId
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.catRequerimientoAdjunto ADD CONSTRAINT
	FK_catRequerimientoAdjunto_catRequerimiento FOREIGN KEY
	(
	reqId
	) REFERENCES dbo.catRequerimiento
	(
	reqId
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO

CREATE TABLE dbo.catRequerimientoComentarios
	(
	reqcId int NOT NULL IDENTITY (1, 1),
	reqcFecha datetime NOT NULL,
	reqcComentario varchar(250) NOT NULL,
	usrId smallint NOT NULL,
	reqId int NOT NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.catRequerimientoComentarios ADD CONSTRAINT
	PK_catRequerimientoComentarios PRIMARY KEY CLUSTERED 
	(
	reqcId
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.catRequerimientoComentarios ADD CONSTRAINT
	FK_catRequerimientoComentarios_sisUsuario FOREIGN KEY
	(
	usrId
	) REFERENCES dbo.sisUsuario
	(
	usrId
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.catRequerimientoComentarios ADD CONSTRAINT
	FK_catRequerimientoComentarios_catRequerimiento FOREIGN KEY
	(
	reqId
	) REFERENCES dbo.catRequerimiento
	(
	reqId
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO

CREATE TABLE dbo.catRequerimientoLogTrabajo
	(
	reqlId int NOT NULL IDENTITY (1, 1),
	reqlFecha datetime NOT NULL,
	reqlTiempo int NOT NULL,
	reqlObservaciones VarChar(250) Not Null,
	usrId smallint NOT NULL,
	reqId int NOT NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.catRequerimientoLogTrabajo ADD CONSTRAINT
	PK_catRequerimientoLogTrabajo PRIMARY KEY CLUSTERED 
	(
	reqlId
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.catRequerimientoLogTrabajo ADD CONSTRAINT
	FK_catRequerimientoLogTrabajo_sisUsuario FOREIGN KEY
	(
	usrId
	) REFERENCES dbo.sisUsuario
	(
	usrId
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.catRequerimientoLogTrabajo ADD CONSTRAINT
	FK_catRequerimientoLogTrabajo_catRequerimiento FOREIGN KEY
	(
	reqId
	) REFERENCES dbo.catRequerimiento
	(
	reqId
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO

Declare @tipoeId Int
Insert Into sisTipoEntidad Values('TR', 'Tipo de Requerimiento')
Set @tipoeId = @@IDENTITY
Insert Into sisTipo Values('TA', @tipoeId, 'Tarea', Null, Null)
Insert Into sisTipo Values('ER', @tipoeId, 'Error', Null, Null)
Insert Into sisTipo Values('NF', @tipoeId, 'Nueva Funcionalidad', Null, Null)
Insert Into sisTipo Values('MO', @tipoeId, 'Mejora', Null, Null)
Go

Declare @tipoeId Int
Insert Into sisTipoEntidad Values('ER', 'Estado del Requerimiento')
Set @tipoeId = @@IDENTITY
Insert Into sisTipo Values('SO', @tipoeId, 'Solicitado', 'Solicitado.png', Null)
Insert Into sisTipo Values('PE', @tipoeId, 'Pendiente', 'Pendiente.png', Null)
Insert Into sisTipo Values('AN', @tipoeId, 'Anulado', 'Anulado.png', Null)
Insert Into sisTipo Values('EP', @tipoeId, 'En Proceso', 'En Proceso.png', Null)
Insert Into sisTipo Values('TE', @tipoeId, 'Terminado', 'Terminado.png', Null)
Insert Into sisTipo Values('IM', @tipoeId, 'Implementado', 'Implementado.png', Null)
Go

Declare @mnuId Int
Insert Into sisMenu Values('/Catalogos/Sistema', 3,	'Sistema', NULL,	'Index', 'Home')
Insert Into sisMenu Values('/Catalogos/Sistema/Requerimientos', 0,	'Requerimientos', NULL,	'Index', 'catRequerimiento')
Set @mnuId = @@IDENTITY
Insert Into sisMenuAccion Values(@mnuId, 1)
Insert Into sisMenuAccion Values(@mnuId, 2)
Insert Into sisMenuAccion Values(@mnuId, 3)
Insert Into sisMenuAccion Values(@mnuId, 17)
Insert Into sisMenuAccion Values(@mnuId, 18)
Go

CREATE TABLE dbo.catAsunto
	(
	asuId int NOT NULL IDENTITY (1, 1),
	asuDescripcion varchar(80) NOT NULL,
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.catAsunto ADD CONSTRAINT
	PK_catAsunto PRIMARY KEY CLUSTERED 
	(
	asuId
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO

Declare @acId SmallInt
Insert Into sisAccion Values('Mis Requerimientos', '-32px -336px;')
Set @acId = @@IDENTITY
Insert Into sisMenuAccion Values(57, @acId)
Go

Select * From sisAccion
Select * From sisMenuAccion
Select * From sisMenu
------------------- FIN DE ACTUALIZACION ----------------------

Select * From sisMenu Where mnuNombre Like '/Catalogos/Padron%'
Select * From sisMenuAccion Where mnuId = 4

Select * From sisMenuAccion Where mnuId = 4

Execute ProcGeneraClase 'catRequerimientoComentarios'

