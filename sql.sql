USE [MVCCRUD]
GO
/****** Object:  Table [dbo].[es]    Script Date: 06/01/2023 01:45:58 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[es](
	[id] [int] IDENTITY(0,1) NOT NULL,
	[id_productos] [int] NOT NULL,
	[cantidad] [int] NOT NULL,
	[descripcion] [varchar](100) NULL,
	[fecha] [datetime] NULL,
 CONSTRAINT [e_s_PK] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[productos]    Script Date: 06/01/2023 01:45:58 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[productos](
	[id] [int] IDENTITY(0,1) NOT NULL,
	[codigo] [varchar](100) NOT NULL,
	[nombre] [varchar](150) NOT NULL,
	[marca] [varchar](150) NOT NULL,
	[stock] [int] NOT NULL,
	[precio] [int] NOT NULL,
 CONSTRAINT [productos_PK] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[usuarios]    Script Date: 06/01/2023 01:45:58 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[usuarios](
	[id] [int] IDENTITY(0,1) NOT NULL,
	[nombre] [varchar](100) NULL,
	[fecha] [date] NULL,
	[clave] [varchar](100) NULL,
 CONSTRAINT [usuarios_PK] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[es] ON 

INSERT [dbo].[es] ([id], [id_productos], [cantidad], [descripcion], [fecha]) VALUES (32, 4, 3, N'Entrada', CAST(N'2023-01-06T13:36:13.563' AS DateTime))
SET IDENTITY_INSERT [dbo].[es] OFF
GO
SET IDENTITY_INSERT [dbo].[productos] ON 

INSERT [dbo].[productos] ([id], [codigo], [nombre], [marca], [stock], [precio]) VALUES (3, N'aa', N'ddd', N'fdgfdg', 60, 500)
INSERT [dbo].[productos] ([id], [codigo], [nombre], [marca], [stock], [precio]) VALUES (4, N'2265', N'oopp', N'oppp', 23, 33)
INSERT [dbo].[productos] ([id], [codigo], [nombre], [marca], [stock], [precio]) VALUES (6, N'34dffd', N'COCA-COLA', N'COCA-COLA', 15, 25)
SET IDENTITY_INSERT [dbo].[productos] OFF
GO
SET IDENTITY_INSERT [dbo].[usuarios] ON 

INSERT [dbo].[usuarios] ([id], [nombre], [fecha], [clave]) VALUES (1, N'user', CAST(N'2022-01-12' AS Date), N'sad')
SET IDENTITY_INSERT [dbo].[usuarios] OFF
GO
ALTER TABLE [dbo].[es] ADD  DEFAULT (getdate()) FOR [fecha]
GO
ALTER TABLE [dbo].[es]  WITH CHECK ADD  CONSTRAINT [es_FK] FOREIGN KEY([id_productos])
REFERENCES [dbo].[productos] ([id])
GO
ALTER TABLE [dbo].[es] CHECK CONSTRAINT [es_FK]
GO
/****** Object:  StoredProcedure [dbo].[actualizar_productos]    Script Date: 06/01/2023 01:45:58 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[actualizar_productos]
 @id INT,
   @codigo VARCHAR(100),
    @nombre VARCHAR(150),
    @marca VARCHAR(150),
    @stock INT,
    @precio INT
AS
BEGIN

UPDATE productos SET codigo=@codigo,nombre=@nombre,marca=@marca,stock=@stock,precio=@precio WHERE id=@id

END
GO
/****** Object:  StoredProcedure [dbo].[buscar_productos]    Script Date: 06/01/2023 01:45:58 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[buscar_productos]
    @Id INT
AS
BEGIN
    SELECT *
    FROM productos
    WHERE Id=@Id
END
GO
/****** Object:  StoredProcedure [dbo].[insertar_productos]    Script Date: 06/01/2023 01:45:58 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[insertar_productos]
    @codigo VARCHAR(100),
    @nombre VARCHAR(150),
    @marca VARCHAR(150),
    @stock INT,
    @precio INT
AS
BEGIN
    INSERT INTO productos
    VALUES(@codigo, @nombre,@marca, @stock, @precio)
END
GO
/****** Object:  StoredProcedure [dbo].[istar_productos]    Script Date: 06/01/2023 01:45:58 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[istar_productos]
AS
BEGIN
    SELECT *
    FROM productos
END
GO
/****** Object:  StoredProcedure [dbo].[liminar_productos]    Script Date: 06/01/2023 01:45:58 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[liminar_productos]
    @Id INT
AS
BEGIN
    DELETE productos WHERE Id=@Id
END
GO
/****** Object:  StoredProcedure [dbo].[listar_es]    Script Date: 06/01/2023 01:45:58 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[listar_es]
AS
BEGIN
    SELECT *
    FROM es
END
GO
/****** Object:  StoredProcedure [dbo].[ProcedureEntradas]    Script Date: 06/01/2023 01:45:58 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ProcedureEntradas]
	@id_productos int,
	@cantidad int,
	@descripcion NVARCHAR(10),
	
	@ModeProcedure int ,
	@MessageCode int OUTPUT,
	@MessageValue nvarchar(200) OUTPUT
AS
SET NOCOUNT ON;
BEGIN TRY
BEGIN TRANSACTION
	 IF(@ModeProcedure = 1 )
		begin
			IF EXISTS (SELECT * FROM productos WHERE id = @id_productos)
				begin
					update productos set 
						stock = stock + @cantidad
					WHERE id = @id_productos;
					INSERT INTO es(id_productos, cantidad, descripcion)VALUES(@id_productos, @cantidad, @descripcion);
					
					set @MessageCode = 0
					set @MessageValue = 'No existe ningun registro'
				end
			else
				begin
					set @MessageCode = 200
					set @MessageValue = 'No hay suficiente stock'
				end
		end
	
	ELSE
		begin
			set @MessageCode = -100
			set @MessageValue = 'No existe ningun registro'
		end

	COMMIT TRANSACTION
	return_value:
		SET @MessageCode =  @MessageCode;
		SET @MessageValue =  @MessageValue;
END TRY
BEGIN CATCH
	return_result: 
		SET @MessageCode = -200;
		SET @MessageValue = CONVERT(NVARCHAR ,ERROR_MESSAGE());
	ROLLBACK TRANSACTION
END CATCH
GO
/****** Object:  StoredProcedure [dbo].[ProcedureES]    Script Date: 06/01/2023 01:45:58 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ProcedureES]
	@id_productos int,
	@cantidad int,
	@descripcion NVARCHAR(10),
	
	@ModeProcedure int ,
	@MessageCode int OUTPUT,
	@MessageValue nvarchar(200) OUTPUT
AS
SET NOCOUNT ON;
BEGIN TRY
BEGIN TRANSACTION
	IF(@ModeProcedure =1 )
		BEGIN
			INSERT INTO es(id_productos, cantidad, descripcion)VALUES(@id_productos, @cantidad, @descripcion);
			set @MessageCode = 0
			set @MessageValue = 'Cambios guardados'
		END
	ELSE IF(@ModeProcedure = 2 )
		begin
			IF @cantidad < (SELECT stock FROM productos WHERE id = @id_productos)
				begin
					update productos set 
						stock = stock - @cantidad
					WHERE id = @id_productos;
					INSERT INTO es(id_productos, cantidad, descripcion)VALUES(@id_productos, @cantidad, @descripcion);
					
					set @MessageCode = 0
					set @MessageValue = 'Cambios guardados'
				end
			else
				begin
					set @MessageCode = 200
					set @MessageValue = 'No hay suficiente stock'
				end
		end
	ELSE IF(@ModeProcedure = 3 )
		begin
			IF EXISTS(SELECT * FROM productos WHERE id = @id_productos)
				begin
					update productos set 
						stock = stock + @cantidad
					WHERE id = @id_productos;
					INSERT INTO es(id_productos, cantidad, descripcion)VALUES(@id_productos, @cantidad, @descripcion);
					
					set @MessageCode = 0
					set @MessageValue = 'Cambios guardados'
				end
			else
				begin
					set @MessageCode = 200
					set @MessageValue = 'error'
				end
		end
	ELSE
		begin
			set @MessageCode = -100
			set @MessageValue = 'No existe ningun registro'
		end

	COMMIT TRANSACTION
	return_value:
		SET @MessageCode =  @MessageCode;
		SET @MessageValue =  @MessageValue;
END TRY
BEGIN CATCH
	return_result: 
		SET @MessageCode = -200;
		SET @MessageValue = CONVERT(NVARCHAR ,ERROR_MESSAGE());
	ROLLBACK TRANSACTION
END CATCH
GO
/****** Object:  StoredProcedure [dbo].[ProcedureSalidas]    Script Date: 06/01/2023 01:45:58 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ProcedureSalidas]
	@id_productos int,
	@cantidad int,
	@descripcion NVARCHAR(10),
	
	@ModeProcedure int ,
	@MessageCode int OUTPUT,
	@MessageValue nvarchar(200) OUTPUT
AS
SET NOCOUNT ON;
BEGIN TRY
BEGIN TRANSACTION
	 IF(@ModeProcedure = 1 )
		begin
			IF @cantidad <= (SELECT stock FROM productos WHERE id = @id_productos)
				begin
					update productos set 
						stock = stock - @cantidad
					WHERE id = @id_productos;
					INSERT INTO es(id_productos, cantidad, descripcion)VALUES(@id_productos, @cantidad, @descripcion);
					
					set @MessageCode = 0
					set @MessageValue = 'Cambios guardados'
				end
			else
				begin
					set @MessageCode = 200
					set @MessageValue = 'No hay suficiente stock'
				end
		end
	
	ELSE
		begin
			set @MessageCode = -100
			set @MessageValue = 'No existe ningun registro'
		end

	COMMIT TRANSACTION
	return_value:
		SET @MessageCode =  @MessageCode;
		SET @MessageValue =  @MessageValue;
END TRY
BEGIN CATCH
	return_result: 
		SET @MessageCode = -200;
		SET @MessageValue = CONVERT(NVARCHAR ,ERROR_MESSAGE());
	ROLLBACK TRANSACTION
END CATCH
GO
