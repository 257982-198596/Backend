using Excepciones;
using LogicaNegocio.Dominio;
using LogicaNegocio.InterfacesRepositorios;
using Microsoft.Extensions.Logging;
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

        private readonly ILogger<RepositorioCotizacionDolar> logAzure;

        public RepositorioCotizacionDolar(CobrosContext context, ILogger<RepositorioCotizacionDolar> logger)
        {
            Contexto = context;
            logAzure = logger;
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
                logAzure.LogError(ex.Message);
                throw new Exception("Error al agregar la cotizacion", ex);
            }
        }
    }
}
