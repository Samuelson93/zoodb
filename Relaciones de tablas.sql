

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
	)
) 

GO
--Relacion de la tabla Especies con clasificaciones

ALTER TABLE [dbo].[Especies]  
WITH CHECK ADD  CONSTRAINT [FK_Especies_Clasificaciones] FOREIGN KEY([idClasificacion])
REFERENCES [dbo].[Clasificaciones] ([idClasificacion])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[Especies] CHECK CONSTRAINT [FK_Especies_Clasificaciones]
GO

--Relación de la tabla Especies con TiposAnimal

ALTER TABLE [dbo].[Especies]  WITH CHECK ADD  CONSTRAINT [FK_Especies_TiposAnimal] FOREIGN KEY([idTipoAnimal])
REFERENCES [dbo].[TiposAnimal] ([idTipoAnimal])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[Especies] CHECK CONSTRAINT [FK_Especies_TiposAnimal]
GO


