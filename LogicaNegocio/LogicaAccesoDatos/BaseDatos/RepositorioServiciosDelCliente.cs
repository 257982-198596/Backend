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
    public class RepositorioServiciosDelCliente : IRepositorioServiciosDelCliente
    {
        public CobrosContext Contexto { get; set; }

        public RepositorioServiciosDelCliente(CobrosContext context)
        {
            Contexto = context;
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
                        if (laFrecuencia != null)
                        {
                            if (laMoneda != null)
                            {
                                if(elEstadoInicial != null)
                                {
                                    obj.ServicioContratado = elServicio;
                                    obj.Cliente = elCliente;
                                    obj.FrecuenciaDelServicio = laFrecuencia;
                                    obj.MonedaDelServicio = laMoneda;
                                    obj.EstadoDelServicioDelCliente = elEstadoInicial;
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
                throw;
            }
            catch (Exception e)
            {
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
                throw;
            }
            catch (Exception e) {
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
                throw;
            }
            catch (Exception e) {
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
                    //validar registros en otras tablas
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
                throw;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public void Update(ServicioDelCliente obj)
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
            try
            {
                obj.FrecuenciaDelServicio = laFrecuencia;
                obj.MonedaDelServicio = laMoneda;
                obj.ServicioContratado = elServicio;
                obj.Cliente = elServicioACambiar.Cliente;
                obj.EstadoDelServicioDelCliente = elServicioACambiar.EstadoDelServicioDelCliente;
                laFrecuencia.CalcularVencimiento(obj);
                obj.Validar();
                Contexto.ServiciosDelCliente.Update(obj);
                Contexto.SaveChanges();
            }
            catch (ServicioDelClienteException ce)
            {
                throw;
            }
            catch (Exception e)
            {
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
                throw;
            }
            catch (Exception e)
            {
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
                throw;
            }
            catch (Exception e)
            {
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
                throw;
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}
