Drop Function fnCronosAsistenciaDeUnDiaEstado
Go
Create Function fnCronosAsistenciaDeUnDiaEstado
(@DNI BigInt, @perId Int, @Dia Int)
Returns @Resultado Table(	Codigo	SmallInt		,
							Estado	VarChar(250)	)
As
	Begin
		Declare @Fichada Table(	id_persona		BigInt		,
								fecha_fichada	DateTime	,
								Apellido		VarChar(50)	,
								Nombre			VarChar(50)	,
								DNI				BigInt		)
		Declare @FechaFichada DateTime
		Declare @FechaActual DateTime
		Declare @FechaActualHoraInicio DateTime
		Declare @FechaActualHoraFin DateTime
		Declare @FechaFichadaMinima DateTime
		Declare @FechaFichadaMaxima DateTime
		Declare @id_persona BigInt
		Declare @Justificacion VarChar(100)
		Declare @Descripcion VarChar(100)
		Declare @DiaSemana VarChar(20)
		Declare @Fecha DateTime
		
		Set @Fecha = Convert(DateTime, Convert(VarChar(4),Year(GetDate())) + Right('0' + Convert(VarChar(2),Month(GetDate())), 2) + Right('0' + Convert(VarChar(2),@Dia), 2) + ' ' + Convert(VarChar(10),GetDate(),108))
		Select @DiaSemana = Case DatePart(dw,@Fecha)
								When 1 Then 'Domingo'
								When 2 Then 'Lunes'
								When 3 Then 'Martes'
								When 4 Then 'Miércoles'
								When 5 Then 'Jueves'
								When 6 Then 'Viernes'
								When 7 Then 'Sábado'
							End
		If Not Exists(Select Top 1 1 From catPersonaHorario Where perId = @perId)
			Begin
				Insert Into @Resultado
				Values(-3, 'FALTAN LOS HORARIOS DE TRABAJO')
				
				Return
			End

		If Not Exists(Select Top 1 1 From catPersonaHorario Where perId = @perId And perhDiaSemana = @DiaSemana)
			Begin
				Insert Into @Resultado
				Values(-1, 'DIA NO LABORAL')
				
				Return
			End

		If Exists(Select Top 1 1 From catFeriado Where Convert(Date, ferFecha) = Convert(Date, @Fecha))
			Begin
				Insert Into @Resultado
				Select -1, ferDescripcion
				From catFeriado
				Where Convert(Date, ferFecha) = Convert(Date, @Fecha)
				
				Return
			End

		If Not Exists(Select Top 1 1 From OpenQuery(AccesoCronos,'Select f.id_persona, fecha_fichada, apellido, nombre, DNI
							 From fichada_proceso f
									Inner Join persona p On(f.id_persona = p.id_persona)
							 Where	extract(year from fecha_fichada) = extract(year from CURRENT_DATE)
									And extract(month from fecha_fichada) = extract(month from CURRENT_DATE)')
						Where Day(fecha_fichada) = @Dia)
			Begin
				Insert Into @Resultado
				Values(-4, 'PROCESO DE CRONOS NO REALIZADO')
				
				Return
			End

		Select @FechaActualHoraInicio = Convert(DateTime,Convert(VarChar(50),@Fecha, 112) + ' ' + perhHoraInicio + ':' + perhMinutoInicio + ':00')
		From catPersonaHorario Where perId = @perId And perhDiaSemana = @DiaSemana

		If @Fecha <= @FechaActualHoraInicio And Convert(Date,@Fecha) = Convert(Date,GetDate())
			Begin
				Insert Into @Resultado
				Values(-2, '')
				
				Return
			End

		--Select @FechaActualHoraInicio = DateAdd(hh,-1,@FechaActualHoraInicio)
		--Select @FechaActualHoraInicioTope = DateAdd(hh,3,@FechaActualHoraInicio)
		
		Select @FechaActualHoraFin = Convert(DateTime,Convert(VarChar(10),@Fecha, 112) + ' ' + perhHoraFin + ':' + perhMinutoFin + ':00')
		From catPersonaHorario Where perId = @perId And perhDiaSemana = @DiaSemana

		--Select @FechaActualHoraFinTope = DateAdd(hh,3,@FechaActualHoraFin)
		
		Insert Into @Fichada
		Select *
		From OpenQuery(AccesoCronos,'Select f.id_persona, fecha_fichada, apellido, nombre, DNI
						 From fichada_proceso f
								Inner Join persona p On(f.id_persona = p.id_persona)
						 Where	extract(year from fecha_fichada) = extract(year from CURRENT_DATE)
								And extract(month from fecha_fichada) = extract(month from CURRENT_DATE)')
		Where DNI = @DNI And Day(fecha_fichada) = @Dia
		
		If @@RowCount = 0
			Begin
				-- Ausente
				Select @id_persona = 0
				Select @id_persona = id_persona
				From OpenQuery(AccesoCronos,'Select id_persona, DNI From persona')
				Where DNI = @DNI
				
				If @id_persona = 0 Or @id_persona Is Null
					Begin
						Insert Into @Resultado
						Values(0, 'SIN VINCULO CON CRONOS')
						
						Return
					End
					
				Select @Justificacion = ''
				Select @Justificacion = jus_des
				From OpenQuery(AccesoCronos, 'Select n.*, id_persona, j.descripcion As jus_des
												From novedad n
													Inner Join rel_novedad_persona rn On(rn.id_novedad = n.id_novedad)
													Inner Join justificacion j On(j.id_justificacion = n.id_justificacion)
												Where n.fecha_baja Is Null')
				Where id_persona = @id_persona
						And Convert(Date,@Fecha) BetWeen Convert(Date,fecha_desde) And Convert(Date,fecha_hasta)

				If @Justificacion = '' Or @Justificacion Is Null
					Begin
						Insert Into @Resultado
						Values(1, 'AUSENTE')
						
						Return
					End
			End
		Else
			Begin
				Select @FechaFichadaMinima = Min(fecha_fichada) From @Fichada
				Select @FechaFichadaMaxima = Max(fecha_fichada) From @Fichada
				
				Set @Descripcion = ''
				
				If @FechaFichadaMinima >= DateAdd(mi,15,@FechaActualHoraInicio)
					Begin
						--Llega tarde
						Select @Descripcion = 'TARDANZA DE ' + Convert(VarChar(10), DateDiff(mi, @FechaActualHoraInicio, @FechaFichadaMinima)) + ' MINUTOS ---' + Convert(VarChar(25),@FechaActualHoraInicio) + ' -- ' + Convert(VarChar(25),@FechaFichadaMinima)
					End
				
				--If @Fecha >= @FechaActualHoraFin
				--	Begin
						If @FechaFichadaMaxima <= DateAdd(mi,-5,@FechaActualHoraFin)
							Begin
								--Se retira antes
								Select @Descripcion = 'SE RETIRA ' + Convert(VarChar(10), DateDiff(mi, @FechaFichadaMaxima, @FechaActualHoraFin)) + ' MINUTOS ANTES'
							End
					--End
					
				If @Descripcion = ''
					Begin
						Insert Into @Resultado
						Values(2, 'PRESENTE')
					End
				Else
					Begin
						Insert Into @Resultado
						Values(3, @Descripcion)
					End
				
				Return
			End
		
		Insert Into @Resultado
		Values(-4, 'DE BAJA')
		
		Return
	End
