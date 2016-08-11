Select REPLACE(resLinkArchivo, 'Resoluciones/', ''), * From proResolucion Where resLinkArchivo Like 'Resoluciones%'
Select REPLACE(decLinkArchivo, 'Decretos/', ''), * From proDecreto Where decLinkArchivo Like 'Decretos%'
Select REPLACE(resConsiderando, '<br />', ' '), * From proResolucion Where resConsiderando Like '%<br />%'
Select REPLACE(decConsiderando, '<br />', ' '), * From proDecreto Where decConsiderando Like '%<br />%'

Update proResolucion
Set resConsiderando = ISNULL(resConsiderando, '') + ISNULL(resResuelve, '')

Update proResolucion
Set resLinkArchivo = REPLACE(resLinkArchivo, 'Resoluciones/', '')
Where resLinkArchivo Like 'Resoluciones%'

Update proResolucion
Set resConsiderando = REPLACE(resConsiderando, '<br />', ' ')
Where resConsiderando Like '%<br />%'

Update proDecreto
Set decConsiderando = ISNULL(decConsiderando, '') + ' ' + ISNULL(decResuelve, '')

Update proDecreto
Set decLinkArchivo = REPLACE(decLinkArchivo, 'Decretos/', '')
Where decLinkArchivo Like 'Decretos%'

Update proDecreto
Set decConsiderando = REPLACE(decConsiderando, '</p>', ' ')
Where decConsiderando Like '%</p>%'

Insert Into sisMenuAccion
(mnuId, acId)
Values(7, 4)

Insert Into sisMenuAccion
(mnuId, acId)
Values(7, 6)

Select * From proResolucionExpedientes
Select * From proResolucion Where resId = 1146
Select * From sisMenuAccion
Select * From sisAccion
Select * From sisTipo
Select * From sisTipoEntidad
Select * From catMotivo
Select * From proDecreto Order By decFecha Desc
