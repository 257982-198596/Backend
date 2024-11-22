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
    public class RepositorioMonedas : IRepositorioMonedas
    {
        public CobrosContext Contexto { get; set; }

        public RepositorioMonedas(CobrosContext context)
        {
            Contexto = context;
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
                throw;
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }

}
