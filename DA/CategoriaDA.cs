using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidad;

namespace DA
{
    public class CategoriaDA
    {
        private readonly CatalogoEntities DbContext;
        ErrorDA errorDataAccess;


        public CategoriaDA()
        {
            DbContext = new CatalogoEntities();
        }

        
        public List<CategoriaDto> ObtenerCategorias()
        {
            List<CategoriaDto> lstCategoriaDto = new List<CategoriaDto>();
            try
            {
                List<categoria> lstCategoria = DbContext.categorias.ToList();
                if (lstCategoria.Count > 0)
                {
                    lstCategoriaDto = RetornarListCategoriaDto(lstCategoria);
                }
            }
            catch (Exception ex)
            {
                errorDataAccess = new ErrorDA();
                error objError = errorDataAccess.RetornarError("Error al obtener las categorias", ex.Message);
                errorDataAccess.Crear(objError);
            }
            finally
            {
                DbContext.Dispose();
            }

            return lstCategoriaDto;
        }

        public CategoriaDto ObtenerCategoriaId(int id)
        {
            CategoriaDto ObjCategoriaDto = new CategoriaDto();
            try
            {
                categoria categoriaDato = DbContext.categorias.Find(id);
                if (categoriaDato != null)
                {
                    ObjCategoriaDto = RetornarCategoriaDto(categoriaDato);
                }
                else
                {
                    errorDataAccess = new ErrorDA();
                    error objError = errorDataAccess.RetornarError(string.Format("Error Capa Data Acces al obtener la categoria con el identificador = {0}", id), string.Empty);
                    errorDataAccess.Crear(objError);
                }
            }
            catch (Exception ex)
            {
                errorDataAccess = new ErrorDA();
                error objError = errorDataAccess.RetornarError(string.Format("Error Capa Data Acces al obtener la categoria con el identificador = {0}", id), ex.Message);
                errorDataAccess.Crear(objError);
            }
            finally
            {
                DbContext.Dispose();
            }

            return ObjCategoriaDto;
        }

        CategoriaDto RetornarCategoriaDto(categoria dato)
        {
            CategoriaDto objCategoriaDto = new CategoriaDto();

            try
            {
                objCategoriaDto.Id = dato.Id;
                objCategoriaDto.Nombre = dato.Nombre;
                objCategoriaDto.Descripcion = dato.Descripcion;
               
            }
            catch (Exception ex)
            {
                errorDataAccess = new ErrorDA();
                error objError = errorDataAccess.RetornarError("Error capadata access RetornarCategoriaDto al obtener  categorias", ex.Message);
                errorDataAccess.Crear(objError);
            }
            
            return objCategoriaDto;
        }

        List<CategoriaDto> RetornarListCategoriaDto(List<categoria> lstCategoria)
        {
            List<CategoriaDto> lstCategoriaDto = new List<CategoriaDto>();

            try
            {
                foreach (categoria dato in lstCategoria)
                {
                    CategoriaDto objCategoriaDto = new CategoriaDto();
                    objCategoriaDto.Id = dato.Id;
                    objCategoriaDto.Nombre = dato.Nombre;
                    objCategoriaDto.Descripcion = dato.Descripcion;
                    lstCategoriaDto.Add(objCategoriaDto);
                }
            }
            catch (Exception ex)
            {
                errorDataAccess = new ErrorDA();
                error objError = errorDataAccess.RetornarError("Error al obtener las categorias", ex.Message);
                errorDataAccess.Crear(objError);
            }

            return lstCategoriaDto;
        }

        public bool Crear(categoria objCategoria)
        {
            bool inserto = true;
            try
            {
                DbContext.categorias.Add(objCategoria);
                DbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                errorDataAccess = new ErrorDA();
                error objError = errorDataAccess.RetornarError(string.Format("Error al insertar la categoria Capa Data Access Nombre ={0}  Descripcion = {1}", objCategoria.Nombre, objCategoria.Descripcion), ex.Message);
                errorDataAccess.Crear(objError);
                inserto = false;
            }
            finally
            {
                DbContext.Dispose();
            }
            return inserto;
        }

        //try
        //    {
        //    }
        //    catch (Exception ex)
        //    { 
        // }


    }
}
