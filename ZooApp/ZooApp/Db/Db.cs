using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


namespace ZooApp
{
    public static class Db
    {
        public static SqlConnection conexion = null;
        public static void Conectar()
        {
            try
            {
                string cadenaConexion = @"Server=.\sqlexpress;
                                          Database=zoodb;
                                          User Id=zooUser;
                                          Password=!Curso@2017;";
                conexion = new SqlConnection();
                conexion.ConnectionString = cadenaConexion;
                conexion.Open();


            }
            catch (Exception)
            {
                if (conexion != null)
                {
                    if (conexion.State != ConnectionState.Closed)
                    {
                        conexion.Close();
                    }
                    conexion.Dispose();
                    conexion = null;
                }
            };

        }
        public static bool EstaLaConexionAbierta()
        {
            return conexion.State == ConnectionState.Open;
        }
        public static void Desconectar()
        {
            if (conexion != null)
            {
                if (conexion.State != ConnectionState.Closed)
                {
                    conexion.Close();
                }
            }
        }
        public static List<Especie> DameListaEspecies()
        {
            // CREO EL OBJETO EN EL QUE SE DEVOLVERÁN LOS RESULTADOS
            List<Especie> resultados = new List<Especie>();

            // PREPARO LA LLAMADA AL PROCEDIMIENTO ALMACENADO
            string procedimientoAEjecutar = "dbo.GET_ESPECIES_CLASIFICACION";

            // PREPARAMOS EL COMANDO PARA EJECUTAR EL PROCEDIMIENTO ALMACENADO
            SqlCommand comando = new SqlCommand(procedimientoAEjecutar, conexion);
            comando.CommandType = CommandType.StoredProcedure;
            // EJECUTO EL COMANDO
            SqlDataReader reader = comando.ExecuteReader();
            // RECORRO EL RESULTADO Y LO PASO A LA VARIABLE A DEVOLVER
            while (reader.Read())
            {
                // CREO LA ESPECIE
                Especie especie = new Especie();
                especie.idEspecie = (long)reader["idEspecie"];
                especie.nombre = reader["nombre"].ToString();
                especie.nPatas = (short)reader["nPatas"];
                especie.esMascota = (bool)reader["esMascota"];
                especie.clasificacion = new Clasificacion();
                especie.clasificacion.id = (int)reader["idClasificacion"];
                especie.clasificacion.denominacion = reader["denominacionClasificacion"].ToString();
                especie.tipoAnimal = new TipoAnimal();
                especie.tipoAnimal.id = (long)reader["idTipoAnimal"];
                especie.tipoAnimal.denominacion = reader["denominacionTiposAnimal"].ToString();
                // AÑADO EL especie A LA LISTA DE RESULTADOS
                resultados.Add(especie);

            }

            return resultados;
        }
        public static List<Clasificacion> GetAnimalesClasificacion()
        {
            List<Clasificacion> resultado = new List<Clasificacion>();

            // PREPARO EL PROCEDIMIENTO A EJECUTAR
            string procedimiento = "dbo.GetAnimalesClasificacion";
            // PREPARO EL COMANDO PARA LA Base de datos
            SqlCommand comando = new SqlCommand(procedimiento, conexion);
            // INDICO QUE LO QUE VOY A EJECUTAR ES UN Parametro
            comando.CommandType = CommandType.StoredProcedure;
            // EJECUTO EL COMANDO
            SqlDataReader reader = comando.ExecuteReader();
            // PROCESO EL RESULTADO Y LO MENTO EN LA VARIABLE
            while (reader.Read())
            {
                Clasificacion Clasificacion = new Clasificacion();
                Clasificacion.id = (int)reader["idClasificacion"];
                Clasificacion.denominacion = reader["denominacion"].ToString();


                resultado.Add(Clasificacion);
            }

            return resultado;
        }
        public static List<TipoAnimal> GetAnimalesTipo()
        {
            List<TipoAnimal> resultados = new List<TipoAnimal>();
            string procedimiento = "dbo.GetAnimalesTipo";

            SqlCommand comando = new SqlCommand(procedimiento, conexion);
            comando.CommandType = CommandType.StoredProcedure;
            SqlDataReader reader = comando.ExecuteReader();
            while (reader.Read())
            {
                TipoAnimal tipoAnimal = new TipoAnimal();
                tipoAnimal.id = (long)reader["idTipoAnimal"];
                tipoAnimal.denominacion = reader["denominacion"].ToString();
                resultados.Add(tipoAnimal);
            }


            return resultados;

        }
        public static List<Especie> GetEspeciesId(long id)
        {

            List<Especie> resultados = new List<Especie>();
            string procedimiento = "dbo.GetEspeciesId";
            SqlCommand comando = new SqlCommand(procedimiento, conexion);
            comando.CommandType = CommandType.StoredProcedure;
            SqlParameter parametroId = new SqlParameter();
            parametroId.ParameterName = "id";
            parametroId.SqlDbType = SqlDbType.BigInt;
            parametroId.SqlValue = id;
            comando.Parameters.Add(parametroId);
            SqlDataReader reader = comando.ExecuteReader();
            while (reader.Read())
            {
                // CREO LA ESPECIE
                Especie especie = new Especie();
                especie.idEspecie = (long)reader["idEspecie"];
                especie.nombre = reader["nombre"].ToString();
                especie.nPatas = (short)reader["nPatas"];
                especie.esMascota = (bool)reader["esMascota"];
                especie.clasificacion = new Clasificacion();
                especie.clasificacion.id = (int)reader["idClasificacion"];
                especie.clasificacion.denominacion = reader["denominacionClasificacion"].ToString();
                especie.tipoAnimal = new TipoAnimal();
                especie.tipoAnimal.id = (long)reader["idTipoAnimal"];
                especie.tipoAnimal.denominacion = reader["denominacionTiposAnimal"].ToString();
                // AÑADO LA ESPECIE A LOS RESULTADOS
                resultados.Add(especie);

            }

            return resultados;
        }
        public static int AgregarEspecie(Especie especie)
        {
            string procedimiento = "dbo.AgregarEspecie";
            SqlCommand comando = new SqlCommand(procedimiento, conexion);
            comando.CommandType = CommandType.StoredProcedure;

            SqlParameter parametro = new SqlParameter();
            parametro.ParameterName = "idClasificacion";
            parametro.SqlDbType = SqlDbType.Int;
            parametro.SqlValue = especie.clasificacion.id;
            comando.Parameters.Add(parametro);


            SqlParameter parametroTipoAnimalId = new SqlParameter();
            parametroTipoAnimalId.ParameterName = "idTipoAnimal";
            parametroTipoAnimalId.SqlDbType = SqlDbType.Int;
            parametroTipoAnimalId.SqlValue = especie.tipoAnimal.id;
            comando.Parameters.Add(parametroTipoAnimalId);


            SqlParameter parametroNombre = new SqlParameter();
            parametroNombre.ParameterName = "nombre";
            parametroNombre.SqlDbType = SqlDbType.NVarChar;
            parametroNombre.SqlValue = especie.nombre;
            comando.Parameters.Add(parametroNombre);

            SqlParameter parametroNPatas = new SqlParameter();
            parametroNPatas.ParameterName = "nPatas";
            parametroNPatas.SqlDbType = SqlDbType.SmallInt;
            parametroNPatas.SqlValue = especie.nPatas;
            comando.Parameters.Add(parametroNPatas);

            SqlParameter parametroEsMascota = new SqlParameter();
            parametroEsMascota.ParameterName = "esMascota";
            parametroEsMascota.SqlDbType = SqlDbType.Bit;
            parametroEsMascota.SqlValue = especie.esMascota;
            comando.Parameters.Add(parametroEsMascota);

            int filasAfectadas = comando.ExecuteNonQuery();


            return 0;
        }
        public static int ActualizarEspecie(long id, Especie especie)
        {
            string procedimiento = "dbo.ActualizarEspecie";
            SqlCommand comando = new SqlCommand(procedimiento, conexion);
            comando.CommandType = CommandType.StoredProcedure;

            SqlParameter parametroEspecie = new SqlParameter();
            parametroEspecie.ParameterName = "idEspecie";
            parametroEspecie.SqlDbType = SqlDbType.BigInt;
            parametroEspecie.SqlValue = id;
            comando.Parameters.Add(parametroEspecie);

            
            SqlParameter parametro = new SqlParameter();
            parametro.ParameterName = "idClasificacion";
            parametro.SqlDbType = SqlDbType.Int;
            parametro.SqlValue = especie.clasificacion.id;
            comando.Parameters.Add(parametro);


            SqlParameter parametroTipoAnimalId = new SqlParameter();
            parametroTipoAnimalId.ParameterName = "idTipoAnimal";
            parametroTipoAnimalId.SqlDbType = SqlDbType.Int;
            parametroTipoAnimalId.SqlValue = especie.tipoAnimal.id;
            comando.Parameters.Add(parametroTipoAnimalId);


            SqlParameter parametroNombre = new SqlParameter();
            parametroNombre.ParameterName = "nombre";
            parametroNombre.SqlDbType = SqlDbType.NVarChar;
            parametroNombre.SqlValue = especie.nombre;
            comando.Parameters.Add(parametroNombre);

            SqlParameter parametroNPatas = new SqlParameter();
            parametroNPatas.ParameterName = "nPatas";
            parametroNPatas.SqlDbType = SqlDbType.SmallInt;
            parametroNPatas.SqlValue = especie.nPatas;
            comando.Parameters.Add(parametroNPatas);

            SqlParameter parametroEsMascota = new SqlParameter();
            parametroEsMascota.ParameterName = "esMascota";
            parametroEsMascota.SqlDbType = SqlDbType.Bit;
            parametroEsMascota.SqlValue = especie.esMascota;
            comando.Parameters.Add(parametroEsMascota);

            int filasAfectadas = comando.ExecuteNonQuery();


            return filasAfectadas;

        }
        public static int EliminarEspecie(long id)
        {
            string procedimiento = "dbo.EliminarEspecie";
            SqlCommand comando = new SqlCommand(procedimiento, conexion);
            comando.CommandType = CommandType.StoredProcedure;

            SqlParameter parametroEspecie = new SqlParameter();
            parametroEspecie.ParameterName = "idEspecie";
            parametroEspecie.SqlDbType = SqlDbType.BigInt;
            parametroEspecie.SqlValue = id;
            comando.Parameters.Add(parametroEspecie);

            int filasAfectadas = comando.ExecuteNonQuery();


            return filasAfectadas;



        }
        public static List<Clasificacion> GetClasificacionesId(int id)
        {
            List<Clasificacion> resultado = new List<Clasificacion>();
            string procedimiento = "dbo.GetClasificacionesId";
            SqlCommand comando = new SqlCommand(procedimiento, conexion);
            comando.CommandType = CommandType.StoredProcedure;
            SqlParameter parametroId = new SqlParameter();
            parametroId.ParameterName = "id";
            parametroId.SqlDbType = SqlDbType.Int;
            parametroId.SqlValue = id;
            comando.Parameters.Add(parametroId);
            SqlDataReader reader = comando.ExecuteReader();
            while (reader.Read())
            {
                Clasificacion Clasificacion = new Clasificacion();
                Clasificacion.id = (int)reader["idClasificacion"];
                Clasificacion.denominacion = reader["denominacionClasificacion"].ToString();


                resultado.Add(Clasificacion);
            }

            return resultado;

        }
        public static int AgregarClasificacion(Clasificacion clasificacion)
        {
            string procedimiento = "dbo.AgregarClasificacion";
            SqlCommand comando = new SqlCommand(procedimiento, conexion);
            comando.CommandType = CommandType.StoredProcedure;

            SqlParameter parametro = new SqlParameter();
            parametro.ParameterName = "denominacion";
            parametro.SqlDbType = SqlDbType.NVarChar;
            parametro.SqlValue = clasificacion.denominacion;
            comando.Parameters.Add(parametro);




            int filasAfectadas = comando.ExecuteNonQuery();


            return 0;
        }
        public static int ActualizarClasificacion(int id,Clasificacion clasificacion)
        {
            string procedimiento = "dbo.ActualizarClasificacion";
            SqlCommand comando = new SqlCommand(procedimiento, conexion);
            comando.CommandType = CommandType.StoredProcedure;

            SqlParameter parametroId = new SqlParameter();
            parametroId.ParameterName = "id";
            parametroId.SqlDbType = SqlDbType.BigInt;
            parametroId.SqlValue = id;
            comando.Parameters.Add(parametroId);

            SqlParameter parametro = new SqlParameter();
            parametro.ParameterName = "denominacion";
            parametro.SqlDbType = SqlDbType.NVarChar;
            parametro.SqlValue = clasificacion.denominacion;
            comando.Parameters.Add(parametro);




            int filasAfectadas = comando.ExecuteNonQuery();


            return filasAfectadas;
        }
        public static int EliminarClasificacion(int id)
        {
            string procedimiento = "dbo.EliminarClasificacion";
            SqlCommand comando = new SqlCommand(procedimiento, conexion);
            comando.CommandType = CommandType.StoredProcedure;

            SqlParameter parametro = new SqlParameter();
            parametro.ParameterName = "idClasificacion";
            parametro.SqlDbType = SqlDbType.Int;
            parametro.SqlValue = id;
            comando.Parameters.Add(parametro);

            int filasAfectadas = comando.ExecuteNonQuery();


            return filasAfectadas;


        }
        public static List<TipoAnimal> GetTipoAnimalesId(int id)
        {
            List<TipoAnimal> resultado = new List<TipoAnimal>();
            string procedimiento = "dbo.GetTiposAnimalesId";
            SqlCommand comando = new SqlCommand(procedimiento, conexion);
            comando.CommandType = CommandType.StoredProcedure;
            SqlParameter parametroId = new SqlParameter();
            parametroId.ParameterName = "id";
            parametroId.SqlDbType = SqlDbType.Int;
            parametroId.SqlValue = id;
            comando.Parameters.Add(parametroId);
            SqlDataReader reader = comando.ExecuteReader();
            while (reader.Read())
            {
                TipoAnimal tipoAnimal = new TipoAnimal();
                tipoAnimal.id = (long)reader["idTipoAnimal"];
                tipoAnimal.denominacion = reader["denominacionTipoAnimal"].ToString();
                resultado.Add(tipoAnimal);
            }


            return resultado;


        }
        public static int AgregarTipoAnimal(TipoAnimal tipoAnimal)
        {
            string procedimiento = "dbo.AgregarTipoAnimal";
            SqlCommand comando = new SqlCommand(procedimiento, conexion);
            comando.CommandType = CommandType.StoredProcedure;

            SqlParameter parametro = new SqlParameter();
            parametro.ParameterName = "denominacion";
            parametro.SqlDbType = SqlDbType.NVarChar;
            parametro.SqlValue = tipoAnimal.denominacion;
            comando.Parameters.Add(parametro);




            int filasAfectadas = comando.ExecuteNonQuery();


            return 0;
        }
        public static int ActualizarTipoAnimal(int id, TipoAnimal tipoAnimal)
        {
            string procedimiento = "dbo.ActualizarTipoAnimal";
            SqlCommand comando = new SqlCommand(procedimiento, conexion);
            comando.CommandType = CommandType.StoredProcedure;

            SqlParameter parametroId = new SqlParameter();
            parametroId.ParameterName = "id";
            parametroId.SqlDbType = SqlDbType.BigInt;
            parametroId.SqlValue = id;
            comando.Parameters.Add(parametroId);

            SqlParameter parametro = new SqlParameter();
            parametro.ParameterName = "denominacion";
            parametro.SqlDbType = SqlDbType.NVarChar;
            parametro.SqlValue = tipoAnimal.denominacion;
            comando.Parameters.Add(parametro);




            int filasAfectadas = comando.ExecuteNonQuery();


            return filasAfectadas;
        }
        public static int EliminarTipoAnimal(int id)
        {
            string procedimiento = "dbo.EliminarTipoAnimal";
            SqlCommand comando = new SqlCommand(procedimiento, conexion);
            comando.CommandType = CommandType.StoredProcedure;

            SqlParameter parametro = new SqlParameter();
            parametro.ParameterName = "idTipoAnimal";
            parametro.SqlDbType = SqlDbType.BigInt;
            parametro.SqlValue = id;
            comando.Parameters.Add(parametro);

            int filasAfectadas = comando.ExecuteNonQuery();


            return filasAfectadas;


        }


    }
}