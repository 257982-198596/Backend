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
    public class RepositorioDocumentos : IRepositorioDocumentos
    {
        public CobrosContext Contexto { get; set; }

        private readonly ILogger<RepositorioDocumentos> logAzure;

        public RepositorioDocumentos(CobrosContext context, ILogger<RepositorioDocumentos> logger)
        {
            Contexto = context;
            logAzure = logger;
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
                    throw new DocumentoException("No hay tipos de documentos ingresados en el sistema");
                }

            }
            catch (DocumentoException ex)
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
