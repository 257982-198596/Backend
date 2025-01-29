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
    public class RepositorioMediosDePago : IRepositorioMediosDePago
    {
        public CobrosContext Contexto { get; set; }

        private readonly ILogger<RepositorioMediosDePago> logAzure;

        public RepositorioMediosDePago(CobrosContext context, ILogger<RepositorioMediosDePago> logger)
        {
            Contexto = context;
            logAzure = logger;
        }
        public IEnumerable<MedioDePago> FindAll()
        {
            try
            {
                List<MedioDePago> losMediosDePago = Contexto.MediosDePago.ToList();
                if (losMediosDePago != null)
                {
                    return losMediosDePago;
                }
                else
                {
                    throw new MedioDePagoException("No hay medios de pago ingresados en el sistema");
                }

            }
            catch (MedioDePagoException ex)
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
