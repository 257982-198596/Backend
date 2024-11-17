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
    public class RepositorioDocumentos : IRepositorioDocumentos
    {
        public CobrosContext Contexto { get; set; }

        public RepositorioDocumentos(CobrosContext context)
        {
            Contexto = context;
        }

        public IEnumerable<Documento> FindAll()
        {
            try
            {
                List<Documento> losTiposDeDocumentos = Contexto.Documentos.ToList();
                if (losTiposDeDocumentos != null)
                {
                    return losTiposDeDocumentos;
                }
                else
                {
                    throw new ServicioException("No hay tipos de documentos ingresados en el sistema");
                }

            }
            catch (ServicioException ex)
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
