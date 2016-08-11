IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('FK_proResolucionNotificacionEmpleado_catPersona') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE proResolucionNotificacionEmpleado DROP CONSTRAINT FK_proResolucionNotificacionEmpleado_catPersona
;

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('FK_proResolucionNotificacionEmpleado_proResolucion') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE proResolucionNotificacionEmpleado DROP CONSTRAINT FK_proResolucionNotificacionEmpleado_proResolucion
;

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('FK_proResolucionExpedientes_catZona') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE proResolucionExpedientes DROP CONSTRAINT FK_proResolucionExpedientes_catZona
;

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('FK_proResolucionExpedientes_proResolucion') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE proResolucionExpedientes DROP CONSTRAINT FK_proResolucionExpedientes_proResolucion
;



IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('proResolucionNotificacionEmpleado') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE proResolucionNotificacionEmpleado
;

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('proResolucionExpedientes') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE proResolucionExpedientes
;

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('proResolucion') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE proResolucion
;

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('catZona') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE catZona
;


CREATE TABLE proResolucionNotificacionEmpleado ( 
	resId bigint NOT NULL,
	perId int NOT NULL
)
;

CREATE TABLE proResolucionExpedientes ( 
	reseId bigint identity(1,1)  NOT NULL,
	zonId int NOT NULL,
	reseExpedienteNumero int NOT NULL,
	reseExpedienteAnio smallint NOT NULL,
	resId bigint NOT NULL
)
;

CREATE TABLE proResolucion ( 
	resId bigint identity(1,1)  NOT NULL,
	resNumero smallint NOT NULL,
	resFecha datetime NOT NULL,
	resConsiderando varchar(max),
	resResuelve varchar(max),
	resLinkArchivo varchar(max),
	resEsImportante bit NOT NULL,
	resNotificacionVencimiento datetime,
	resNotificacionDiaInicio smallint
)
;

CREATE TABLE catZona ( 
	zonId int identity(1,1)  NOT NULL,
	zonCodigo int NOT NULL,
	zonNombre varchar(80) NOT NULL
)
;


ALTER TABLE proResolucionExpedientes
	ADD CONSTRAINT UQ_proResolucionExpedientes_reseId UNIQUE (reseId)
;

ALTER TABLE proResolucion
	ADD CONSTRAINT UQ_proResolucion_resId UNIQUE (resId)
;

ALTER TABLE proResolucion
	ADD CONSTRAINT UQ_proResolucion_resNumero_resFecha UNIQUE (resNumero, resFecha)
;

ALTER TABLE catZona
	ADD CONSTRAINT UQ_catZona_zonCodigo UNIQUE (zonCodigo)
;

ALTER TABLE catZona
	ADD CONSTRAINT UQ_catZona_zonId UNIQUE (zonId)
;

ALTER TABLE catZona
	ADD CONSTRAINT UQ_catZona_zonNombre UNIQUE (zonNombre)
;

ALTER TABLE proResolucionNotificacionEmpleado ADD CONSTRAINT PK_proResolucionNotificacionEmpleado 
	PRIMARY KEY CLUSTERED (resId, perId)
;

ALTER TABLE proResolucionExpedientes ADD CONSTRAINT PK_proResolucionExpedientes 
	PRIMARY KEY CLUSTERED (reseId)
;

ALTER TABLE proResolucion ADD CONSTRAINT PK_proResolucion 
	PRIMARY KEY CLUSTERED (resId)
;

ALTER TABLE catZona ADD CONSTRAINT PK_catZona 
	PRIMARY KEY CLUSTERED (zonId)
;



ALTER TABLE proResolucionNotificacionEmpleado ADD CONSTRAINT FK_proResolucionNotificacionEmpleado_catPersona 
	FOREIGN KEY (perId) REFERENCES catPersona (perId)
;

ALTER TABLE proResolucionNotificacionEmpleado ADD CONSTRAINT FK_proResolucionNotificacionEmpleado_proResolucion 
	FOREIGN KEY (resId) REFERENCES proResolucion (resId)
;

ALTER TABLE proResolucionExpedientes ADD CONSTRAINT FK_proResolucionExpedientes_catZona 
	FOREIGN KEY (zonId) REFERENCES catZona (zonId)
;

ALTER TABLE proResolucionExpedientes ADD CONSTRAINT FK_proResolucionExpedientes_proResolucion 
	FOREIGN KEY (resId) REFERENCES proResolucion (resId)
;
