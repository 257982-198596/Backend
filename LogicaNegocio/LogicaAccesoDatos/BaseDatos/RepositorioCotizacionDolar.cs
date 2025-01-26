using Excepciones;
using LogicaNegocio.Dominio;
using LogicaNegocio.InterfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAccesoDatos.BaseDatos
{
    public class RepositorioCotizacionDolar : IRepositorioCotizacionDolar

    {
        public CobrosContext Contexto { get; set; }

        public RepositorioCotizacionDolar(CobrosContext context)
        {
            Contexto = context;
        }

        public void Add(CotizacionDolar obj)
        {
            try
            {
                
                Contexto.Cotizaciones.Add(obj);
                Contexto.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al agregar la cotizacion", ex);
            }
        }
    }
}
