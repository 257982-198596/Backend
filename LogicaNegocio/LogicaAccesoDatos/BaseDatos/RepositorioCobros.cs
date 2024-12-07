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

namespace LogicaAccesoDatos.BaseDatos
{
    public class RepositorioCobros : Observable<RepositorioCobros>, IRepositorioCobros
    {

        private readonly EnviarCorreo SistemaEnviarCorreo;
        public CobrosContext Contexto { get; set; }

        public RepositorioCobros(CobrosContext context, EnviarCorreo enviarCorreo)
        {
            Contexto = context;
            SistemaEnviarCorreo = enviarCorreo;
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
                //Cliente elCliente = Contexto.Clientes.Find(elServicioDelCliente.ClienteId);
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

                            SistemaEnviarCorreo.EnviarRenovacionServicio(obj, elCliente);
                            Notificacion laNotificacion = new Notificacion(DateTime.Now, $"RENOVACIÓN DE SERVICIO: {obj.ServicioDelCliente.Descripcion}");

                            
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
                throw;
            }
            catch (Exception e)
            {
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
                throw;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public CobroRecibido FindById(int id)
        {
            return Contexto.CobrosRecibidos.Where(co => co.Id == id).SingleOrDefault();
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
                throw;
            }
            catch (Exception e)
            {
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
            catch (ServicioException ce)
            {
                throw;
            }
            catch (Exception e)
            {
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
    }
}
