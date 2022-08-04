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
    public class ValuesController : ApiController
    {
        // GET api/values


        readonly CategoriaBL CategoriaNegocio;

        public ValuesController()
        {
            CategoriaNegocio = new CategoriaBL();

        }

        public IEnumerable<string> Get()
        {
            List<CategoriaDto> lstCategoriaDto = CategoriaNegocio.ObtenerCategorias();
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
