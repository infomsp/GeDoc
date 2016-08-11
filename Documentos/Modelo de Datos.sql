IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('FK_catBienDeUso_catMarca') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE catBienDeUso DROP CONSTRAINT FK_catBienDeUso_catMarca
;

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('FK_catBienDeUso_catSector') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE catBienDeUso DROP CONSTRAINT FK_catBienDeUso_catSector
;

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('FK_catBienes_catGrupo') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE catBienDeUso DROP CONSTRAINT FK_catBienes_catGrupo
;

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('FK_catBienDeUsoCaracteristica_catBienDeUso') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE catBienDeUsoCaracteristica DROP CONSTRAINT FK_catBienDeUsoCaracteristica_catBienDeUso
;

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('FK_catBienDeUsoCaracteristica_catCaracteristica') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE catBienDeUsoCaracteristica DROP CONSTRAINT FK_catBienDeUsoCaracteristica_catCaracteristica
;

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('FK_catBienDeUsoComponente_catBienDeUso') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE catBienDeUsoComponente DROP CONSTRAINT FK_catBienDeUsoComponente_catBienDeUso
;

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('FK_catBienDeUsoComponente_catComponentes') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE catBienDeUsoComponente DROP CONSTRAINT FK_catBienDeUsoComponente_catComponentes
;

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('FK_catBienDeUsoComponente_sisUsuario') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE catBienDeUsoComponente DROP CONSTRAINT FK_catBienDeUsoComponente_sisUsuario
;

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('FK_catBienDeUsoEstado_catBienDeUso') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE catBienDeUsoEstado DROP CONSTRAINT FK_catBienDeUsoEstado_catBienDeUso
;

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('FK_catBienDeUsoEstado_sisTipo') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE catBienDeUsoEstado DROP CONSTRAINT FK_catBienDeUsoEstado_sisTipo
;

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('FK_catBienDeUsoMantenimiento_catBienDeUso') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE catBienDeUsoMantenimiento DROP CONSTRAINT FK_catBienDeUsoMantenimiento_catBienDeUso
;

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('FK_catBienDeUsoMantenimiento_sisUsuario') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE catBienDeUsoMantenimiento DROP CONSTRAINT FK_catBienDeUsoMantenimiento_sisUsuario
;

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('FK_catBienDeUsoResponzable_catBienDeUso') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE catBienDeUsoResponzable DROP CONSTRAINT FK_catBienDeUsoResponzable_catBienDeUso
;

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('FK_catBienDeUsoResponzable_catPersona') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE catBienDeUsoResponzable DROP CONSTRAINT FK_catBienDeUsoResponzable_catPersona
;

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('FK_catGrupo_catRubro') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE catGrupo DROP CONSTRAINT FK_catGrupo_catRubro
;

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('FK_catPersona_catSector') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE catPersona DROP CONSTRAINT FK_catPersona_catSector
;

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('FK_catSector_catReparticion') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE catSector DROP CONSTRAINT FK_catSector_catReparticion
;

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('FK_sisTipo_sisTipoEntidad') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE sisTipo DROP CONSTRAINT FK_sisTipo_sisTipoEntidad
;

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('FK_sisUsuarioEstado_sisTipo') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE sisUsuarioEstado DROP CONSTRAINT FK_sisUsuarioEstado_sisTipo
;

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('FK_sisUsuarioEstado_sisUsuario') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE sisUsuarioEstado DROP CONSTRAINT FK_sisUsuarioEstado_sisUsuario
;



IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('catBienDeUso') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE catBienDeUso
;

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('catBienDeUsoCaracteristica') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE catBienDeUsoCaracteristica
;

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('catBienDeUsoComponente') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE catBienDeUsoComponente
;

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('catBienDeUsoEstado') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE catBienDeUsoEstado
;

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('catBienDeUsoMantenimiento') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE catBienDeUsoMantenimiento
;

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('catBienDeUsoResponzable') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE catBienDeUsoResponzable
;

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('catCaracteristica') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE catCaracteristica
;

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('catComponentes') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE catComponentes
;

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('catGrupo') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE catGrupo
;

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('catMarca') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE catMarca
;

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('catPersona') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE catPersona
;

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('catReparticion') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE catReparticion
;

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('catRubro') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE catRubro
;

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('catSector') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE catSector
;

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('sisTipo') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE sisTipo
;

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('sisTipoEntidad') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE sisTipoEntidad
;

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('sisUsuario') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE sisUsuario
;

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('sisUsuarioEstado') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE sisUsuarioEstado
;


CREATE TABLE catBienDeUso ( 
	buId bigint identity(1,1)  NOT NULL,
	buDenominacion varchar(40) NOT NULL,
	buFechaDeCompra datetime,
	buCodigoDeBarra varchar(20) NOT NULL,
	gpoId smallint NOT NULL,
	buNumeroDeSerie varchar(30),
	marId smallint NOT NULL,
	secId smallint NOT NULL,
	buPrecioDeCosto decimal(17,2) DEFAULT 0 NOT NULL
)
;

CREATE TABLE catBienDeUsoCaracteristica ( 
	buId bigint NOT NULL,
	carId smallint NOT NULL,
	bcDetalle varchar(255)
)
;

CREATE TABLE catBienDeUsoComponente ( 
	buId bigint NOT NULL,
	comId int NOT NULL,
	bucFechaAlta datetime NOT NULL,
	bucFechaEliminacion datetime,
	bucMotivoEliminacion varchar(100),
	usrId smallint
)
;

CREATE TABLE catBienDeUsoEstado ( 
	buId bigint NOT NULL,
	tipoId smallint NOT NULL,
	bueFecha datetime NOT NULL,
	bueMotivo varchar(50) NOT NULL
)
;

CREATE TABLE catBienDeUsoMantenimiento ( 
	buId bigint NOT NULL,
	manFecha datetime NOT NULL,
	manTrabajo varchar(255) NOT NULL,
	usrId smallint NOT NULL
)
;

CREATE TABLE catBienDeUsoResponzable ( 
	buId bigint NOT NULL,
	perId int NOT NULL
)
;

CREATE TABLE catCaracteristica ( 
	carId smallint identity(1,1)  NOT NULL,
	carDescripcion varchar(150) NOT NULL
)
;

CREATE TABLE catComponentes ( 
	comId int identity(1,1)  NOT NULL,
	comDescripcion varchar(150) NOT NULL,
	comPrecioDeCosto decimal(17,2) DEFAULT 0 NOT NULL
)
;

CREATE TABLE catGrupo ( 
	gpoId smallint identity(1,1)  NOT NULL,
	gpoDescripcion varchar(80) NOT NULL,
	rubId smallint NOT NULL
)
;

CREATE TABLE catMarca ( 
	marId smallint NOT NULL,
	marDescripcion varchar(80) NOT NULL
)
;

CREATE TABLE catPersona ( 
	perId int identity(1,1)  NOT NULL,
	perApellido varchar(50) NOT NULL,
	perNombre varchar(50) NOT NULL,
	perEmail varchar(30),
	perEsContratado smallint NOT NULL,
	secId smallint NOT NULL
)
;

CREATE TABLE catReparticion ( 
	repId int identity(1,1)  NOT NULL,
	repNombre varchar(100) NOT NULL
)
;

CREATE TABLE catRubro ( 
	rubId smallint identity(1,1)  NOT NULL,
	rubDescripcion varchar(50) NOT NULL
)
;

CREATE TABLE catSector ( 
	secId smallint identity(1,1)  NOT NULL,
	secNombre varchar(80) NOT NULL,
	secUbicacionGeografica varchar(255) NOT NULL,
	secLinkMapa varchar(255),
	repId int
)
;

CREATE TABLE sisTipo ( 
	tipoId smallint identity(1,1)  NOT NULL,
	tipoeId smallint NOT NULL,
	tipoDescripcion varchar(50) NOT NULL,
	tipoImagen varchar(255),
	tipoColor varchar(10)
)
;

CREATE TABLE sisTipoEntidad ( 
	tipoeId smallint identity(1,1)  NOT NULL,
	tipoeCodigo char(2) NOT NULL,
	tipoeDescripcion varchar(50) NOT NULL
)
;

CREATE TABLE sisUsuario ( 
	usrId smallint identity(1,1)  NOT NULL,
	usrNombreDeUsuario varchar(30),
	usrApellidoyNombre varchar(80),
	usrPassword varbinary(50) NOT NULL
)
;

CREATE TABLE sisUsuarioEstado ( 
	usrId smallint NOT NULL,
	tipoId smallint NOT NULL,
	ueFecha datetime NOT NULL,
	ueMotivo varchar(50) NOT NULL
)
;


ALTER TABLE catBienDeUso
	ADD CONSTRAINT UQ_catBienDeUso_marId UNIQUE (marId)
;

ALTER TABLE catBienDeUso
	ADD CONSTRAINT UQ_catBienDeUso_buCodigoDeBarra UNIQUE (buCodigoDeBarra)
;

ALTER TABLE catBienDeUso
	ADD CONSTRAINT UQ_catBienDeUso_buDenominacion UNIQUE (buDenominacion)
;

ALTER TABLE catCaracteristica
	ADD CONSTRAINT UQ_catCaracteristica_carDescripcion UNIQUE (carDescripcion)
;

ALTER TABLE catComponentes
	ADD CONSTRAINT UQ_catComponentes_comDescripcion UNIQUE (comDescripcion)
;

ALTER TABLE catGrupo
	ADD CONSTRAINT UQ_catGrupo_gpoDescripcion UNIQUE (gpoDescripcion)
;

ALTER TABLE catMarca
	ADD CONSTRAINT UQ_catMarca_marDescripcion UNIQUE (marDescripcion)
;

ALTER TABLE catPersona
	ADD CONSTRAINT UQ_catPersona_perApellido UNIQUE (perApellido)
;

ALTER TABLE catReparticion
	ADD CONSTRAINT UQ_catReparticion_repNombre UNIQUE (repNombre)
;

ALTER TABLE catRubro
	ADD CONSTRAINT UQ_catRubro_rubDescripcion UNIQUE (rubDescripcion)
;

ALTER TABLE catSector
	ADD CONSTRAINT UQ_catSector_secNombre UNIQUE (secNombre)
;

ALTER TABLE sisTipoEntidad
	ADD CONSTRAINT UQ_sisTipoEntidad_tipoeCodigo UNIQUE (tipoeCodigo)
;

ALTER TABLE sisTipoEntidad
	ADD CONSTRAINT UQ_sisTipoEntidad_tipoeDescripcion UNIQUE (tipoeDescripcion)
;

ALTER TABLE sisUsuario
	ADD CONSTRAINT UQ_sisUsuario_usrNombreDeUsuario UNIQUE (usrNombreDeUsuario)
;

ALTER TABLE catBienDeUso ADD CONSTRAINT PK_catBienDeUso 
	PRIMARY KEY CLUSTERED (buId)
;

ALTER TABLE catBienDeUsoCaracteristica ADD CONSTRAINT PK_catBienDeUsoCaracteristica 
	PRIMARY KEY CLUSTERED (buId, carId)
;

ALTER TABLE catBienDeUsoComponente ADD CONSTRAINT PK_catBienDeUsoComponente 
	PRIMARY KEY CLUSTERED (buId, comId)
;

ALTER TABLE catBienDeUsoEstado ADD CONSTRAINT PK_catBienDeUsoEstado 
	PRIMARY KEY CLUSTERED (buId, tipoId, bueFecha)
;

ALTER TABLE catBienDeUsoMantenimiento ADD CONSTRAINT PK_catBienDeUsoMantenimiento 
	PRIMARY KEY CLUSTERED (buId, manFecha)
;

ALTER TABLE catBienDeUsoResponzable ADD CONSTRAINT PK_catBienDeUsoResponzable 
	PRIMARY KEY CLUSTERED (buId, perId)
;

ALTER TABLE catCaracteristica ADD CONSTRAINT PK_catCaracteristica 
	PRIMARY KEY CLUSTERED (carId)
;

ALTER TABLE catComponentes ADD CONSTRAINT PK_catComponentes 
	PRIMARY KEY CLUSTERED (comId)
;

ALTER TABLE catGrupo ADD CONSTRAINT PK_catGrupo 
	PRIMARY KEY CLUSTERED (gpoId)
;

ALTER TABLE catMarca ADD CONSTRAINT PK_catMarca 
	PRIMARY KEY CLUSTERED (marId)
;

ALTER TABLE catPersona ADD CONSTRAINT PK_catPersona 
	PRIMARY KEY CLUSTERED (perId)
;

ALTER TABLE catReparticion ADD CONSTRAINT PK_catReparticion 
	PRIMARY KEY CLUSTERED (repId)
;

ALTER TABLE catRubro ADD CONSTRAINT PK_catRubro 
	PRIMARY KEY CLUSTERED (rubId)
;

ALTER TABLE catSector ADD CONSTRAINT PK_catSector 
	PRIMARY KEY CLUSTERED (secId)
;

ALTER TABLE sisTipo ADD CONSTRAINT PK_sisTipo 
	PRIMARY KEY CLUSTERED (tipoId)
;

ALTER TABLE sisTipoEntidad ADD CONSTRAINT PK_sisTipoEntidad 
	PRIMARY KEY CLUSTERED (tipoeId)
;

ALTER TABLE sisUsuario ADD CONSTRAINT PK_sisUsuario 
	PRIMARY KEY CLUSTERED (usrId)
;

ALTER TABLE sisUsuarioEstado ADD CONSTRAINT PK_sisUsuarioEstado 
	PRIMARY KEY CLUSTERED (usrId, tipoId, ueFecha)
;



ALTER TABLE catBienDeUso ADD CONSTRAINT FK_catBienDeUso_catMarca 
	FOREIGN KEY (marId) REFERENCES catMarca (marId)
;

ALTER TABLE catBienDeUso ADD CONSTRAINT FK_catBienDeUso_catSector 
	FOREIGN KEY (secId) REFERENCES catSector (secId)
;

ALTER TABLE catBienDeUso ADD CONSTRAINT FK_catBienes_catGrupo 
	FOREIGN KEY (gpoId) REFERENCES catGrupo (gpoId)
;

ALTER TABLE catBienDeUsoCaracteristica ADD CONSTRAINT FK_catBienDeUsoCaracteristica_catBienDeUso 
	FOREIGN KEY (buId) REFERENCES catBienDeUso (buId)
;

ALTER TABLE catBienDeUsoCaracteristica ADD CONSTRAINT FK_catBienDeUsoCaracteristica_catCaracteristica 
	FOREIGN KEY (carId) REFERENCES catCaracteristica (carId)
;

ALTER TABLE catBienDeUsoComponente ADD CONSTRAINT FK_catBienDeUsoComponente_catBienDeUso 
	FOREIGN KEY (buId) REFERENCES catBienDeUso (buId)
;

ALTER TABLE catBienDeUsoComponente ADD CONSTRAINT FK_catBienDeUsoComponente_catComponentes 
	FOREIGN KEY (comId) REFERENCES catComponentes (comId)
;

ALTER TABLE catBienDeUsoComponente ADD CONSTRAINT FK_catBienDeUsoComponente_sisUsuario 
	FOREIGN KEY (usrId) REFERENCES sisUsuario (usrId)
;

ALTER TABLE catBienDeUsoEstado ADD CONSTRAINT FK_catBienDeUsoEstado_catBienDeUso 
	FOREIGN KEY (buId) REFERENCES catBienDeUso (buId)
;

ALTER TABLE catBienDeUsoEstado ADD CONSTRAINT FK_catBienDeUsoEstado_sisTipo 
	FOREIGN KEY (tipoId) REFERENCES sisTipo (tipoId)
;

ALTER TABLE catBienDeUsoMantenimiento ADD CONSTRAINT FK_catBienDeUsoMantenimiento_catBienDeUso 
	FOREIGN KEY (buId) REFERENCES catBienDeUso (buId)
;

ALTER TABLE catBienDeUsoMantenimiento ADD CONSTRAINT FK_catBienDeUsoMantenimiento_sisUsuario 
	FOREIGN KEY (usrId) REFERENCES sisUsuario (usrId)
;

ALTER TABLE catBienDeUsoResponzable ADD CONSTRAINT FK_catBienDeUsoResponzable_catBienDeUso 
	FOREIGN KEY (buId) REFERENCES catBienDeUso (buId)
;

ALTER TABLE catBienDeUsoResponzable ADD CONSTRAINT FK_catBienDeUsoResponzable_catPersona 
	FOREIGN KEY (perId) REFERENCES catPersona (perId)
;

ALTER TABLE catGrupo ADD CONSTRAINT FK_catGrupo_catRubro 
	FOREIGN KEY (rubId) REFERENCES catRubro (rubId)
;

ALTER TABLE catPersona ADD CONSTRAINT FK_catPersona_catSector 
	FOREIGN KEY (secId) REFERENCES catSector (secId)
;

ALTER TABLE catSector ADD CONSTRAINT FK_catSector_catReparticion 
	FOREIGN KEY (repId) REFERENCES catReparticion (repId)
;

ALTER TABLE sisTipo ADD CONSTRAINT FK_sisTipo_sisTipoEntidad 
	FOREIGN KEY (tipoeId) REFERENCES sisTipoEntidad (tipoeId)
;

ALTER TABLE sisUsuarioEstado ADD CONSTRAINT FK_sisUsuarioEstado_sisTipo 
	FOREIGN KEY (tipoId) REFERENCES sisTipo (tipoId)
;

ALTER TABLE sisUsuarioEstado ADD CONSTRAINT FK_sisUsuarioEstado_sisUsuario 
	FOREIGN KEY (usrId) REFERENCES sisUsuario (usrId)
;
