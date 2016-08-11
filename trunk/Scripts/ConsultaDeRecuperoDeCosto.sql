Select * From catFactura
Select * From catFacturaPago

Select tipoDescripcion, f.facPeriodoAnio, SUM(pagImporte)
From catFacturaPago p
	Inner Join catFactura f On(f.facId = p.facId)
	Inner Join sisTipo t On(t.tipoId = p.tipoId)
Where Year(pagFecha) = 2013
Group By tipoDescripcion, f.facPeriodoAnio
Order By tipoDescripcion, f.facPeriodoAnio

Select * From sisTipo Where tipoId = 11