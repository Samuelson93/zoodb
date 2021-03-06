USE [zoodb]
GO
/****** Object:  Table [dbo].[Clasificaciones]    Script Date: 15/06/2017 21:29:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Clasificaciones](
	[idClasificacion] [int] IDENTITY(1,1) NOT NULL,
	[denominacion] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Clasificaciones] PRIMARY KEY CLUSTERED 
(
	[idClasificacion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Especies]    Script Date: 15/06/2017 21:29:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Especies](
	[idEspecie] [bigint] IDENTITY(1,1) NOT NULL,
	[idTipoAnimal] [bigint] NOT NULL,
	[idClasificacion] [int] NOT NULL,
	[nombre] [nvarchar](50) NOT NULL,
	[nPatas] [smallint] NOT NULL,
	[esMascota] [bit] NOT NULL,
 CONSTRAINT [PK_Especies] PRIMARY KEY CLUSTERED 
(
	[idEspecie] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TiposAnimal]    Script Date: 15/06/2017 21:29:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TiposAnimal](
	[idTipoAnimal] [bigint] IDENTITY(1,1) NOT NULL,
	[denominacion] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_TiposAnimal] PRIMARY KEY CLUSTERED 
(
	[idTipoAnimal] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[Clasificaciones] ON 

INSERT [dbo].[Clasificaciones] ([idClasificacion], [denominacion]) VALUES (1, N'vertebrados')
INSERT [dbo].[Clasificaciones] ([idClasificacion], [denominacion]) VALUES (2, N'invertebrados')
SET IDENTITY_INSERT [dbo].[Clasificaciones] OFF
SET IDENTITY_INSERT [dbo].[Especies] ON 

INSERT [dbo].[Especies] ([idEspecie], [idTipoAnimal], [idClasificacion], [nombre], [nPatas], [esMascota]) VALUES (16, 1, 1, N'Perro', 4, 1)
INSERT [dbo].[Especies] ([idEspecie], [idTipoAnimal], [idClasificacion], [nombre], [nPatas], [esMascota]) VALUES (17, 1, 1, N'Leon', 4, 0)
INSERT [dbo].[Especies] ([idEspecie], [idTipoAnimal], [idClasificacion], [nombre], [nPatas], [esMascota]) VALUES (18, 2, 1, N'Cebra', 4, 0)
INSERT [dbo].[Especies] ([idEspecie], [idTipoAnimal], [idClasificacion], [nombre], [nPatas], [esMascota]) VALUES (19, 1, 2, N'Avispa', 6, 0)
INSERT [dbo].[Especies] ([idEspecie], [idTipoAnimal], [idClasificacion], [nombre], [nPatas], [esMascota]) VALUES (20, 2, 1, N'Conjeo', 4, 1)
INSERT [dbo].[Especies] ([idEspecie], [idTipoAnimal], [idClasificacion], [nombre], [nPatas], [esMascota]) VALUES (21, 2, 1, N'Oso Panda', 4, 0)
INSERT [dbo].[Especies] ([idEspecie], [idTipoAnimal], [idClasificacion], [nombre], [nPatas], [esMascota]) VALUES (22, 4, 1, N'Ornitorrinco', 4, 0)
INSERT [dbo].[Especies] ([idEspecie], [idTipoAnimal], [idClasificacion], [nombre], [nPatas], [esMascota]) VALUES (23, 5, 1, N'Canguro', 2, 0)
INSERT [dbo].[Especies] ([idEspecie], [idTipoAnimal], [idClasificacion], [nombre], [nPatas], [esMascota]) VALUES (25, 1, 2, N'Escorpion', 8, 0)
INSERT [dbo].[Especies] ([idEspecie], [idTipoAnimal], [idClasificacion], [nombre], [nPatas], [esMascota]) VALUES (26, 1, 2, N'Araña', 8, 0)
SET IDENTITY_INSERT [dbo].[Especies] OFF
SET IDENTITY_INSERT [dbo].[TiposAnimal] ON 

INSERT [dbo].[TiposAnimal] ([idTipoAnimal], [denominacion]) VALUES (1, N'carnivoros')
INSERT [dbo].[TiposAnimal] ([idTipoAnimal], [denominacion]) VALUES (2, N'herbivoros')
INSERT [dbo].[TiposAnimal] ([idTipoAnimal], [denominacion]) VALUES (3, N'omnivoros')
INSERT [dbo].[TiposAnimal] ([idTipoAnimal], [denominacion]) VALUES (4, N'oviparos')
INSERT [dbo].[TiposAnimal] ([idTipoAnimal], [denominacion]) VALUES (5, N'viviparos')
SET IDENTITY_INSERT [dbo].[TiposAnimal] OFF
ALTER TABLE [dbo].[Especies] ADD  CONSTRAINT [DF_Especies_nPatas]  DEFAULT ((0)) FOR [nPatas]
GO
ALTER TABLE [dbo].[Especies]  WITH CHECK ADD  CONSTRAINT [FK_Especies_Clasificaciones] FOREIGN KEY([idClasificacion])
REFERENCES [dbo].[Clasificaciones] ([idClasificacion])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Especies] CHECK CONSTRAINT [FK_Especies_Clasificaciones]
GO
ALTER TABLE [dbo].[Especies]  WITH CHECK ADD  CONSTRAINT [FK_Especies_TiposAnimal] FOREIGN KEY([idTipoAnimal])
REFERENCES [dbo].[TiposAnimal] ([idTipoAnimal])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Especies] CHECK CONSTRAINT [FK_Especies_TiposAnimal]
GO
/****** Object:  StoredProcedure [dbo].[ActualizarClasificacion]    Script Date: 15/06/2017 21:29:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ActualizarClasificacion]
	@id int
	,@denominacion nvarchar(50)

AS
BEGIN
	UPDATE Clasificaciones SET
		denominacion = @denominacion
	WHERE idClasificacion = @id
END
GO
/****** Object:  StoredProcedure [dbo].[ActualizarEspecie]    Script Date: 15/06/2017 21:29:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ActualizarEspecie]
	@idEspecie bigint
	,@idClasificacion int
	,@idTipoAnimal bigint
	,@nombre nvarchar(50)
	,@nPatas smallint
	,@esMascota bit
	
	
AS
BEGIN
	UPDATE Especies SET
		idClasificacion = @idClasificacion
		,idTipoAnimal = @idTipoAnimal
		,nombre = @nombre
		,nPatas = @nPatas
		,esMascota = @esMascota
	WHERE idEspecie = @idEspecie
END
GO
/****** Object:  StoredProcedure [dbo].[ActualizarTipoAnimal]    Script Date: 15/06/2017 21:29:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ActualizarTipoAnimal]
	@id int
	,@denominacion nvarchar(50)

AS
BEGIN
	UPDATE TiposAnimal SET
		denominacion = @denominacion
	WHERE idTipoAnimal = @id
END
GO
/****** Object:  StoredProcedure [dbo].[AgregarClasificacion]    Script Date: 15/06/2017 21:29:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AgregarClasificacion]
	@denominacion nvarchar(50)
AS 
BEGIN
	INSERT INTO Clasificaciones(denominacion) 
	VALUES (@denominacion)
END
GO
/****** Object:  StoredProcedure [dbo].[AgregarEspecie]    Script Date: 15/06/2017 21:29:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AgregarEspecie]
	@idClasificacion int
	,@idTipoAnimal bigint
	,@nombre nvarchar(50)
	,@nPatas smallint
	,@esMascota bit
AS 
BEGIN
	INSERT INTO Especies(idClasificacion,idTipoAnimal,nombre,nPatas,esMascota) 
	VALUES (@idClasificacion,@idTipoAnimal,@nombre,@nPatas,@esMascota)
END
GO
/****** Object:  StoredProcedure [dbo].[AgregarTipoAnimal]    Script Date: 15/06/2017 21:29:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AgregarTipoAnimal]
	@denominacion nvarchar(50)
AS 
BEGIN
	INSERT INTO TiposAnimal(denominacion) 
	VALUES (@denominacion)
END

GO
/****** Object:  StoredProcedure [dbo].[EliminarClasificacion]    Script Date: 15/06/2017 21:29:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[EliminarClasificacion]
	@idClasificacion int

	
AS
BEGIN
	DELETE FROM Clasificaciones WHERE idClasificacion =@idClasificacion
END
GO
/****** Object:  StoredProcedure [dbo].[EliminarEspecie]    Script Date: 15/06/2017 21:29:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[EliminarEspecie]
	@idEspecie bigint

	
AS
BEGIN
	DELETE FROM Especies WHERE idEspecie =@idEspecie
END
GO
/****** Object:  StoredProcedure [dbo].[EliminarTipoAnimal]    Script Date: 15/06/2017 21:29:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[EliminarTipoAnimal]
	@idTipoAnimal bigint

	
AS
BEGIN
	DELETE FROM TiposAnimal WHERE idTipoAnimal =@idTipoAnimal
END
GO
/****** Object:  StoredProcedure [dbo].[GET_ESPECIES_CLASIFICACION]    Script Date: 15/06/2017 21:29:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GET_ESPECIES_CLASIFICACION]
	
AS 
BEGIN
SELECT
	  Clasificaciones.denominacion as denominacionClasificacion
	, TiposAnimal.denominacion as denominacionTiposAnimal
	, Especies.idClasificacion
	, Especies.idTipoAnimal
	, Especies.idEspecie, Especies.nombre
	, Especies.nPatas, Especies.esMascota

FROM Clasificaciones
	INNER JOIN Especies on Clasificaciones.idClasificacion = Especies.idClasificacion
	INNER JOIN TiposAnimal on Especies.idTipoAnimal = TiposAnimal.idTipoAnimal

GROUP BY
	Clasificaciones.denominacion
	, TiposAnimal.denominacion
	, Especies.idClasificacion
	, Especies.idTipoAnimal
	, Especies.idEspecie, Especies.nombre
	, Especies.nPatas, Especies.esMascota
ORDER BY Clasificaciones.denominacion
	

END
GO
/****** Object:  StoredProcedure [dbo].[GetAnimalesClasificacion]    Script Date: 15/06/2017 21:29:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--Procedimientos Almacenados

CREATE PROCEDURE [dbo].[GetAnimalesClasificacion]
AS
BEGIN
	SELECT idClasificacion , denominacion FROM Clasificaciones
END	
GO
/****** Object:  StoredProcedure [dbo].[GetAnimalesTipo]    Script Date: 15/06/2017 21:29:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE  [dbo].[GetAnimalesTipo]	
AS
BEGIN
	SELECT idTipoAnimal, denominacion FROM TiposAnimal			
END
GO
/****** Object:  StoredProcedure [dbo].[GetClasificacionesId]    Script Date: 15/06/2017 21:29:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetClasificacionesId]
	@id int	
AS 
BEGIN
SELECT
	 Clasificaciones.idClasificacion
	,Clasificaciones.denominacion as denominacionClasificacion
	

FROM Clasificaciones
	
WHERE Clasificaciones.idClasificacion = @id

GROUP BY
	 Clasificaciones.idClasificacion
	,Clasificaciones.denominacion

ORDER BY Clasificaciones.denominacion

END
GO
/****** Object:  StoredProcedure [dbo].[GetEspeciesId]    Script Date: 15/06/2017 21:29:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetEspeciesId]
	@id bigint	
AS 
BEGIN
SELECT
	  Clasificaciones.denominacion as denominacionClasificacion
	, TiposAnimal.denominacion as denominacionTiposAnimal
	, Especies.idClasificacion
	, Especies.idTipoAnimal
	, Especies.idEspecie, Especies.nombre
	, Especies.nPatas, Especies.esMascota

FROM Clasificaciones
	INNER JOIN Especies on Clasificaciones.idClasificacion = Especies.idClasificacion
	INNER JOIN TiposAnimal on Especies.idTipoAnimal = TiposAnimal.idTipoAnimal
WHERE Especies.idEspecie = @id

GROUP BY
	Clasificaciones.denominacion
	, TiposAnimal.denominacion
	, Especies.idClasificacion
	, Especies.idTipoAnimal
	, Especies.idEspecie, Especies.nombre
	, Especies.nPatas, Especies.esMascota
ORDER BY Clasificaciones.denominacion

END
GO
/****** Object:  StoredProcedure [dbo].[GetTiposAnimalesId]    Script Date: 15/06/2017 21:29:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetTiposAnimalesId]
	@id bigint	
AS 
BEGIN
SELECT
	 TiposAnimal.idTipoAnimal
	,TiposAnimal.denominacion as denominacionTipoAnimal
	

FROM TiposAnimal
	
WHERE TiposAnimal.idTipoAnimal = @id

GROUP BY
	TiposAnimal.idTipoAnimal
	,TiposAnimal.denominacion

ORDER BY TiposAnimal.denominacion

END
GO
