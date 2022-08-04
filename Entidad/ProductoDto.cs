using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidad
{
    public class ProductoDto
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public int IdCategoria { get; set; }
        public string Imagen { get; set; }

    }
}
