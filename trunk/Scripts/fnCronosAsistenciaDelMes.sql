Drop Function fnCronosAsistenciaDelMes
Go
Create Function fnCronosAsistenciaDelMes
(@perId Int)
Returns @Resultado Table(	Fecha	DateTime		,
							Codigo	SmallInt		,
							Estado	VarChar(250)	,
							Entrada	DateTime		,
							Salida	DateTime		)
As
	Begin
		Declare @DNI BigInt
		Declare @Fecha DateTime
		Declare @Codigo		SmallInt		,
				@Estado		VarChar(250)	,
				@Entrada	DateTime		,
				@Salida		DateTime

		Set @Fecha = Convert(DateTime, Convert(VarChar(4),Year(GetDate())) + Right('0' + Convert(VarChar(2),Month(GetDate())), 2) + '01')
		
		Select @DNI = perDNI
		From catPersona
		Where perId = @perId
		
		While Convert(Date, @Fecha) < Convert(Date, DateAdd(dd, 1, GetDate()))
			Begin
				Set @Codigo = Null
				Set @Estado = Null
				Set @Entrada = Null
				Set @Salida = Null
				
				Select @Codigo = Codigo, @Estado = Estado
				From fnCronosAsistenciaDeUnDiaEstado(@DNI, @perId, Day(@Fecha))

				Select @Entrada = Entrada, @Salida = Salida
				From fnCronosAsistenciaDeUnDia(@DNI, Day(@Fecha))

				Insert Into @Resultado
				Values(@Fecha, @Codigo, @Estado, @Entrada, @Salida)
				
				Set @Fecha = DateAdd(dd, 1, @Fecha)
			End
		
		Return
	End
