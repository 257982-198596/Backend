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
    public class RepositorioCategorias : IRepositorioCategorias
    {
        public CobrosContext Contexto { get; set; }

        private readonly ILogger<RepositorioCategorias> logAzure;

        public RepositorioCategorias(CobrosContext context, ILogger<RepositorioCategorias> logger)
        {
            Contexto = context;
            logAzure = logger;
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
                logAzure.LogError(ex.Message);
                throw;
            }
            catch (Exception e)
            {
                logAzure.LogError(e.Message);
                throw;
            }

        }

        public IEnumerable<Categoria> FindBySuscriptorId(int suscriptorId)
        {
            try
            {
                List<Categoria> categorias = Contexto.Categorias
                    .Where(c => c.SuscriptorId == suscriptorId)
                    .ToList();
                return categorias;
            }
            catch (CategoriaException ex)
            {
                logAzure.LogError(ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                logAzure.LogError(ex.Message);
                throw new CategoriaException("Error al obtener las categorías del suscriptor", ex);
            }
        }

        public void Add(Categoria obj)
        {
            try
            {
                obj.ValidarNombre();
                Contexto.Categorias.Add(obj);
                Contexto.SaveChanges();
            }
            catch (CategoriaException ex)
            {
                logAzure.LogError(ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                logAzure.LogError(ex.Message);
                throw new CategoriaException("Error al agregar la categoría", ex);
            }
        }


        public Categoria FindById(int id)
        {
            try
            {
                Categoria laCategoria = Contexto.Categorias.Find(id);
                if (laCategoria != null)
                {
                    return laCategoria;
                }
                else
                {
                    throw new CategoriaException("Categoría no encontrada");
                }
            }
            catch (CategoriaException ex)
            {
                logAzure.LogError(ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                logAzure.LogError(ex.Message);
                throw new CategoriaException("Error al buscar la categoría", ex);
            }
        }


        public void Remove(int id)
        {
            try
            {
                Categoria laCategoria = FindById(id);
                if (laCategoria != null)
                {
                    Contexto.Categorias.Remove(laCategoria);
                    Contexto.SaveChanges();
                }
                else
                {
                    throw new CategoriaException("Categoría no encontrada");
                }
            }
            catch (CategoriaException ex)
            {
                logAzure.LogError(ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                logAzure.LogError(ex.Message);
                throw new CategoriaException("Error al eliminar la categoría", ex);
            }
        }

        public void Update(Categoria obj)
        {
            try
            {
                obj.ValidarNombre();
                Contexto.Categorias.Update(obj);
                Contexto.SaveChanges();
            }
            catch (CategoriaException ex)
            {
                logAzure.LogError(ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                logAzure.LogError(ex.Message);
                throw new CategoriaException("Error al actualizar la categoría", ex);
            }
        }
    }
}
