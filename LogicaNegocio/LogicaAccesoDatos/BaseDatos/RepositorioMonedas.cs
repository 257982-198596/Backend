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
    public class RepositorioMonedas : IRepositorioMonedas
    {
        public CobrosContext Contexto { get; set; }

        private readonly ILogger<RepositorioMonedas> logAzure;

        public RepositorioMonedas(CobrosContext context, ILogger<RepositorioMonedas> logger)
        {
            Contexto = context;
            logAzure = logger;
        }


        public IEnumerable<Moneda> FindAll()
        {
            try
            {
                List<Moneda> lasMonedas = Contexto.Monedas.ToList();
                if (lasMonedas != null)
                {
                    return lasMonedas;
                }
                else
                {
                    throw new MonedaException("No hay monedas ingresados en el sistema");
                }

            }
            catch (MonedaException ex)
            {
                logAzure.LogError(ex.Message);
                throw;
            }
            catch (Exception e)
            {
                logAzure.LogError(e.Message);
                throw;
            }
        }
    }

}
