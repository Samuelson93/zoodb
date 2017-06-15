using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ZooApp.Controllers
{
    public class TipoAnimalController : ApiController
    {
        // GET: api/TipoAnimal
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        
        public RespuestaAPI Get()
        {
            RespuestaAPI resultado = new RespuestaAPI();
            List<TipoAnimal> listaTiposAnimales = new List<TipoAnimal>();
            try
            {
                Db.Conectar();
                if (Db.EstaLaConexionAbierta())
                {
                    listaTiposAnimales = Db.GetAnimalesTipo();
                    resultado.error = "";
                }
                Db.Desconectar();
            }
            catch (Exception ex)
            {
                resultado.error = "Ha habido un error";
            }
            resultado.totalElementos = listaTiposAnimales.Count;
            resultado.dataTiposAnimal = listaTiposAnimales ;
            return resultado;
        }

        // GET: api/TipoAnimal/5

        public RespuestaAPI Get(int id)
        {
            RespuestaAPI resultado = new RespuestaAPI();
            List<TipoAnimal> listaTiposAnimales = new List<TipoAnimal>();
            try
            {
                Db.Conectar();
                if (Db.EstaLaConexionAbierta())
                {
                    listaTiposAnimales = Db.GetTipoAnimalesId(id);
                    resultado.error = "";
                }
                Db.Desconectar();
            }
            catch (Exception ex)
            {
                resultado.error = "Ha habido un error";
            }
            resultado.totalElementos = listaTiposAnimales.Count;
            resultado.dataTiposAnimal = listaTiposAnimales;
            return resultado;
        }


        // POST: api/TipoAnimal
        [HttpPost]
        public IHttpActionResult Post([FromBody]TipoAnimal tipoAnimal)
        {
            RespuestaAPI respuesta = new RespuestaAPI();
            respuesta.error = "";
            int filasAfectadas = 0;
            try
            {
                Db.Conectar();

                if (Db.EstaLaConexionAbierta())
                {
                    filasAfectadas = Db.AgregarTipoAnimal(tipoAnimal);
                }
                respuesta.totalElementos = filasAfectadas;

                Db.Desconectar();
            }
            catch (Exception ex)
            {
                respuesta.totalElementos = 0;
                respuesta.error = "error al agregar Tipo Animal";

            }

            return Ok(respuesta);
        }

        // PUT: api/TipoAnimal/5
        [HttpPut]
        public IHttpActionResult Put(int id, [FromBody]TipoAnimal tipoAnimal)
        {
            RespuestaAPI respuesta = new RespuestaAPI();
            respuesta.error = "";
            int filasAfectadas = 0;
            try
            {
                Db.Conectar();

                if (Db.EstaLaConexionAbierta())
                {
                    filasAfectadas = Db.ActualizarTipoAnimal(id, tipoAnimal);
                }
                respuesta.totalElementos = filasAfectadas;

                Db.Desconectar();
            }
            catch (Exception ex)
            {
                respuesta.totalElementos = 0;
                respuesta.error = "error al Actualizar el tipo de animal";

            }

            return Ok(respuesta);
        }

        // DELETE: api/TipoAnimal/5
        [HttpDelete]
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
                    filasAfectadas = Db.EliminarTipoAnimal(id);
                }
                respuesta.totalElementos = filasAfectadas;

                Db.Desconectar();
            }
            catch (Exception ex)
            {
                respuesta.totalElementos = 0;
                respuesta.error = "error al borrar el tipo de animal";

            }

            return Ok(respuesta);
        }
    }
}
