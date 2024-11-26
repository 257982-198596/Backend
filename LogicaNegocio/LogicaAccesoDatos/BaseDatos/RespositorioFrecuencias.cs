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
    public class RespositorioFrecuencias : IRepositorioFrecuencias
    {
        public CobrosContext Contexto { get; set; }

        public RespositorioFrecuencias(CobrosContext context)
        {
            Contexto = context;
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
                throw;
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}
