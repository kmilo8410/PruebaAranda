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
    public class ProductoController : ApiController
    {
        readonly ProductoBL ProductoNegocio;
        readonly CategoriaBL CategoriaNegocio;


        public ProductoController()
        {
            ProductoNegocio = new ProductoBL();
            CategoriaNegocio = new CategoriaBL();
        }

        public IEnumerable<ProductoDto> Get()
        {
            List<ProductoDto> lstProducto = ProductoNegocio.ObtenerProductos();
            return lstProducto;
        }

        // GET api/<controller>/5
        public ProductoDto Get(int id)
        {
            ProductoDto producto = ProductoNegocio.ObtenerProductoId(id);
            return producto;
        }

        // POST api/<controller>
        public string Post(ProductoDto producto)
        {
            string mensaje = string.Empty;
            if (ModelState.IsValid)
            {
                CategoriaDto categoria = CategoriaNegocio.ObtenerCategoriaId(producto.IdCategoria);
                if (categoria != null && categoria.Id > 0)
                {
                    if (ProductoNegocio.CrearProducto(producto))
                    {
                        mensaje = "Producto creado satisfactoriamente";
                    }
                    else
                    {
                        mensaje = "Error al crear el producto";
                    }
                }
                else
                {
                    mensaje = "Categoría no existe";
                }
            }
            else
            {
                mensaje = "Faltan datos";
            }
            return mensaje;
        }

        // PUT api/<controller>/5
        public string Put(int id, ProductoDto producto)
        {
            string mensaje;

            if (ModelState.IsValid && id >0)
            {

                ProductoDto productoEncontrado = ProductoNegocio.ObtenerProductoId(id);
                if (productoEncontrado != null && productoEncontrado.Id > 0)
                {




                    if (producto.IdCategoria > 0)
                    {
                        CategoriaDto categoria = CategoriaNegocio.ObtenerCategoriaId(producto.IdCategoria);
                        if (categoria != null && categoria.Id > 0)
                        {
                            productoEncontrado.IdCategoria = producto.IdCategoria;
                        }
                    }                    
                    productoEncontrado.Imagen = producto.Imagen;
                    if (!string.IsNullOrEmpty(producto.Descripcion))
                    {
                        productoEncontrado.Descripcion = producto.Descripcion;
                    }
                    
                    ProductoNegocio.ActualizarProducto(productoEncontrado);
                    mensaje = string.Format("Producto id {0} eliminado satisfactoriamente", id);
                }
                else
                {
                    mensaje = "Producto no Existe";
                }
            }
            else
            {
                mensaje = "Debe ingresar un identificador mayor que 0";
            }
            return mensaje;
        }

        // DELETE api/<controller>/5
        public string Delete(int id)
        {
            string mensaje;
            if (id > 0)
            {
                ProductoDto producto = ProductoNegocio.ObtenerProductoId(id);
                if (producto != null && producto.Id > 0)
                {
                    ProductoNegocio.EliminarProducto(producto);
                    mensaje = string.Format("Producto id {0} eliminado satisfactoriamente", id);
                }
                else
                {
                    mensaje = "Producto no Existe";
                }

            }
            else
            {
                mensaje = "Debe ingresar un identificador mayor que 0";
            }
            return mensaje;

        }
    }
}