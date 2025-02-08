using Excepciones;
using LogicaNegocio.Dominio;
using LogicaNegocio.InterfacesDominio;
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
    public class RepositorioServiciosDelCliente : IObservador<RepositorioCobros> ,IRepositorioServiciosDelCliente
    {
        public CobrosContext Contexto { get; set; }

        private readonly ILogger<RepositorioServiciosDelCliente> logAzure;

        public RepositorioServiciosDelCliente(CobrosContext context, ILogger<RepositorioServiciosDelCliente> logger)
        {
            Contexto = context;
            logAzure = logger;
        }
        public void Add(ServicioDelCliente obj)
        {
            obj.Validar();
            try
            {
                Servicio elServicio = Contexto.Servicios.Find(obj.ServicioContratadoId);
                Cliente elCliente = Contexto.Clientes.Find(obj.ClienteId);
                Frecuencia laFrecuencia = Contexto.Frecuencias.Find(obj.FrecuenciaDelServicioId);
                Moneda laMoneda = Contexto.Monedas.Find(obj.MonedaDelServicioId);
                EstadoServicioDelCliente elEstadoInicial = Contexto.EstadosServiciosDelClientes.FirstOrDefault(e => e.Nombre == "Activo");

                if (elServicio != null)
                {
                    if (elCliente != null)
                    {
                        Usuario elUsuario = elCliente.UsuarioLogin;
                        if (laFrecuencia != null)
                        {
                            if (laMoneda != null)
                            {
                                if(elEstadoInicial != null)
                                {
                                    obj.ServicioContratado = elServicio;
                                    obj.Cliente = elCliente;
                                    obj.Cliente.UsuarioLogin = elUsuario;
                                    obj.FrecuenciaDelServicio = laFrecuencia;
                                    obj.MonedaDelServicio = laMoneda;
                                    obj.EstadoDelServicioDelCliente = elEstadoInicial;
                                    laFrecuencia.ValidarFechaMaxima(obj.FechaInicio);
                                    laFrecuencia.CalcularVencimiento(obj);

                                    Contexto.Add(obj);
                                    Contexto.SaveChanges();
                                }
                                else
                                {
                                    throw new ServicioDelClienteException("Error al obtener el estado del servicio inicial");
                                }
                                
                            }
                            else
                            {
                                throw new ServicioDelClienteException("Debe seleccionar una moneda");
                            }
                        }
                        else
                        {
                            throw new ServicioDelClienteException("Debe seleccionar una frecuencia de servicio");
                        }
                    }
                    else
                    {
                        throw new ServicioDelClienteException("Debe seleccionar una frecuencia de servicio");
                    }
                }
                else
                {
                    throw new ServicioDelClienteException("Debe seleccionar un servicio");
                }
                
            }
            catch (ServicioDelClienteException ex)
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

        public IEnumerable<ServicioDelCliente> FindAll()
        {
            
            try
            {
                List<ServicioDelCliente> losServiciosDelCliente = Contexto.ServiciosDelCliente
                    .Include(serv => serv.FrecuenciaDelServicio)
                    .Include(serv => serv.ServicioContratado)
                    .Include(serv => serv.MonedaDelServicio)
                    .Include(serv => serv.EstadoDelServicioDelCliente)
                    .ToList();
                if (losServiciosDelCliente != null) {
                    return losServiciosDelCliente;
                }
                else
                {
                    throw new ServicioDelClienteException("No hay servicios del cliente ingresados en el sistema");
                }

            }
            catch (ServicioDelClienteException ex)
            {
                logAzure.LogError(ex.Message);
                throw;
            }
            catch (Exception e) {
                logAzure.LogError(e.Message);
                throw;
            }
            
        }

        public ServicioDelCliente FindById(int id)
        {
            try
            {
                return Contexto.ServiciosDelCliente.Where(serv => serv.Id == id)
                    .Include(ser => ser.Cliente)
                    .Include(ser => ser.FrecuenciaDelServicio)
                    .Include(ser => ser.EstadoDelServicioDelCliente)
                    .Include(ser => ser.MonedaDelServicio)
                    .Include(ser => ser.ServicioContratado).SingleOrDefault();
            }
            catch (ServicioDelClienteException ex)
            {
                logAzure.LogError(ex.Message);
                throw;
            }
            catch (Exception e) {
                logAzure.LogError(e.Message);
                throw;
            }
        }

        public void Remove(int id)
        {
            ServicioDelCliente elServicioDelClienteAEliminar = Contexto.ServiciosDelCliente.Find(id);
            try
            {
                if (elServicioDelClienteAEliminar != null)
                {
                    // Obtener las notificaciones asociadas al ServicioDelCliente
                    List<Notificacion> notificaciones = Contexto.Notificaciones
                        .Where(n => n.ServicioNotificadoId == id)
                        .ToList();
                    if (notificaciones.Count > 0)
                    {
                        throw new ServicioDelClienteException("No se puede eliminar el servicio del cliente porque tiene notificaciones asociadas.");
                    }

                    Contexto.Remove(elServicioDelClienteAEliminar);
                    Contexto.SaveChanges();
                }
                else
                {
                    throw new ServicioDelClienteException("No se pudo dar la baja, el servicio de cliente no existe en el sistema");
                }

            }
            catch (ServicioDelClienteException ex)
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

        public void Update(ServicioDelCliente obj)
        {

            
            try
            {
                Frecuencia laFrecuencia = Contexto.Frecuencias.Find(obj.FrecuenciaDelServicioId);
                Moneda laMoneda = Contexto.Monedas.Find(obj.MonedaDelServicioId);
                Servicio elServicio = Contexto.Servicios.Find(obj.ServicioContratadoId);
                ServicioDelCliente elServicioACambiar = Contexto.ServiciosDelCliente
                    .Include(ser => ser.Cliente)
                    .Include(ser => ser.EstadoDelServicioDelCliente)
                    .Where(ser => ser.Id == obj.Id).SingleOrDefault();
                Contexto.Entry(elServicioACambiar).State = EntityState.Detached;
                Contexto.Entry(obj).State = EntityState.Modified;
                if (laFrecuencia != null)
                {
                    if (laMoneda != null)
                    {
                        if (elServicio != null)
                        {
                            if (elServicioACambiar != null)
                            {
                                if (elServicioACambiar.EstadoDelServicioDelCliente.Nombre == "Activo")
                                {
                                    obj.FrecuenciaDelServicio = laFrecuencia;
                                    obj.MonedaDelServicio = laMoneda;
                                    obj.ServicioContratado = elServicio;
                                    obj.Cliente = elServicioACambiar.Cliente;
                                    obj.EstadoDelServicioDelCliente = elServicioACambiar.EstadoDelServicioDelCliente;
                                    laFrecuencia.ValidarFechaMaxima(obj.FechaInicio);
                                    laFrecuencia.CalcularVencimiento(obj);
                                    obj.Validar();
                                    Contexto.ServiciosDelCliente.Update(obj);
                                    Contexto.SaveChanges();
                                }
                                else
                                {
                                    throw new ServicioDelClienteException("El servicio debe estar en estado Activo");
                                }
                            }
                            else
                            {
                                throw new ServicioDelClienteException("No se pudo encontrar el servicio del cliente a modificar");
                            }
                        }
                        else
                        {
                            throw new ServicioDelClienteException("No se pudo encontrar el servicio a modificar");
                        }
                    }
                    else
                    {
                        throw new ServicioDelClienteException("Debe seleccionar una moneda");
                    }
                }
                else
                {
                    throw new ServicioDelClienteException("Debe seleccionar una frecuencia de servicio");
                }
            }
            catch (ServicioDelClienteException ex)
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

        public IEnumerable<ServicioDelCliente> ServiciosDeUnCliente(int idCliente)
        {
            try
            {
                List<ServicioDelCliente> losServiciosDeClientes = Contexto.ServiciosDelCliente
                .Include(servCli => servCli.Cliente)
                .Include(servCli => servCli.ServicioContratado)
                .Include(servCli => servCli.MonedaDelServicio)
                .Include(servCli => servCli.EstadoDelServicioDelCliente)
                .Include(serCli => serCli.FrecuenciaDelServicio)
                .Where(servCli => servCli.ClienteId == idCliente).ToList();
                return losServiciosDeClientes;

            }
            catch (ServicioDelClienteException ex)
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

        public IEnumerable<ServicioDelCliente> ServiciosActivosDeUnCliente(int idCliente)
        {
            try
            {
                if (idCliente != null || idCliente != 0)
                {
                    List<ServicioDelCliente> losServiciosDeClientes = Contexto.ServiciosDelCliente
                   .Include(servCli => servCli.Cliente)
                   .Include(servCli => servCli.ServicioContratado)
                   .Include(servCli => servCli.MonedaDelServicio)
                   .Include(servCli => servCli.EstadoDelServicioDelCliente)
                   .Include(serCli => serCli.FrecuenciaDelServicio)
                   .Where(servCli => servCli.ClienteId == idCliente &&
                                 servCli.EstadoDelServicioDelCliente.Nombre == "Activo")
                   .ToList();
                    return losServiciosDeClientes;
                }
                else {
                    throw new ServicioDelClienteException("El ID del cliente es inválido.");
                }
                   

            }
            catch (ServicioDelClienteException ex)
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

        public IEnumerable<ServicioDelCliente> ServiciosPagosDeUnCliente(int idCliente)
        {
            try
            {
                if (idCliente != null || idCliente != 0)
                {
                    List<ServicioDelCliente> losServiciosDeClientes = Contexto.ServiciosDelCliente
                   .Include(servCli => servCli.Cliente)
                   .Include(servCli => servCli.ServicioContratado)
                   .Include(servCli => servCli.MonedaDelServicio)
                   .Include(servCli => servCli.EstadoDelServicioDelCliente)
                   .Include(serCli => serCli.FrecuenciaDelServicio)
                   .Where(servCli => servCli.ClienteId == idCliente &&
                                 servCli.EstadoDelServicioDelCliente.Nombre == "Pago")
                   .ToList();
                    return losServiciosDeClientes;
                }
                else
                {
                    throw new ServicioDelClienteException("El ID del cliente es inválido.");
                }


            }
            catch (ServicioDelClienteException ex)
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

        public void RenovarServicio(ServicioDelCliente servicioDelCliente)
        {
            try
            {
                ServicioDelCliente nuevoServicio = new ServicioDelCliente();
                nuevoServicio.Cliente = servicioDelCliente.Cliente;
                nuevoServicio.ServicioContratado = servicioDelCliente.ServicioContratado;
                nuevoServicio.MonedaDelServicio = servicioDelCliente.MonedaDelServicio;
                nuevoServicio.FrecuenciaDelServicio = servicioDelCliente.FrecuenciaDelServicio;
                nuevoServicio.EstadoDelServicioDelCliente = Contexto.EstadosServiciosDelClientes.FirstOrDefault(e => e.Nombre == "Activo");
                nuevoServicio.Precio = servicioDelCliente.Precio;
                nuevoServicio.Descripcion = servicioDelCliente.Descripcion;
                nuevoServicio.FechaInicio = servicioDelCliente.FechaVencimiento;
                nuevoServicio.FrecuenciaDelServicio.CalcularVencimiento(nuevoServicio);
                Contexto.Add(nuevoServicio);
            }
            catch (ServicioDelClienteException ex)
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


        public ServicioDelCliente ObtenerProximoServicioActivoAVencerse(int idCliente)
        {
            try
            {
                return Contexto.ServiciosDelCliente
                .Where(s => s.ClienteId == idCliente
                    && s.FechaVencimiento > DateTime.Now
                    && s.EstadoDelServicioDelCliente.Nombre == "Activo")
                 .OrderBy(s => s.FechaVencimiento)
                .FirstOrDefault();
            }
            catch (ServicioDelClienteException ex)
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

        public void Actualizar(RepositorioCobros obj, string evento)
        {
            if (evento == "AltaCobro")
                try
            {
                // Obtener el último cobro recibido
                var ultimoCobro = obj.Contexto.CobrosRecibidos
                    .Include(c => c.ServicioDelCliente)
                    .ThenInclude(ser => ser.FrecuenciaDelServicio)
                    .OrderByDescending(c => c.Id)
                    .FirstOrDefault();

                if (ultimoCobro != null)
                {
                    // Obtener el servicio del cliente asociado al cobro
                    var servicioDelCliente = ultimoCobro.ServicioDelCliente;

                    if (servicioDelCliente != null)
                    {
                        // Actualizar el estado del servicio del cliente
                        var estadoPagado = obj.Contexto.EstadosServiciosDelClientes
                            .FirstOrDefault(e => e.Nombre == "Pago");

                        if (estadoPagado != null)
                        {
                            servicioDelCliente.EstadoDelServicioDelCliente = estadoPagado;
                            obj.Contexto.SaveChanges();
                            RenovarServicio(servicioDelCliente);
                        }
                        else
                        {
                            throw new Exception("Estado 'Pago' no encontrado.");
                        }
                    }
                    else
                    {
                        throw new Exception("Servicio del cliente no encontrado para el cobro recibido.");
                    }
                }
                else
                {
                    throw new Exception("No se encontró ningún cobro recibido.");
                }
            }
            catch (Exception ex)
            {
                    logAzure.LogError(ex.Message);
                    throw new Exception("Error updating ServicioDelCliente", ex);
            }
        }


        // FUNCIONES PARA REPORTES //
        // INDICADOR DE SERVICIOS DE UN CLIENTE MONTO ANUAL
        public decimal CalcularIngresosProximos365Dias(int idCliente)
        {
            try
            {
                CotizacionDolar ultimaCotizacion = Contexto.Cotizaciones
                                   .OrderByDescending(c => c.Fecha)
                                   .FirstOrDefault();

                IEnumerable<ServicioDelCliente> serviciosActivos = Contexto.ServiciosDelCliente
                    .Include(s => s.FrecuenciaDelServicio)
                    .Include(s => s.MonedaDelServicio)
                    .Where(s => s.ClienteId == idCliente
                        && s.EstadoDelServicioDelCliente.Nombre == "Activo"
                        && s.FechaVencimiento <= DateTime.Now.AddYears(1));

                decimal totalIngresos = 0;
                int multiplicador = 1;
                foreach (var servicio in serviciosActivos)
                {
                    decimal montoServicio = servicio.Precio;
                    if (servicio.MonedaDelServicio.Nombre == "Pesos")
                    {
                        if (ultimaCotizacion != null)
                        {
                            montoServicio = servicio.MonedaDelServicio.CovertirADolares(servicio.Precio, ultimaCotizacion.Valor);
                            //montoServicio = montoServicio / ultimaCotizacion.Valor;
                        }
                    }
                    switch (servicio.FrecuenciaDelServicio.Nombre)
                    {
                        case "Mensual":
                            totalIngresos += montoServicio * 12;
                            break;
                        case "Trimestral":
                            totalIngresos += montoServicio * 4;
                            break;
                        case "Semestral":
                            totalIngresos += montoServicio * 2;
                            break;
                        case "Anual":
                            totalIngresos += montoServicio;
                            break;
                    }

                }

                return totalIngresos;
            }
            catch (ServicioDelClienteException ex)
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

        public IEnumerable<ServicioDelCliente> ServiciosDeClientesDeUnSuscriptor(int idSuscriptor)
        {
            try
            {
                if (idSuscriptor != null || idSuscriptor != 0)
                {
                    List<ServicioDelCliente> serviciosActivos = Contexto.ServiciosDelCliente
                        .Include(servCli => servCli.Cliente)
                        .Include(servCli => servCli.ServicioContratado)
                        .Include(servCli => servCli.MonedaDelServicio)
                        .Include(servCli => servCli.EstadoDelServicioDelCliente)
                        .Include(servCli => servCli.FrecuenciaDelServicio)
                        .Where(servCli => servCli.Cliente.SuscriptorId == idSuscriptor)
                        .ToList();
                    return serviciosActivos;
                }
                else
                {
                    throw new ServicioDelClienteException("El ID del suscriptor es inválido.");
                }
            }
            catch (ServicioDelClienteException ex)
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

        public IEnumerable<ServicioDelCliente> ServiciosDeClientesDeUnSuscriptorQueVencenEsteMes(int idSuscriptor)
        {
            try
            {
                if (idSuscriptor != null || idSuscriptor != 0)
                {
                    IEnumerable<ServicioDelCliente> serviciosDelSuscriptor = ServiciosDeClientesDeUnSuscriptor(idSuscriptor);
                    IEnumerable<ServicioDelCliente> serviciosQueVencenEsteMes = serviciosDelSuscriptor.Where(s => s.FechaVencimiento.Year == DateTime.Now.Year && s.FechaVencimiento.Month == DateTime.Now.Month);
                    return serviciosQueVencenEsteMes;
                }
                else
                {
                    throw new ServicioDelClienteException("El ID del suscriptor es inválido.");
                }
            }
            catch (ServicioDelClienteException ex)
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

        public Dictionary<string, decimal> ObtenerIndicadoresServiciosVencenEsteMes(int idSuscriptor)
        {
            var indicadores = new Dictionary<string, decimal>();

            try
            {
                DateTime fechaActual = DateTime.Now;
                List<ServicioDelCliente> serviciosVencenEsteMes = ServiciosDeClientesDeUnSuscriptor(idSuscriptor)
                    .Where(s => s.FechaVencimiento.Year == fechaActual.Year &&
                                s.FechaVencimiento.Month == fechaActual.Month)
                    .ToList();

                CotizacionDolar cotizacionDolar = Contexto.Cotizaciones
                .OrderByDescending(c => c.Fecha)
                .FirstOrDefault();

                decimal montoTotalRenovaciones = 0;
                decimal montoYaCobrado = 0;
                foreach (var servicio in serviciosVencenEsteMes)
                {
                    decimal montoDelServicioEnDolares = servicio.MonedaDelServicio.CovertirADolares(servicio.Precio, cotizacionDolar.Valor);
                    montoTotalRenovaciones += montoDelServicioEnDolares;
                    if(servicio.EstadoDelServicioDelCliente.Nombre == "Pago")
                    {
                        montoYaCobrado += montoDelServicioEnDolares;
                    }
                }

                var montoPendienteCobro = montoTotalRenovaciones - montoYaCobrado;
                var cantidadVencimientos = serviciosVencenEsteMes.Count;

                indicadores["MontoTotalRenovaciones"] = montoTotalRenovaciones;
                indicadores["MontoYaCobrado"] = montoYaCobrado;
                indicadores["MontoPendienteCobro"] = montoPendienteCobro;
                indicadores["CantidadVencimientos"] = cantidadVencimientos;
                indicadores["CotizacionDolar"] = cotizacionDolar.Valor;
            }
            catch (ServicioDelClienteException ex)
            {
                logAzure.LogError(ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                logAzure.LogError(ex.Message);
                throw new ServicioDelClienteException("Error al obtener los indicadores de servicios que vencen este mes", ex);
            }

            return indicadores;
        }

        public IEnumerable<ServicioDelCliente> MarcarServiciosComoVencidos()
        {
            try
            {
                IEnumerable<ServicioDelCliente> serviciosVencidos = Contexto.ServiciosDelCliente
                    .Where(s => s.FechaVencimiento < DateTime.Now && s.EstadoDelServicioDelCliente.Nombre == "Activo")
                    .ToList();

                EstadoServicioDelCliente estadoVencido = Contexto.EstadosServiciosDelClientes
                    .FirstOrDefault(e => e.Nombre == "Vencido");

                if (estadoVencido == null)
                {
                    throw new Exception("Estado 'Vencido' no encontrado en la base de datos.");
                }


                foreach (ServicioDelCliente servicioDelCliente in serviciosVencidos)
                {
                    servicioDelCliente.EstadoDelServicioDelCliente = estadoVencido;
                }

                Contexto.SaveChanges();
                return serviciosVencidos;
            }
            catch (ServicioDelClienteException ex)
            {
                logAzure.LogError(ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                logAzure.LogError(ex.Message);
                throw new Exception($"Error al marcar servicios como vencidos: {ex.Message}", ex);
            }
        }

        public void CancelarServicioDelCliente(int idServicioDelCliente)
        {
            try
            {
                ServicioDelCliente servicio = Contexto.ServiciosDelCliente.FirstOrDefault(s => s.Id == idServicioDelCliente);
                if (servicio == null)
                {
                    throw new ServicioDelClienteException("El servicio no esta cargado en el sistema.");
                }

                EstadoServicioDelCliente estadoActivo = Contexto.EstadosServiciosDelClientes.FirstOrDefault(e => e.Nombre == "Activo");
                EstadoServicioDelCliente estadoCancelado = Contexto.EstadosServiciosDelClientes.FirstOrDefault(e => e.Nombre == "Cancelado");
                if (estadoActivo == null || estadoCancelado == null)
                {
                    throw new ServicioDelClienteException("El estado 'Activo' o 'Cancelado' no existe.");
                }

                if (servicio.EstadoDelServicioDelCliente != estadoActivo)
                {
                    throw new ServicioDelClienteException("El servicio debe estar en estado 'Activo' para poder ser cancelado.");
                }

                servicio.EstadoDelServicioDelCliente = estadoCancelado;
                Contexto.SaveChanges();
            }
            catch (ServicioDelClienteException ex)
            {
                logAzure.LogError(ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                logAzure.LogError(ex.Message);
                throw new Exception($"Error al cancelar servicios de un cliente", ex);
            }

        }
    }
}
