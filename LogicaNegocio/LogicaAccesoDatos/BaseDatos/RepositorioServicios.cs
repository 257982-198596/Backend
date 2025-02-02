using Excepciones;
using LogicaNegocio.Dominio;
using LogicaNegocio.InterfacesRepositorios;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
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

        private readonly ILogger<RepositorioServicios> logAzure;

        public RepositorioServicios(CobrosContext context, ILogger<RepositorioServicios> logger)
        {
            Contexto = context;
            logAzure = logger;
        }

        public void Add(Servicio obj)
        {
            try
            {
                

                Categoria laCategoria = Contexto.Categorias.Find(obj.CategoriaId);
                
                if (laCategoria != null)
                {
                                         
                    obj.CategoriaDelServicio = laCategoria;
                    obj.Validar();
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
                logAzure.LogError(ex.Message);
                throw;
            }
            catch (Exception e)
            {
                logAzure.LogError(e.Message);
                throw;
            }
        }

        public IEnumerable<Servicio> FindAll()
        {
            try
            {
                List<Servicio> losServicios = Contexto.Servicios.Include(serv => serv.CategoriaDelServicio).ToList();
                if (losServicios != null)
                {
                    return losServicios;
                }
                else
                {
                    throw new ServicioException("No hay servicios ingresados en el sistema");
                }

            }
            catch (ServicioException ex)
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

        public Servicio FindById(int id)
        {
            try
            {
                return Contexto.Servicios.Where(ser => ser.Id == id).SingleOrDefault();
            }
            catch (ServicioException ex)
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

        public void Remove(int id)
        {
            Servicio elServicioAEliminar = Contexto.Servicios.Find(id);
            try
            {
                if (elServicioAEliminar != null)
                {
                    // Obtener los ServiciosDelCliente asociados al servicio
                    List<ServicioDelCliente> serviciosDelCliente = Contexto.ServiciosDelCliente
                        .Where(sdc => sdc.ServicioContratadoId == id)
                        .ToList();

                    // Obtener las notificaciones asociadas al servicio
                    List<Notificacion> notificaciones = Contexto.Notificaciones
                        .Include(n => n.ServicioNotificado)
                        .Where(n => n.ServicioNotificado.ServicioContratadoId == id)
                        .ToList();

                    // Verificar si hay servicios del cliente asociados
                    if (serviciosDelCliente.Count > 0)
                    {
                        throw new ServicioException("No se puede eliminar el Servicio porque existen clientes con este servicio contratado.");
                    }

                    // Verificar si hay notificaciones asociadas
                    if (notificaciones.Count > 0)
                    {
                        throw new ServicioException("No se puede eliminar el servicio porque tiene notificaciones asociadas.");
                    }

                    Contexto.Remove(elServicioAEliminar);
                    Contexto.SaveChanges();
                }
                else
                {
                    throw new ServicioException("No se pudo dar la baja, el servicio no existe en el sistema");
                }

            }
            catch (ServicioException ex)
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

        public void Update(Servicio obj)
        {
            try
            {

                Categoria laCategoriaDelServicio = Contexto.Categorias.Find(obj.CategoriaId);
                if(laCategoriaDelServicio != null)
                {
                    //Valida Servicio
                    obj.CategoriaDelServicio = laCategoriaDelServicio;
                    obj.Validar();
                    Contexto.Servicios.Update(obj);
                    Contexto.SaveChanges();
                }
                else
                {
                    throw new ServicioException("La categoría seleccionada no existe en el sistema");
                }
                
            }
            catch (ServicioException ex)
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

        public IEnumerable<Servicio> FindAllBySuscriptorId(int suscriptorId)
        {
            try
            {
                List<Servicio> losServicios = Contexto.Servicios
                    .Include(serv => serv.CategoriaDelServicio)
                    .Where(serv => serv.SuscriptorId == suscriptorId)
                    .ToList();

                if (losServicios != null)
                {
                    return losServicios;
                }
                else
                {
                    throw new ServicioException("No hay servicios ingresados en el sistema para el suscriptor especificado");
                }
            }
            catch (ServicioException ex)
            {
                logAzure.LogError(ex.Message);
                throw;
            }
            catch (Exception e)
            {
                logAzure.LogError(e.Message);
                throw new ServicioException("Error al obtener los servicios del suscriptor", e);
            }
        }
    }
}
