use [MSP-GeDoc-Desarrollo]
go

insert into sisMenu(mnuNombre, mnuOrden, mnuTitulo, mnuImagen, mnuAccion, mnuController)
values('/GestionDeLicenciasMedicas/Estadisticas',2,'Estad�sticas',NULL,'Index','Home')

insert into sisMenu(mnuNombre, mnuOrden, mnuTitulo, mnuImagen, mnuAccion, mnuController)
values('/GestionDeLicenciasMedicas/Estadisticas/ListadoDeAtencion',0,'Listado de Atenci�n',NULL,'ListadoDeAtencion','ListadoDeAtencion')