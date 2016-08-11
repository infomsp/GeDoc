ALTER TABLE dbo.proCompraEstado ADD
	comeObservaciones varchar(250) NULL
GO

Declare @tipoeId Int
Insert Into sisTipoEntidad Values('EL', 'Encuadre Legal')
Set @tipoeId = @@IDENTITY
Insert Into sisTipo Values('CD', @tipoeId, 'Compra Directa', Null, Null)
Insert Into sisTipo Values('69', @tipoeId, 'Compra Directa Art. 69° LC', Null, Null)
Insert Into sisTipo Values('FP', @tipoeId, 'Fondo Permanente-Compra Directa', Null, Null)
Insert Into sisTipo Values('CP', @tipoeId, 'Concurso de Precios', Null, Null)
Insert Into sisTipo Values('LP', @tipoeId, 'Licitación Privada', Null, Null)
Insert Into sisTipo Values('LU', @tipoeId, 'Licitación Pública', Null, Null)
Insert Into sisTipo Values('68', @tipoeId, 'Contratación Directa Ley 7668', Null, Null)
Insert Into sisTipo Values('RE', @tipoeId, 'Régimen Especial', Null, Null)
Go

ALTER TABLE dbo.proCompra ADD
	tipoIdEncuadreLegal smallint NULL
GO
ALTER TABLE dbo.proCompra ADD CONSTRAINT
	FK_proCompra_sisTipoEncuadreLegal FOREIGN KEY
	(
	tipoIdEncuadreLegal
	) REFERENCES dbo.sisTipo
	(
	tipoId
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO

Declare @acId Int
Declare @menuId Int
Select @menuId = mnuId From sisMenu Where mnuNombre = '/GesContable/Compras'

Insert Into sisAccion Values('Encuadre Legal', '-64px -190px;')
Set @acId = @@IDENTITY
Insert Into sisMenuAccion Values(@menuId, @acId)
Go

Declare @acId Int
Declare @menuId Int
Select @menuId = mnuId From sisMenu Where mnuNombre = '/GesContable/Compras'

Insert Into sisAccion Values('Cambiar Estado', '-64px -208px;')
Set @acId = @@IDENTITY
Insert Into sisMenuAccion Values(@menuId, @acId)
Go

Declare @tipoeId Int
Select @tipoeId = tipoeId From sisTipoEntidad Where tipoeCodigo = 'CM'
Update sisTipo Set tipoImagen = 'Azul-2.png' Where tipoeId = @tipoeId And tipoCodigo = 'PP'
Update sisTipo Set tipoImagen = 'Celeste-2.png' Where tipoeId = @tipoeId And tipoCodigo = 'PO'
Update sisTipo Set tipoImagen = 'Marron.png' Where tipoeId = @tipoeId And tipoCodigo = 'PR'
Update sisTipo Set tipoImagen = 'Naranja-2.png' Where tipoeId = @tipoeId And tipoCodigo = 'AD'
Update sisTipo Set tipoImagen = 'Turquesa-2.png' Where tipoeId = @tipoeId And tipoCodigo = 'OE'
Update sisTipo Set tipoImagen = 'Verde-2.png' Where tipoeId = @tipoeId And tipoCodigo = 'PA'
Go
