-- Personas --
Select *
From OpenQuery(AccesoCronos, 'Select * From persona Where id_persona = 1899')

Select *
From OpenQuery(AccesoCronos, 'Select * From persona_horario Where id_persona = 1899')

Select *
From OpenQuery(AccesoCronos, 'Select * From horario_control_personal Where id_horario_Control_personal = 1')

Select *
From OpenQuery(AccesoCronos, 'Select * From jornada Where id_horario_Control_personal = 1')

Select *
From OpenQuery(AccesoCronos, 'Select * From horario')

Select *
From OpenQuery(AccesoCronos, 'Select * From ')

Select *
From OpenQuery(AccesoCronos, 'Select j.*
								From persona p
									Inner Join persona_horario ph On(ph.id_persona = p.id_persona)
									Inner Join jornada j On(j.id_horario_Control_personal = ph.id_horario_Control_personal)
								Where p.id_persona = 705')

Select *
From OpenQuery(AccesoCronos,
				'Select ph.*
				 From persona p
					Inner Join persona_horario ph On(ph.id_persona = p.id_persona)
				 Where apellido Like ''%CASARINO%''')


Insert Into catFeriado
(ferFecha, ferDescripcion)
Select fecha, descripcion
From OpenQuery(AccesoCronos, 'Select * From feriado') a
Where Not Exists(Select 1 From catFeriado Where Convert(Date, ferFecha) = Convert(Date, fecha))

Select *
From OpenQuery(AccesoCronos, 'Select * From proceso_persona_jornada')

Select *
From OpenQuery(AccesoCronos, 'Select * From jornada Where id_jornada = 1')

Select *
From OpenQuery(AccesoCronos, 'Select * From fecha_jornada')
Order By fecha_jornada Desc

-- Fichadas
Select *
From OpenQuery(AccesoCronos, 'Select * From proceso Where Not fecha_fin Is Null Order By fecha_fin Desc')

-- Procesos Fichadas
Select *
From OpenQuery(AccesoCronos,
				'Select f.id_persona, fecha_fichada, apellido, nombre, DNI
				 From fichada_proceso f
						Inner Join persona p On(f.id_persona = p.id_persona)')
Where	DNI = 23189798
		And Year(fecha_fichada) = 2014
		And Month(fecha_fichada) = 1
		And Day(fecha_fichada) = 7

Select *
From OpenQuery(AccesoCronos,'Select f.id_persona, fecha_fichada, apellido, nombre, DNI
				 From fichada_proceso f
						Inner Join persona p On(f.id_persona = p.id_persona)
				 Where	extract(year from fecha_fichada) = extract(year from CURRENT_DATE)
						And extract(month from fecha_fichada) = extract(month from CURRENT_DATE)
						And extract(day from fecha_fichada) = extract(day from CURRENT_DATE)')
--Where Apellido Like '%PIZARR%'
Where DNI = 28622913

Select *
From OpenQuery(AccesoCronos,'Select f.id_persona, fecha_fichada, apellido, nombre, DNI
				 From fichada_proceso f
						Inner Join persona p On(f.id_persona = p.id_persona)
				 Where	extract(year from fecha_fichada) = extract(year from CURRENT_DATE)
						And extract(month from fecha_fichada) = extract(month from CURRENT_DATE)')
Where DNI = 28622913
Order By fecha_fichada Desc

Select *
From OpenQuery(AccesoCronos,'Select Count(*) As Cantidad
				 From fichada_proceso f
						Inner Join persona p On(f.id_persona = p.id_persona)
				 Where	extract(year from fecha_fichada) = extract(year from CURRENT_DATE)
						And extract(month from fecha_fichada) = extract(month from CURRENT_DATE)
						And extract(day from fecha_fichada) = extract(day from CURRENT_DATE)')

Select *
From OpenQuery(AccesoCronos,'Select Count(*) As Cantidad
				 From fichada_proceso f
						Inner Join persona p On(f.id_persona = p.id_persona)
				 Where	extract(year from fecha_fichada) = 2014
						And extract(month from fecha_fichada) = 1
						And extract(day from fecha_fichada) = 14')

-- Justificaciones --
Select *
From OpenQuery(AccesoCronos, 'Select * From justificacion')

-- Novedades --
Select *
From OpenQuery(AccesoCronos, 'Select n.*, id_persona, j.descripcion As jus_des
								From novedad n
									Inner Join rel_novedad_persona rn On(rn.id_novedad = n.id_novedad)
									Inner Join justificacion j On(j.id_justificacion = n.id_justificacion)
								Where n.fecha_baja Is Null')
Where id_persona = 1899
		And Convert(Date,DateAdd(dd,-20,GetDate())) BetWeen fecha_desde And fecha_hasta

Select *
From OpenQuery(AccesoCronos, 'Select * From tipo_novedad')

Select *
From OpenQuery(AccesoCronos, 'Select * From rel_novedad_persona')


