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
    public class RepositorioCategorias : IRepositorioCategorias
    {
        public CobrosContext Contexto { get; set; }

        public RepositorioCategorias(CobrosContext context)
        {
            Contexto = context;
        }

        public IEnumerable<Categoria> FindAll()
        {
            try
            {
                List<Categoria> lasCategorias = Contexto.Categorias.ToList();
                if (lasCategorias != null)
                {
                    return lasCategorias;
                }
                else
                {
                    throw new CategoriaException("No hay categorias ingresados en el sistema");
                }

            }
            catch (CategoriaException ex)
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
