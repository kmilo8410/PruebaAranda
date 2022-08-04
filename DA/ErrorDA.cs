using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DA
{
    public class ErrorDA
    {
        private readonly CatalogoEntities DbContext;


        public ErrorDA()
        {
            DbContext = new CatalogoEntities();
        }
        public bool Crear(error objError)
        {
            bool inserto = true;
            try
            {
                DbContext.errors.Add(objError);
                DbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                inserto = false;
            }
            finally
            {
                DbContext.Dispose();
            }
            return inserto;
        }

        public error RetornarError(string descripcion, string excepcion)
        {
            error objError = new error();
            objError.Descripcion = descripcion;
            objError.MensajeError = excepcion;
            objError.Fecha = DateTime.Now; 
            objError.Hora = DateTime.Now.TimeOfDay; 
            

            return objError;
        }
    }
}
