using Excepciones;
using LogicaNegocio.Dominio;
using LogicaNegocio.InterfacesDominio;
using LogicaNegocio.InterfacesRepositorios;
using Microsoft.EntityFrameworkCore;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAccesoDatos.BaseDatos
{
    public class RepositorioNotificaciones : IRepositorioNotificaciones, IObservador<RepositorioCobros>
    {
        public CobrosContext Contexto { get; set; }

        public RepositorioNotificaciones(CobrosContext context)
        {
            Contexto = context;
        }


        public void Add(Notificacion obj)
        {
            try
            {

             Contexto.Add(obj);
             Contexto.SaveChanges();

            }
            catch (NotificacionException ex)
            {
                throw;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public IEnumerable<Notificacion> FindAll()
        {
            try
            {
                List<Notificacion> lasNotificaciones = Contexto.Notificaciones
                    .Include(not => not.EstadoDeNotificacion)
                    .Include(not => not.ClienteNotificado)
                    .Include(not => not.ServicioNotificado)
                    .ToList();
                if (lasNotificaciones != null)
                {
                    return lasNotificaciones;
                }
                else
                {
                    throw new NotificacionException("No hay notificaciones ingresadas en el sistema");
                }

            }
            catch (NotificacionException ex)
            {
                throw;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public Notificacion FindById(int id)
        {
            try
            {
                return Contexto.Notificaciones.Where(noti => noti.Id == id).SingleOrDefault();
            }
            catch (NotificacionException ex)
            {
                throw;
            }
            catch (Exception e)
            {
                throw;
            }

        }

        public void Remove(int id)
        {
            Notificacion laNotificacionAEliminar = Contexto.Notificaciones.Find(id);
            try
            {
                if (laNotificacionAEliminar != null)
                {
                    //TODO:validar registros en otras tablas
                    Contexto.Remove(laNotificacionAEliminar);
                    Contexto.SaveChanges();
                }
                else
                {
                    throw new NotificacionException("No se pudo dar la baja, la notificacion no existe en el sistema");
                }

            }
            catch (NotificacionException ex)
            {
                throw;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public void Update(Notificacion obj)
        {
            try
            {
             Contexto.Notificaciones.Update(obj);
             Contexto.SaveChanges();

            }
            catch (NotificacionException ce)
            {
                throw;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public void Actualizar(RepositorioCobros obj, string evento)
        {
            //throw new NotImplementedException();
        }
    }
}
