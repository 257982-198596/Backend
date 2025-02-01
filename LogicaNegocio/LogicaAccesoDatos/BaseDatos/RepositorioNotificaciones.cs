using Excepciones;
using LogicaNegocio.Dominio;
using LogicaNegocio.InterfacesDominio;
using LogicaNegocio.InterfacesRepositorios;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
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

        private readonly ILogger<RepositorioNotificaciones> logAzure;

        public RepositorioNotificaciones(CobrosContext context, EnviarCorreo enviarCorreo, ILogger<RepositorioNotificaciones> logger)
        {
            Contexto = context;
            SistemaEnviarCorreo = enviarCorreo;
            logAzure = logger;
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
                logAzure.LogError(ex.Message);
                throw;
            }
            catch (Exception e)
            {
                logAzure.LogError(e.Message);
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
                logAzure.LogError(ex.Message);
                throw;
            }
            catch (Exception e)
            {
                logAzure.LogError(e.Message);
                throw;
            }
        }

        public IEnumerable<Notificacion> FindBySuscriptorId(int suscriptorId)
        {
            try
            {
                List<Notificacion> notificacionesDelSuscriptor = Contexto.Notificaciones
                    .Include(not => not.EstadoDeNotificacion)
                    .Include(not => not.ServicioNotificado)
                    .ThenInclude(serCli => serCli.ServicioContratado)
                    .Include(n => n.ClienteNotificado)
                    .Where(n => n.ClienteNotificado.SuscriptorId == suscriptorId)
                    .ToList();
                return notificacionesDelSuscriptor;
            }
            catch (NotificacionException ex)
            {
                logAzure.LogError(ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                logAzure.LogError(ex.Message);
                throw new NotificacionException("Error al obtener las notificaciones del suscriptor", ex);
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
                logAzure.LogError(ex.Message);
                throw;
            }
            catch (Exception e)
            {
                logAzure.LogError(e.Message);
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
            catch (NotificacionException ex)
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

        public IEnumerable<Notificacion> GenerarNotificacionesPendientes()
        {
            try
            {
                DateTime hoy = DateTime.Today;
                DateTime rangoMaximo = hoy.AddDays(31);
                // Filtrar servicios en la base de datos
                List<ServicioDelCliente> servicios = Contexto.ServiciosDelCliente
                    .Include(ser => ser.MonedaDelServicio)
                    .Include(s => s.Cliente)
                    .ThenInclude(c => c.UsuarioLogin)
                    .Include(s => s.FrecuenciaDelServicio)
                    .Where(s => s.FechaVencimiento > hoy && s.FechaVencimiento <= rangoMaximo)
                    .ToList();

                List<Notificacion> notificacionesGeneradas = new List<Notificacion>();

                foreach (ServicioDelCliente servicio in servicios)
                {
                    int diasParaVencimiento = (servicio.FechaVencimiento - hoy).Days;

                    
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
            catch (NotificacionException ex)
            {
                logAzure.LogError(ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                logAzure.LogError(ex.Message);
                throw new NotificacionException("Error al generar notificaciones pendientes.", ex);
            }
        }


        public void Actualizar(RepositorioCobros obj, string evento)
        {
            //throw new NotImplementedException();
        }

        public int ContarNotificacionesEnviadas(int clienteId, DateTime desdeFecha)
        {
            try
            {
                return Contexto.Notificaciones
                .Count(n => n.ClienteNotificadoId == clienteId && n.FechaEnvio >= desdeFecha);
            }
            catch (NotificacionException ex)
            {
                logAzure.LogError(ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                logAzure.LogError(ex.Message);
                throw;
            }

        }

        public Dictionary<int, decimal> CantidadNotificacionesPorMesaDelSuscriptorId(int suscriptorId, int year)
        {
            try
            {
                var notificacionesPorMes = Contexto.Notificaciones
                    .Include(n => n.ClienteNotificado)
                    .Where(n => n.ClienteNotificado.SuscriptorId == suscriptorId && n.FechaEnvio.Year == year)
                    .GroupBy(n => n.FechaEnvio.Month)
                    .Select(g => new { Mes = g.Key, Cantidad = g.Count() })
                    .ToList();

                Dictionary<int, decimal> resultadoMeses = new Dictionary<int, decimal>();
                for (int i = 1; i <= 12; i++)
                {
                    resultadoMeses[i] = 0m;
                }

                foreach (var item in notificacionesPorMes)
                {
                    resultadoMeses[item.Mes] = (item.Cantidad * 30m) / 60m;
                }

                return resultadoMeses;
            }
            catch (NotificacionException ex)
            {
                logAzure.LogError(ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                logAzure.LogError(ex.Message);
                throw new NotificacionException("Error al obtener las notificaciones del suscriptor por mes", ex);
            }
        }
    }
}
