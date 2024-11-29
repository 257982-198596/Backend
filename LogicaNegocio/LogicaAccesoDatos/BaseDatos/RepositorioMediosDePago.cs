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
    public class RepositorioMediosDePago : IRepositorioMediosDePago
    {
        public CobrosContext Contexto { get; set; }

        public RepositorioMediosDePago(CobrosContext context)
        {
            Contexto = context;
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
                throw;
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}
