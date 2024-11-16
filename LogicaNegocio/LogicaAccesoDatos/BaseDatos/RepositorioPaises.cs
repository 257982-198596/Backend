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
    public class RepositorioPaises : IRepositorioPaises
    {
        public CobrosContext Contexto { get; set; }

        public RepositorioPaises(CobrosContext context)
        {
            Contexto = context;
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
                throw;
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}
