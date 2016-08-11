-- Recordar de generar script creando las bases de fuentes, cuentas, etc...

Declare @mnuId Int
Insert Into sisMenu
Values('/Contable/Fuente', 1, 'Fuentes', Null, 'Index', 'catFuente')
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
Values('/Contable/CuentasContables', 2, 'Cuentas', Null, 'Index', 'catCuentaContable')
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
Values('/Contable/Partidas', 3, 'Partidas', Null, 'Index', 'catPartida')
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
Values('/Contable/Bancos', 0, 'Entidades Bancarias', Null, 'Index', 'catEntidadBancaria')
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
Values('/Contable/FondoPermanente', 4, 'Fondos Permanentes', Null, 'Index', 'catFondoPermanente')
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
Values('/Contable/CreditosAnuales', 5, 'Creditos Anuales', Null, 'Index', 'catCreditoAnual')
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
Values('/Contable/Imputaciones', 7, 'Imputaciones Financieras', Null, 'Index', 'proImputacion')
Set @mnuId = @@IDENTITY

Insert Into sisMenuAccion
Values(@mnuId, 1)

Insert Into sisMenuAccion
Values(@mnuId, 2)

Insert Into sisMenuAccion
Values(@mnuId, 3)
Go

Update sisMenu
Set mnuOrden = 6
Where mnuId = 19
Go

Select * From sisMenu