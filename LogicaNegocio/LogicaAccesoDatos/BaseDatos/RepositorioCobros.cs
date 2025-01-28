using Excepciones;
using LogicaNegocio.Dominio;
using LogicaNegocio.InterfacesDominio;
using LogicaNegocio.InterfacesRepositorios;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaDeNotificaciones;
using SendGrid.Helpers.Mail.Model;
using Microsoft.Extensions.Logging;

namespace LogicaAccesoDatos.BaseDatos
{
    public class RepositorioCobros : Observable<RepositorioCobros>, IRepositorioCobros
    {

        private readonly EnviarCorreo SistemaEnviarCorreo;
        public CobrosContext Contexto { get; set; }

        private readonly ILogger<RepositorioCobros> logAzure;

        public RepositorioCobros(CobrosContext context, EnviarCorreo enviarCorreo, ILogger<RepositorioCobros> logger)
        {
            Contexto = context;
            SistemaEnviarCorreo = enviarCorreo;
            logAzure = logger;
        }

        

        public void Add(CobroRecibido obj)
        {
            try
            {
                obj.Validar();

                Moneda laMoneda = Contexto.Monedas.Find(obj.MonedaDelCobroId);
                MedioDePago elMedio = Contexto.MediosDePago.Find(obj.MedioPagoId);
                ServicioDelCliente elServicioDelCliente = Contexto.ServiciosDelCliente
                    .Include(serCli => serCli.ServicioContratado)
                    .FirstOrDefault(serCli => serCli.Id == obj.ServicioDelClienteId);

                
                Cliente elCliente = Contexto.Clientes
                .Include(cli => cli.UsuarioLogin)
                .FirstOrDefault(cli => cli.Id == elServicioDelCliente.ClienteId);
                //Contexto.Entry(elCliente).State = EntityState.Detached;
                //Contexto.Entry(elServicioDelCliente).State = EntityState.Detached;
                if (laMoneda != null )
                {
                    if (elMedio != null) {
                        if(elServicioDelCliente != null)
                        {
                            obj.MonedaDelCobro = laMoneda;
                            obj.MedioPago = elMedio;

                            Contexto.Add(obj);
                            Contexto.SaveChanges();

                            //aviso a los observadores - ServicioDelCliente (se tiene que renovar), Cliente agregar el cobro
                            NotificarObservadores(this, "AltaCobro");
                            //Envio de correo
                            SistemaEnviarCorreo.EnviarRenovacionServicio(obj, elCliente);
                            Notificacion laNotificacion = new Notificacion(DateTime.Now, $"RENOVACIÓN DE SERVICIO: {obj.ServicioDelCliente.Descripcion}");
                            laNotificacion.ClienteNotificado = elCliente;
                            laNotificacion.ServicioNotificado = elServicioDelCliente;
                            // Recuperar estados de la notificación
                            laNotificacion.EstadoDeNotificacion = Contexto.EstadosDeNotificacion.FirstOrDefault(e => e.Nombre == "Enviada");
                            //EstadoNotificacion estadoFallido = Contexto.EstadosDeNotificacion.FirstOrDefault(e => e.Nombre == "Fallida");
                            //NotificarObservadores(this, "AltaNotificacion");


                            Contexto.Notificaciones.Add(laNotificacion);
                            Contexto.SaveChanges();
                        }
                        else
                        {
                            throw new CobroRecibidoException("Debe seleccionar un Servicio a Pagar");
                        }


                    }
                    else
                    {
                        throw new CobroRecibidoException("Debe seleccionar un Medio de Pago ");
                    }


                }
                else
                {
                    throw new CobroRecibidoException("Debe seleccionar una Moneda");
                }
            }
            catch (CobroRecibidoException ex)
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

        public IEnumerable<CobroRecibido> FindAll()
        {

            try
            {
                List<CobroRecibido> losCobrosRecibidos = Contexto.CobrosRecibidos
                    .Include(co => co.MedioPago)
                    .Include(co => co.MonedaDelCobro)
                    .Include(co => co.ServicioDelCliente)
                        .ThenInclude(serCli => serCli.Cliente)
                    .Include(co => co.ServicioDelCliente)
                        .ThenInclude(serCli => serCli.ServicioContratado) 
                    .ToList();

                if (losCobrosRecibidos != null)
                {
                    return losCobrosRecibidos;
                }
                else
                {
                    throw new CobroRecibidoException("No hay Cobros Recibidos en el sistema");
                }

            }
            catch (CobroRecibidoException ex)
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

        public IEnumerable<CobroRecibido> FindBySuscriptorId(int suscriptorId)
        {
            try
            {
                List<CobroRecibido> cobros = Contexto.CobrosRecibidos
                    .Include(co => co.MedioPago)
                    .Include(co => co.MonedaDelCobro)
                    .Include(co => co.ServicioDelCliente)
                        .ThenInclude(serCli => serCli.ServicioContratado)
                    .Include(c => c.ServicioDelCliente)
                    .ThenInclude(sc => sc.Cliente)
                    .Where(c => c.ServicioDelCliente.Cliente.SuscriptorId == suscriptorId)
                    .ToList();
                return cobros;
            }
            catch (CobroRecibidoException ex)
            {
                logAzure.LogError(ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                logAzure.LogError(ex.Message);
                throw new CobroRecibidoException("Error al obtener los cobros del suscriptor", ex);
            }
        }

        public CobroRecibido FindById(int id)
        {
            try
            {
                return Contexto.CobrosRecibidos.Where(co => co.Id == id).SingleOrDefault();
            }
            catch (CobroRecibidoException ex)
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

        public void Remove(int id)
        {
            CobroRecibido elCobroAEliminar = Contexto.CobrosRecibidos.Find(id);
            try
            {
                if (elCobroAEliminar != null)
                {
                    //TODO:validar registros en otras tablas
                    Contexto.Remove(elCobroAEliminar);
                    Contexto.SaveChanges();
                }
                else
                {
                    throw new CobroRecibidoException("No se pudo dar la baja, el Cobro a Eliminar no existe en el sistema");
                }

            }
            catch (CobroRecibidoException ex)
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

        public void Update(CobroRecibido obj)
        {
            try
            {
                Moneda laMoneda = Contexto.Monedas.Find(obj.MonedaDelCobroId);
                MedioDePago elMedio = Contexto.MediosDePago.Find(obj.MedioPagoId);
                ServicioDelCliente elServicioDelCliente = Contexto.ServiciosDelCliente
                 .Include(serCli => serCli.ServicioContratado)
                 .FirstOrDefault(serCli => serCli.Id == obj.ServicioDelClienteId);

                if (laMoneda != null)
                {
                    if (elMedio !=null) {
                        if (elServicioDelCliente != null)
                        {
                            //Valida Cobro recibido
                            obj.MedioPago = elMedio;
                            obj.MonedaDelCobro = laMoneda;
                            obj.ServicioDelCliente = elServicioDelCliente;

                            obj.Validar();
                            Contexto.CobrosRecibidos.Update(obj);
                            Contexto.SaveChanges();
                        }
                        else
                        {
                            throw new CobroRecibidoException("El servicio seleccionado no existe en el sistema");
                        }

                    }
                    else
                    {
                        throw new CobroRecibidoException("El medio seleccionado no existe en el sistema");
                    }

                }
                else
                {
                    throw new CobroRecibidoException("La Moneda seleccionada no existe en el sistema");
                }

            }
            catch (CobroRecibidoException ex)
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

        public void AgregarObservador(IObservador<IRepositorioServiciosDelCliente> observador)
        {
            AgregarObservador((IObservador<RepositorioCobros>)observador);
        }

        public void AgregarObservador(IRepositorioServiciosDelCliente repositorioServicios)
        {
            AgregarObservador((IObservador<RepositorioCobros>)repositorioServicios);
        }

        public void AgregarObservador(IRepositorioClientes repositorioClientes)
        {
            AgregarObservador((IObservador<RepositorioCobros>)repositorioClientes);
        }


        public Dictionary<int, decimal> SumaCobrosPorMes(int suscriptorId, int year)
        {
            try
            {
                Dictionary<int, decimal> cobrosPorMes = new Dictionary<int, decimal>();
                for (int mes = 1; mes <= 12; mes++)
                {
                    decimal suma = Contexto.CobrosRecibidos
                        .Include(c => c.ServicioDelCliente)
                        .ThenInclude(sc => sc.Cliente)
                        .Where(c => c.ServicioDelCliente.Cliente.SuscriptorId == suscriptorId && c.FechaDePago.Month == mes && c.FechaDePago.Year == year)
                        .Sum(c => c.Monto);
                    cobrosPorMes.Add(mes, suma);
                }
                return cobrosPorMes;
            }
            catch (CobroRecibidoException ex)
            {
                logAzure.LogError(ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                logAzure.LogError(ex.Message);
                throw new CobroRecibidoException("Error al obtener la suma de cobros por mes", ex);
            }
        }

        public Dictionary<int, decimal> SumaCobrosPorMesYServicio(int suscriptorId, int year, int servicioId)
        {
            try
            {
                CotizacionDolar cotizacionDolar = Contexto.Cotizaciones
                .OrderByDescending(c => c.Fecha)
                .FirstOrDefault();

                Dictionary<int, decimal> cobrosPorMes = new Dictionary<int, decimal>();
                for (int mes = 1; mes <= 12; mes++)
                {
                    List<CobroRecibido> cobrosDelMes = Contexto.CobrosRecibidos
                        .Include(c => c.MonedaDelCobro)
                        .Include(c => c.ServicioDelCliente)
                        .ThenInclude(sc => sc.Cliente)
                        .Where(c => c.ServicioDelCliente.Cliente.SuscriptorId == suscriptorId
                                    && c.FechaDePago.Month == mes
                                    && c.FechaDePago.Year == year
                                    && c.ServicioDelCliente.ServicioContratadoId == servicioId)
                        .ToList();
                    decimal sumaDelMes = 0;
                        foreach(var cobro in cobrosDelMes)
                        {
                        decimal monto = cobro.Monto;
                        monto = cobro.MonedaDelCobro.CovertirADolares(cobro.Monto, cotizacionDolar.Valor);
                        sumaDelMes += monto;
                        }
                    
                    cobrosPorMes.Add(mes, sumaDelMes);
                }
                return cobrosPorMes;
            }
            catch (CobroRecibidoException ex)
            {
                logAzure.LogError(ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                logAzure.LogError(ex.Message);
                throw new CobroRecibidoException("Error al obtener la suma de cobros por mes y servicio", ex);
            }
        }

        public Dictionary<int, decimal> SumaCobrosPorMesYCliente(int suscriptorId, int year, int clienteId)
        {
            try
            {
                CotizacionDolar cotizacionDolar = Contexto.Cotizaciones
                .OrderByDescending(c => c.Fecha)
                .FirstOrDefault();

                Dictionary<int, decimal> cobrosPorMes = new Dictionary<int, decimal>();
                for (int mes = 1; mes <= 12; mes++)
                {
                    List<CobroRecibido> cobrosDelMes = Contexto.CobrosRecibidos
                        .Include(c => c.MonedaDelCobro)
                        .Include(c => c.ServicioDelCliente)
                        .ThenInclude(sc => sc.Cliente)
                        .Where(c => c.ServicioDelCliente.Cliente.SuscriptorId == suscriptorId
                                    && c.FechaDePago.Month == mes
                                    && c.FechaDePago.Year == year
                                    && c.ServicioDelCliente.ClienteId == clienteId)
                        .ToList();

                    decimal sumaDelMes = 0;
                    foreach (var cobro in cobrosDelMes)
                    {
                        decimal monto = cobro.Monto;
                        monto = cobro.MonedaDelCobro.CovertirADolares(cobro.Monto, cotizacionDolar.Valor);
                        sumaDelMes += monto;
                    }

                    cobrosPorMes.Add(mes, sumaDelMes);
                }
                return cobrosPorMes;
            }
            catch (CobroRecibidoException ex)
            {
                logAzure.LogError(ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                logAzure.LogError(ex.Message);
                throw new CobroRecibidoException("Error al obtener la suma de cobros por mes y cliente", ex);
            }
        }
    }
}
