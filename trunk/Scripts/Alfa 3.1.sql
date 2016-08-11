-- Recordar de generar script creando las bases de fuentes, cuentas, etc...

Declare @mnuId Int
Insert Into sisMenu
Values('/Contable/CuentasEscriturales', 10, 'Cuentas Escriturales', Null, 'Index', 'catCuentaEscritural')
Set @mnuId = @@IDENTITY

Insert Into sisMenuAccion
Values(@mnuId, 1)

Insert Into sisMenuAccion
Values(@mnuId, 2)

Insert Into sisMenuAccion
Values(@mnuId, 3)
Go

