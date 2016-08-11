Drop Function fnCronosAsistenciaDeUnDia
Go
Create Function fnCronosAsistenciaDeUnDia
(@DNI BigInt, @Dia Int)
Returns @Resultado Table(	Entrada	DateTime	,
							Salida	DateTime	)
As
	Begin
		Insert Into @Resultado
		Select Entrada, Salida
		From OpenQuery(AccesoCronos,'Select DNI, Min(fecha_fichada) As Entrada, Max(fecha_fichada) As Salida
						 From fichada_proceso f
								Inner Join persona p On(f.id_persona = p.id_persona)
						 Where	extract(year from fecha_fichada) = extract(year from CURRENT_DATE)
								And extract(month from fecha_fichada) = extract(month from CURRENT_DATE)
						 Group By DNI, extract(day from fecha_fichada)')
		Where DNI = @DNI And Day(Entrada) = @Dia
		
		Return
	End

Select * From fnCronosAsistenciaDeUnDia(25823291, 15)
Select * From fnCronosAsistenciaDeUnDiaEstado(25823291, 8, 12)
Select * From fnCronosAsistenciaDelDia(25823291, 8)
Select * From fnCronosAsistenciaDelMes(8) Order By Fecha Desc
Select * From catPersona Where perDNI = 27411695
Select Convert(VarChar(10),GetDate(),108)
Select * From vwPersona

Select Convert(DateTime,Convert(VarChar(10),GetDate(), 112) + ' ' + perhHoraInicio + ':' + perhMinutoInicio + ':00')
From catPersonaHorario Where perId = 2 And perhDiaSemana = 'Miércoles'

Select 'TARDANZA DE ' + Convert(VarChar(10), DateDiff(mi, GetDate(), '20140113 20:50:00')) + ' MINUTOS'

Select DateAdd(mi, 15, GetDate())
