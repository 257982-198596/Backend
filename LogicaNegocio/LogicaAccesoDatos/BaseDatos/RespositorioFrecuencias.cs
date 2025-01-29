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
    public class RespositorioFrecuencias : IRepositorioFrecuencias
    {
        public CobrosContext Contexto { get; set; }

        private readonly ILogger<RespositorioFrecuencias> logAzure;

        public RespositorioFrecuencias(CobrosContext context, ILogger<RespositorioFrecuencias> logger)
        {
            Contexto = context;
            logAzure = logger;
        }


        public IEnumerable<Frecuencia> FindAll()
        {
            try
            {
                List<Frecuencia> lasFrecuencias = Contexto.Frecuencias.ToList();
                if (lasFrecuencias != null)
                {
                    return lasFrecuencias;
                }
                else
                {
                    throw new FrecuenciaException("No hay frecuencias ingresados en el sistema");
                }

            }
            catch (FrecuenciaException ex)
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
