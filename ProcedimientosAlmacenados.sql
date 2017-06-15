--Procedimientos Almacenados

--Conseguir animales según su clasificación

CREATE PROCEDURE GetAnimalesClasificacion
AS
BEGIN
	SELECT idClasificacion , denominacion FROM Clasificaciones
END	

--Conseguir animales segun su tipo

CREATE PROCEDURE  GetAnimalesTipo	
AS
BEGIN
	SELECT idTipoAnimal, denominacion FROM TiposAnimal			
END

--Procedimiento almacenado 

CREATE PROCEDURE GET_ESPECIES_CLASIFICACION
	
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

--Procedimiento Almacenado para obtener Especies por id 

CREATE PROCEDURE GetEspeciesId
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
--Procedimiento para agregar especie
ALTER PROCEDURE [dbo].[AgregarEspecie]
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

--Procedimiento para actualizar Especies

CREATE PROCEDURE ActualizarEspecie
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

CREATE PROCEDURE EliminarEspecie
	@idEspecie bigint
	,@idClasificacion int
	,@idTipoAnimal bigint
	,@nombre nvarchar(50)
	,@nPatas smallint
	,@esMascota bit
	
AS
BEGIN
	DELETE FROM Especies WHERE idEspecie =@idEspecie
END

--Procedimiento Almacenado para Eliminar Especies

ALTER PROCEDURE EliminarEspecie
	@idEspecie bigint

	
AS
BEGIN
	DELETE FROM Especies WHERE idEspecie =@idEspecie
END

--Procedimiento Almacenado GetClasificaciones por id

ALTER PROCEDURE GetClasificacionesId
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

--Procedimiento Almacenado GetTiposAnimalesId

CREATE PROCEDURE GetTiposAnimalesId
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

--Procedimiento Almacenado AgregarClasificacion

ALTER PROCEDURE AgregarClasificacion
	@denominacion nvarchar(50)
AS 
BEGIN
	INSERT INTO Clasificaciones(denominacion) 
	VALUES (@denominacion)
END

--Procedimiento Almacenado ActualizarClasificacion

CREATE PROCEDURE ActualizarClasificacion
	@id int
	,@denominacion nvarchar(50)

AS
BEGIN
	UPDATE Clasificaciones SET
		denominacion = @denominacion
	WHERE idClasificacion = @id
END

--Procedimiento Almacenado EliminarClasificacion

CREATE PROCEDURE EliminarClasificacion
	@idClasificacion int

	
AS
BEGIN
	DELETE FROM Clasificaciones WHERE idClasificacion =@idClasificacion
END


--Procedimiento Almacenado AgregarTipoAnimal

CREATE PROCEDURE AgregarTipoAnimal
	@denominacion nvarchar(50)
AS 
BEGIN
	INSERT INTO TiposAnimal(denominacion) 
	VALUES (@denominacion)
END

--Procedimiento Almacenado ActualizarTipoAnimal

CREATE PROCEDURE ActualizarTipoAnimal
	@id int
	,@denominacion nvarchar(50)

AS
BEGIN
	UPDATE TiposAnimal SET
		denominacion = @denominacion
	WHERE idTipoAnimal = @id
END

--Procedimiento Almacenado EliminarTipoAnimal
CREATE PROCEDURE EliminarTipoAnimal
	@idTipoAnimal bigint

	
AS
BEGIN
	DELETE FROM TiposAnimal WHERE idTipoAnimal =@idTipoAnimal
END





