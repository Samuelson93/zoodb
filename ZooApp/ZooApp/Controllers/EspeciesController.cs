using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ZooApp.Controllers
{
    public class EspeciesController : ApiController
    {
        // GET: api/Especies
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        // GET: api/Especies/
        public RespuestaAPI Get()
        {
            RespuestaAPI resultado = new RespuestaAPI();
            List<Especie> data = new List<Especie>();
            try
            {
                Db.Conectar();
                if (Db.EstaLaConexionAbierta())
                {
                    data = Db.DameListaEspecies();
                    resultado.error = "";
                }
                Db.Desconectar();
            }
            catch 
            {
                resultado.error = "Ha habido un error";
            }
            resultado.totalElementos = data.Count;
            resultado.data = data;
            return resultado;
            
        }
        // GET: api/Especies/5

        public RespuestaAPI Get(long id)
        {
            RespuestaAPI resultado = new RespuestaAPI();
            List<Especie> data = new List<Especie>();
            try
            {
                Db.Conectar();
                if (Db.EstaLaConexionAbierta())
                {
                    data = Db.GetEspeciesId(id);
                    resultado.error = "";
                }
                Db.Desconectar();
            }
            catch (Exception ex)
            {
                resultado.error = "Error";
            }
            resultado.totalElementos = data.Count;
            resultado.data = data;
            return resultado;

        }
        // POST: api/Especies
        [HttpPost]
        
        public IHttpActionResult Post([FromBody]Especie especie)
        {
            RespuestaAPI respuesta = new RespuestaAPI();
            respuesta.error = "";
            int filasAfectadas = 0;
            try
            {
                Db.Conectar();

                if (Db.EstaLaConexionAbierta())
                {
                    filasAfectadas = Db.AgregarEspecie(especie);
                }
                respuesta.totalElementos = filasAfectadas;

                Db.Desconectar();
            }
            catch (Exception ex)
            {
                respuesta.totalElementos = 0;
                respuesta.error = "error al agregar especie";

            }

            return Ok(respuesta);
        }

        [HttpPut]
        // PUT: api/Especies/5
        public IHttpActionResult Put(int id,[FromBody]Especie especie)
        {
            RespuestaAPI respuesta = new RespuestaAPI();
            respuesta.error = "";
            int filasAfectadas = 0;
            try
            {
                Db.Conectar();

                if (Db.EstaLaConexionAbierta())
                {
                    filasAfectadas = Db.ActualizarEspecie(id,especie);
                }
                respuesta.totalElementos = filasAfectadas;

                Db.Desconectar();
            }
            catch (Exception ex)
            {
                respuesta.totalElementos = 0;
                respuesta.error = "error al eliminar especie";

            }

            return Ok(respuesta);
        }
        [HttpDelete]
        // DELETE: api/Especies/5
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
                    filasAfectadas = Db.EliminarEspecie(id);
                }
                respuesta.totalElementos = filasAfectadas;

                Db.Desconectar();
            }
            catch (Exception ex)
            {
                respuesta.totalElementos = 0;
                respuesta.error = "error al borrar especie";

            }

            return Ok(respuesta);
        }
    }
}
