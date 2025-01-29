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
    public class RepositorioPaises : IRepositorioPaises
    {
        public CobrosContext Contexto { get; set; }

        private readonly ILogger<RepositorioPaises> logAzure;

        public RepositorioPaises(CobrosContext context, ILogger<RepositorioPaises> logger)
        {
            Contexto = context;
            logAzure = logger;
        }


        public IEnumerable<Pais> FindAll()
        {
            try
            {
                List<Pais> losPaises = Contexto.Paises.ToList();
                if (losPaises != null)
                {
                    return losPaises;
                }
                else
                {
                    throw new PaisException("No hay paises ingresados en el sistema");
                }

            }
            catch (PaisException ex)
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
