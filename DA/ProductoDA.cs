using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidad;


namespace DA
{
    public class ProductoDA
    {
        private readonly CatalogoEntities DbContext;
        ErrorDA errorDataAccess;

        public ProductoDA()
        {
            DbContext = new CatalogoEntities();
        }


        public List<ProductoDto> ObtenerProductos()
        {
            List<ProductoDto> lstProductoDto = new List<ProductoDto>();
            try
            {
                List<producto> lstProducto = DbContext.productoes.ToList();
                if (lstProducto.Count > 0)
                {
                    lstProductoDto = RetornarListaProductoDto(lstProducto);
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

            return lstProductoDto;
        }

        public ProductoDto ObtenerProductoId(int id)
        {
            ProductoDto productoDto = new ProductoDto();
            try
            {
                producto productoDato = DbContext.productoes.Find(id);
                if (productoDato != null)
                {
                    productoDto = RetornarProductoDto(productoDato);
                }
                else
                {
                    errorDataAccess = new ErrorDA();
                    error objError = errorDataAccess.RetornarError(string.Format("Error Capa Data Acces al obtener el producto con el identificador = {0}", id), string.Empty);
                    errorDataAccess.Crear(objError);
                }
            }
            catch (Exception ex)
            {
                errorDataAccess = new ErrorDA();
                error objError = errorDataAccess.RetornarError(string.Format("Error Capa Data Acces al obtener el producto con el identificador = {0}", id), ex.Message);
                errorDataAccess.Crear(objError);
            }
            //finally
            //{
            //    DbContext.Dispose();
            //}

            return productoDto;
        }

        ProductoDto RetornarProductoDto(producto objProducto)
        {
            ProductoDto objProductoDto = new ProductoDto();

            try
            {
                objProductoDto.Id = objProducto.Id;
                objProductoDto.IdCategoria = objProducto.IdCategoria;
                objProductoDto.Descripcion = objProducto.Descripcion;
            }
            catch (Exception ex)
            {
                errorDataAccess = new ErrorDA();
                error objError = errorDataAccess.RetornarError("Error en la capa Data Access al mapear el producto", ex.Message);
                errorDataAccess.Crear(objError);
            }

            return objProductoDto;
        }

        List<ProductoDto> RetornarListaProductoDto(List<producto> lstCategoria)
        {
            List<ProductoDto> lstProductoDto = new List<ProductoDto>();

            try
            {
                foreach (producto dato in lstCategoria)
                {
                    ProductoDto objProductoDto = RetornarProductoDto(dato);                    
                    lstProductoDto.Add(objProductoDto);
                }
            }
            catch (Exception ex)
            {
                errorDataAccess = new ErrorDA();
                error objError = errorDataAccess.RetornarError("Error en la capa Data Access al obtener los productos", ex.Message);
                errorDataAccess.Crear(objError);
            }

            return lstProductoDto;
        }

        public bool Crear(producto objProducto)
        {
            bool inserto = true;
            try
            {
                DbContext.productoes.Add(objProducto);
                DbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                errorDataAccess = new ErrorDA();
                error objError = errorDataAccess.RetornarError(string.Format("Error al insertar el producto Capa Data Access Descripcion ={0}  IdCategoria = {1}",  objProducto.Descripcion, objProducto.IdCategoria), ex.Message);
                errorDataAccess.Crear(objError);
                inserto = false;
            }
            finally
            {
                DbContext.Dispose();
            }
            return inserto;
        }

        public bool Actualizar(producto objProducto)
        {
            bool actualizo = true;
            try
            {
                //DbContext.Entry(objProducto).State = EntityState.Modified;
                //DbContext.Entry(objProducto).CurrentValues.SetValues(objProducto);

                //DbContext.Entry(objProducto).CurrentValues.SetValues(objProducto);
                DbContext.Entry(objProducto).State = EntityState.Detached;
                DbContext.Entry(objProducto).State = EntityState.Modified;

                DbContext.SaveChanges();               
            }
            catch (Exception ex)
            {
                errorDataAccess = new ErrorDA();
                error objError = errorDataAccess.RetornarError(string.Format("Error al insertar el producto Capa Data Access Descripcion ={0}  IdCategoria = {1}", objProducto.Descripcion, objProducto.IdCategoria), ex.Message);
                errorDataAccess.Crear(objError);
                actualizo = false;
            }
            finally
            {
                DbContext.Dispose();
            }
            return actualizo;
        }

        public bool Eliminar(producto objProducto)
        {
            bool actualizo = true;
            try
            {                
                DbContext.productoes.Remove(DbContext.productoes.Single(x => x.Id == objProducto.Id));
                DbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                errorDataAccess = new ErrorDA();
                error objError = errorDataAccess.RetornarError(string.Format("Error al insertar el producto Capa Data Access Descripcion ={0}  IdCategoria = {1}", objProducto.Descripcion, objProducto.IdCategoria), ex.Message);
                errorDataAccess.Crear(objError);
                actualizo = false;
            }
            finally
            {
                DbContext.Dispose();
            }
            return actualizo;
        }
    }
}
