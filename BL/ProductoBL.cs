using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DA;
using Entidad;

namespace BL
{
    public class ProductoBL
    {
        private readonly ProductoDA ProductoData;
        ErrorDA errorDataAccess;

        public ProductoBL()
        {
            ProductoData = new ProductoDA();
        }

        public List<ProductoDto> ObtenerProductos()
        {
            List<ProductoDto> lstProductoDto = ProductoData.ObtenerProductos();
            return lstProductoDto;
        }

        public ProductoDto ObtenerProductoId(int id)
        {
            ProductoDto productoDto = ProductoData.ObtenerProductoId(id);
            return productoDto;
        }

        public bool CrearProducto(ProductoDto objProducto)
        {
            bool inserto = false;
            try
            {
                if (objProducto != null)
                {
                    producto productoData = MapearProducto(objProducto);
                    inserto = ProductoData.Crear(productoData);
                }
                else
                {
                    errorDataAccess = new ErrorDA();
                    error objError = errorDataAccess.RetornarError(string.Format("Error al insertar un producto Capa Negocio objeto nulo"),string.Empty);
                    errorDataAccess.Crear(objError);
                }
            }
            catch (Exception ex)
            {
                errorDataAccess = new ErrorDA();
                error objError = errorDataAccess.RetornarError(string.Format("Error al insertar un producto Capa Negocio Descripcion ={0}  IdCategoria = {1}", objProducto.Descripcion, objProducto.IdCategoria), ex.Message);
                errorDataAccess.Crear(objError);
            }
            return inserto;
        }

        producto MapearProducto(ProductoDto objProducto)
        {
            producto productoData = new producto();
            if (objProducto.Id > 0)
            {
                productoData.Id = objProducto.Id;
            }
            productoData.IdCategoria = objProducto.IdCategoria;
            productoData.Descripcion = objProducto.Descripcion;
            return productoData;
        }

        public bool ActualizarProducto(ProductoDto objProducto)
        {
            bool actualizo;
            try
            {
                producto productoData = MapearProducto(objProducto);
                actualizo = ProductoData.Actualizar(productoData);
            }
            catch (Exception ex)
            {
                errorDataAccess = new ErrorDA();
                error objError = errorDataAccess.RetornarError(string.Format("Error al insertar el producto Capa Negocio  Id = {2} Descripcion ={0}  IdCategoria = {1}", objProducto.Descripcion, objProducto.IdCategoria, objProducto.Id), ex.Message);
                errorDataAccess.Crear(objError);
                actualizo = false;
            }          
            return actualizo;
        }

        public bool EliminarProducto(ProductoDto objProducto)
        {
            bool actualizo;
            try
            {
                producto productoData = MapearProducto(objProducto);
                actualizo = ProductoData.Eliminar(productoData);

            }
            catch (Exception ex)
            {
                errorDataAccess = new ErrorDA();
                error objError = errorDataAccess.RetornarError(string.Format("Error al Eliminar el producto Capa Negocio Id = {2} Descripcion ={0}  IdCategoria = {1}", objProducto.Descripcion, objProducto.IdCategoria, objProducto.Id), ex.Message);
                errorDataAccess.Crear(objError);
                actualizo = false;
            }
            
            return actualizo;
        }
    }

}
