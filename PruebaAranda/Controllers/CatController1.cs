using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using BL;
using Entidad;

namespace PruebaAranda.Controllers
{
    public class CatController1 : ApiController
    {

        readonly CategoriaBL CategoriaNegocio;

        public CatController1()
        {
            CategoriaNegocio = new CategoriaBL();

        }

        // GET api/<controller>
        public IEnumerable<CategoriaDto> Get()
        {
            List<CategoriaDto> lstCategoriaDto = CategoriaNegocio.ObtenerCategorias();
            return lstCategoriaDto;
            //return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        //public void Post([FromBody] string value)
        //{
        //}

        public void Post(CategoriaDto categoria)
        {
            CategoriaNegocio.CrearCategoria(categoria);
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}