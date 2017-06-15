using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ZooApp.Controllers
{
    public class ClasificacionController : ApiController
    {
        // GET: api/Clasificacion
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        
        public RespuestaAPI Get()
        {
            RespuestaAPI resultado = new RespuestaAPI();
            List<Clasificacion> Clasificaciones = new List<Clasificacion>();
            try
            {
                Db.Conectar();
                if (Db.EstaLaConexionAbierta())
                {
                    Clasificaciones = Db.GetAnimalesClasificacion();
                    resultado.error = "";
                }
                Db.Desconectar();
            }
            catch
            {
                resultado.error = "Ha habido un error";
            }
            resultado.totalElementos = Clasificaciones.Count;
            resultado.dataClasificaciones = Clasificaciones;
            return resultado;
        }

        // GET: api/Clasificacion/5
        public RespuestaAPI Get(int id)
        {
            RespuestaAPI resultado = new RespuestaAPI();
            List<Clasificacion> Clasificaciones = new List<Clasificacion>();
            try
            {
                Db.Conectar();
                if (Db.EstaLaConexionAbierta())
                {
                    Clasificaciones = Db.GetClasificacionesId(id);
                    resultado.error = "";
                }
                Db.Desconectar();
            }
            catch(Exception ex)
            {
                resultado.error = "Ha habido un error";
            }
            resultado.totalElementos = Clasificaciones.Count;
            resultado.dataClasificaciones = Clasificaciones;
            return resultado;
        }
        [HttpPost]
        // POST: api/Clasificacion
        public IHttpActionResult Post([FromBody]Clasificacion clasificacion)
        {
            RespuestaAPI respuesta = new RespuestaAPI();
            respuesta.error = "";
            int filasAfectadas = 0;
            try
            {
                Db.Conectar();

                if (Db.EstaLaConexionAbierta())
                {
                    filasAfectadas = Db.AgregarClasificacion(clasificacion);
                }
                respuesta.totalElementos = filasAfectadas;

                Db.Desconectar();
            }
            catch (Exception ex)
            {
                respuesta.totalElementos = 0;
                respuesta.error = "error al agregar clasificacion";

            }

            return Ok(respuesta);
        }
        [HttpPut]
        // PUT: api/Clasificacion/5
        public IHttpActionResult Put(int id, [FromBody]Clasificacion clasificacion)
        {
            RespuestaAPI respuesta = new RespuestaAPI();
            respuesta.error = "";
            int filasAfectadas = 0;
            try
            {
                Db.Conectar();

                if (Db.EstaLaConexionAbierta())
                {
                    filasAfectadas = Db.ActualizarClasificacion(id, clasificacion);
                }
                respuesta.totalElementos = filasAfectadas;

                Db.Desconectar();
            }
            catch (Exception ex)
            {
                respuesta.totalElementos = 0;
                respuesta.error = "error al Actualizar la Clasificacion";

            }

            return Ok(respuesta);
        }
        [HttpDelete]
        // DELETE: api/Clasificacion/5
        public IHttpActionResult Delete(int id)
        {
            RespuestaAPI respuesta = new RespuestaAPI();
            respuesta.error = "";
            int filasAfectadas = 0;
            try
            {
                Db.Conectar();

                if (Db.EstaLaConexionAbierta())
                {
                    filasAfectadas = Db.EliminarClasificacion(id);
                }
                respuesta.totalElementos = filasAfectadas;

                Db.Desconectar();
            }
            catch (Exception ex)
            {
                respuesta.totalElementos = 0;
                respuesta.error = "error al borrar Clasificacion";

            }

            return Ok(respuesta);
        }
    }
}
