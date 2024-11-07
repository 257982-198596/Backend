using Excepciones;
using LogicaNegocio.Dominio;
using LogicaNegocio.InterfacesRepositorios;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAccesoDatos.BaseDatos
{
    public class RepositorioServicios : IRepositorioServicios
    {
        public CobrosContext Contexto { get; set; }

        public RepositorioServicios(CobrosContext context)
        {
            Contexto = context;
        }

        public void Add(Servicio obj)
        {
            try
            {
                obj.Validar();

                Categoria laCategoria = Contexto.Categorias.Find(obj.CategoriaId);
                
                if (laCategoria != null)
                {
                                         
                            obj.CategoriaDelServicio = laCategoria;
                            Contexto.Add(obj);
                            Contexto.SaveChanges();
                     
                }
                else
                {
                    throw new ServicioException("Debe seleccionar una categoría de servicio");
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

        public IEnumerable<Servicio> FindAll()
        {
            return Contexto.Servicios.ToList();
        }

        public Servicio FindById(int id)
        {
            return Contexto.Servicios.Where(ser => ser.Id == id).SingleOrDefault();
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Servicio obj)
        {
            throw new NotImplementedException();
        }
    }
}
