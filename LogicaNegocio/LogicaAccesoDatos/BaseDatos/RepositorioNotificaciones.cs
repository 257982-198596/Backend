using Excepciones;
using LogicaNegocio.Dominio;
using LogicaNegocio.InterfacesDominio;
using LogicaNegocio.InterfacesRepositorios;
using Microsoft.EntityFrameworkCore;
using SendGrid.Helpers.Mail;
using SistemaDeNotificaciones;
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

        private readonly EnviarCorreo SistemaEnviarCorreo;

        public RepositorioNotificaciones(CobrosContext context, EnviarCorreo enviarCorreo)
        {
            Contexto = context;
            SistemaEnviarCorreo = enviarCorreo;
        }
 


        public void Add(Notificacion obj)
        {
            ServicioDelCliente elServicioDelCliente = Contexto.ServiciosDelCliente
                .Include(ser => ser.MonedaDelServicio)
                .Include(serCli => serCli.ServicioContratado)
                
                .Include(serCli => serCli.Cliente)
                .ThenInclude(cli => cli.UsuarioLogin)
                .FirstOrDefault(serCli => serCli.Id == obj.ServicioNotificadoId);
            try
            {
                obj.ClienteNotificado = elServicioDelCliente.Cliente;
                obj.ServicioNotificado = elServicioDelCliente;
                obj.EstadoDeNotificacion = Contexto.EstadosDeNotificacion.Find(1);
                obj.FechaEnvio = DateTime.Now;
                obj.Mensaje = $"RECORDATORIO DE VENCIMIENTO: {elServicioDelCliente.Descripcion}";
                SistemaEnviarCorreo.EnviarRecordatorio(obj.ServicioNotificado,obj.ClienteNotificado);
                
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
                    .ThenInclude(serCli => serCli.ServicioContratado)
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

        public IEnumerable<Notificacion> GenerarNotificacionesPendientes()
        {
            try
            {
                DateTime ahora = DateTime.Now;
                DateTime rangoMaximo = ahora.AddDays(30);
                // Filtrar servicios en la base de datos
                List<ServicioDelCliente> servicios = Contexto.ServiciosDelCliente
                    .Include(ser => ser.MonedaDelServicio)
                    .Include(s => s.Cliente)
                    .ThenInclude(c => c.UsuarioLogin)
                    .Include(s => s.FrecuenciaDelServicio)
                    .Where(s => s.FechaVencimiento > ahora && s.FechaVencimiento <= rangoMaximo)
                    .ToList();

                List<Notificacion> notificacionesGeneradas = new List<Notificacion>();

                foreach (ServicioDelCliente servicio in servicios)
                {
                    int diasParaVencimiento = (servicio.FechaVencimiento - DateTime.Now).Days;

                    
                    if (diasParaVencimiento == 30 || diasParaVencimiento == 15 || diasParaVencimiento == 3 || diasParaVencimiento == 1)
                    {
                        Notificacion laNotificacion = new Notificacion
                        {
                            ServicioNotificadoId = servicio.Id,
                            ClienteNotificado = servicio.Cliente,
                            ServicioNotificado = servicio,
                            EstadoDeNotificacion = Contexto.EstadosDeNotificacion.Find(1),
                            FechaEnvio = DateTime.Now,
                            Mensaje = $"RECORDATORIO: El servicio '{servicio.Descripcion}' vence en {diasParaVencimiento} días."
                        };

                        // Enviar notificación
                        SistemaEnviarCorreo.EnviarRecordatorio(laNotificacion.ServicioNotificado, laNotificacion.ClienteNotificado);

                        // Guardar notificación en la base de datos
                        Contexto.Notificaciones.Add(laNotificacion);
                        notificacionesGeneradas.Add(laNotificacion);
                    }
                }

                Contexto.SaveChanges();
                return notificacionesGeneradas;
            }
            catch (Exception ex)
            {
                throw new NotificacionException("Error al generar notificaciones pendientes.", ex);
            }
        }


        public void Actualizar(RepositorioCobros obj, string evento)
        {
            //throw new NotImplementedException();
        }
    }
}
