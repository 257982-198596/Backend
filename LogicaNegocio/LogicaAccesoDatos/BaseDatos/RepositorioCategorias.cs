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

        public void Add(Categoria obj)
        {
            try
            {
                obj.Validar();
                Contexto.Categorias.Add(obj);
                Contexto.SaveChanges();
            }
            catch (Exception ex)
            {
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
            catch (Exception ex)
            {
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
            catch (Exception ex)
            {
                throw new CategoriaException("Error al eliminar la categoría", ex);
            }
        }

        public void Update(Categoria obj)
        {
            try
            {
                obj.Validar();
                Contexto.Categorias.Update(obj);
                Contexto.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new CategoriaException("Error al actualizar la categoría", ex);
            }
        }
    }
}
