using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DA;
using Entidad;


namespace BL
{
    public class CategoriaBL
    {
        readonly CategoriaDA CategoriaData;
        ErrorDA errorDataAccess;

        public CategoriaBL()
        {
            CategoriaData = new CategoriaDA();
        }

        public List<CategoriaDto> ObtenerCategorias()
        {
            List<CategoriaDto> lstCategoriaDto = CategoriaData.ObtenerCategorias();
            return lstCategoriaDto;
        }

        public CategoriaDto ObtenerCategoriaId(int id)
        {
            CategoriaDto categoriaDto = CategoriaData.ObtenerCategoriaId(id);
            return categoriaDto;
        }

        public bool CrearCategoria(CategoriaDto objCategoria)
        {
            bool inserto = true;
            try
            {
                categoria categoriaData = new categoria();
                categoriaData.Nombre = objCategoria.Nombre;
                categoriaData.Descripcion = objCategoria.Descripcion;
                CategoriaData.Crear(categoriaData);
            }
            catch (Exception ex)
            {
                errorDataAccess = new ErrorDA();
                error objError = errorDataAccess.RetornarError(string.Format("Error al insertar la categoria Capa Negocio Nombre ={0}  Descripcion = {1}", objCategoria.Nombre, objCategoria.Descripcion), ex.Message);
                errorDataAccess.Crear(objError);
                inserto = false;
            }
            return inserto;
        }
    }
}
