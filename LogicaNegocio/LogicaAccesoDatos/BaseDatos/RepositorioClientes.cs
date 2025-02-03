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
using BCrypt.Net;
using Microsoft.Extensions.Logging;

namespace LogicaAccesoDatos.BaseDatos
{
    public class RepositorioClientes : IRepositorioClientes, IObservador<RepositorioCobros>
    {

        public CobrosContext Contexto { get; set; }

        private readonly ILogger<RepositorioClientes> logAzure;

        public RepositorioClientes(CobrosContext context, ILogger<RepositorioClientes> logger)
        {
            Contexto = context;
            logAzure = logger;
        }

        public void Add(Cliente obj)
        {
            try
            {
                //Validacion si existe otro cliente en el sistema con el mismo nombre para ese suscriptor
                Cliente clienteExistente = Contexto.Clientes
                .FirstOrDefault(c => c.Nombre.ToLower() == obj.Nombre && c.SuscriptorId == obj.SuscriptorId);

                if (clienteExistente != null)
                {
                    throw new ClienteException("Ya existe un cliente con el mismo nombre para este suscriptor.");
                }
                //Validacion si existe email en el sistema
                Usuario usuarioExistente = Contexto.Usuarios.FirstOrDefault(u => u.Email == obj.UsuarioLogin.Email);
                if (usuarioExistente != null)
                {
                    throw new ClienteException("El correo electrónico ya está registrado en el sistema");
                }

                Documento elTipoDocumento = Contexto.Documentos.Find(obj.DocumentoId);
                EstadoCliente elEstado = Contexto.EstadosDelCliente.FirstOrDefault(e => e.Nombre == "Activo");
                Pais elPais = Contexto.Paises.Find(obj.PaisId);
                Suscriptor elSuscriptor = Contexto.Suscriptores.Find(obj.SuscriptorId);
                Rol elRol = Contexto.Roles.FirstOrDefault(e => e.Nombre == "Cliente");
                
                if (elTipoDocumento != null)
                {
                    if (elPais != null)
                    {
                        Cliente elCliente = FindByNumDocumento(obj.NumDocumento);

                        if (elCliente == null)
                        {
                            if (elSuscriptor != null)
                            {

                                obj.DocumentoCliente = elTipoDocumento;
                                obj.UsuarioLogin.RolDeUsuario = elRol;
                                obj.Estado = elEstado;
                                obj.Pais = elPais;
                                obj.SuscriptorId = elSuscriptor.Id;
                                obj.UsuarioLogin.ValidarContrasena(obj.UsuarioLogin.Password);
                                obj.Validar();
                                obj.UsuarioLogin.Password = BCrypt.Net.BCrypt.HashPassword(obj.UsuarioLogin.Password);
                                Contexto.Add(obj);
                                Contexto.SaveChanges();
                            }
                            else
                            {
                                throw new ClienteException("El Suscriptor no existe en el sistema");
                            }
                            ;
                        }
                        else
                        {
                            throw new ClienteException("Ya existe un cliente con ese numero de documento en el sistema");
                        }

                    }
                    else
                    {
                        throw new ClienteException("El pais seleccionado no existe en el sistema");
                    }
                }
                else
                {
                    throw new ClienteException("El tipo de documento seleccionado no existe en el sistema");
                }
            }
            catch (ClienteException ex)
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

        public IEnumerable<Cliente> FindAll()
        {
            try
            {
                List<Cliente> losClientes = Contexto.Clientes
                    .Include(cli => cli.DocumentoCliente)
                    .Include(cli => cli.UsuarioLogin)
                    .Include(cli => cli.Pais)
                    .Include(cli => cli.CobrosDelCliente)
                    .Include(cli => cli.Estado)
                    .Include(cli => cli.ServiciosDelCliente)
                    .ThenInclude(servCli => servCli.ServicioContratado)
                    .ToList();
                if (losClientes != null)
                {
                    return losClientes;
                }
                else
                {
                    throw new ClienteException("No hay clientes ingresados en el sistema");
                }

            }
            catch (ClienteException ex)
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

        // POR SUSCRIPTOR
        public IEnumerable<Cliente> FindAllBySuscriptorId(int suscriptorId)
        {
            try
            {
                List<Cliente> losClientes = Contexto.Clientes
                    .Include(cli => cli.DocumentoCliente)
                    .Include(cli => cli.UsuarioLogin)
                    .Include(cli => cli.Pais)
                    .Include(cli => cli.CobrosDelCliente)
                    .Include(cli => cli.Estado)
                    .Include(cli => cli.ServiciosDelCliente)
                    .ThenInclude(servCli => servCli.ServicioContratado)
                    .Where(cli => cli.SuscriptorId == suscriptorId)
                    .ToList();

                if (losClientes != null)
                {
                    return losClientes;
                }
                else
                {
                    throw new ClienteException("No hay clientes ingresados en el sistema para el suscriptor especificado");
                }
            }
            catch (ClienteException ex)
            {
                logAzure.LogError(ex.Message);
                throw;
            }
            catch (Exception e)
            {
                logAzure.LogError(e.Message);
                throw new ClienteException("Error al obtener los clientes del suscriptor", e);
            }
        }


        public Cliente FindById(int id)
        {
            try
            {
                return Contexto.Clientes.Include(cli => cli.DocumentoCliente).Include(cli => cli.UsuarioLogin).Where(cli => cli.Id == id).SingleOrDefault();
            }
            catch (ClienteException ex)
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


        public Cliente FindByNumDocumento(string numDocumento)
        {
            try
            {
                return Contexto.Clientes.FirstOrDefault(c => c.NumDocumento == numDocumento);
            }
            catch (ClienteException ex)
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
            Cliente elClienteAEliminar = Contexto.Clientes.Include(c => c.ServiciosDelCliente).FirstOrDefault(c => c.Id == id);
            try
            {
                if (elClienteAEliminar != null)
                {
                    if (elClienteAEliminar.ServiciosDelCliente != null && elClienteAEliminar.ServiciosDelCliente.Any())
                    {
                        throw new ClienteException("El cliente tiene servicios asociados y no puede ser eliminado");
                    }

                    Contexto.Remove(elClienteAEliminar);
                    Contexto.SaveChanges();
                } else
                {
                    throw new ClienteException("No se pudo dar la baja, el cliente no existe en el sistema");
                }

            }
            catch (ClienteException ex)
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

        public void Update(Cliente obj)
        {
            try
            {
                // Traigo rol cliente
                Rol elRol = Contexto.Roles.FirstOrDefault(e => e.Nombre == "Cliente");
                // Busco el tipo de documento
                Documento elTipoDocumento = Contexto.Documentos.Find(obj.DocumentoId);
                // Busco el país
                Pais elPais = Contexto.Paises.Find(obj.PaisId);

                // Busco el cliente existente en la base de datos
                Cliente clienteExistente = Contexto.Clientes.Include(c => c.UsuarioLogin)
                    .Include(c => c.Pais)
                    .Include(c => c.DocumentoCliente)
                    .Include(c => c.Estado)
                    .Include(c => c.CobrosDelCliente)
                    .Include(c => c.ServiciosDelCliente)
                    .FirstOrDefault(c => c.Id == obj.Id);

                if (clienteExistente != null)
                {
                    // Actualiza las propiedades del cliente existente
                    clienteExistente.Nombre = obj.Nombre;
                    clienteExistente.Telefono = obj.Telefono;
                    clienteExistente.Direccion = obj.Direccion;
                    clienteExistente.PersonaContacto = obj.PersonaContacto;
                    clienteExistente.PaisId = obj.PaisId;
                    //clienteExistente.SuscriptorId = obj.SuscriptorId;

                    // No se permite editar Documento, NumDocumento y Email del usuario
                    clienteExistente.DocumentoCliente = elTipoDocumento;
                    clienteExistente.UsuarioLogin.RolDeUsuario = elRol;

                    // Validar el cliente
                    clienteExistente.Validar();

                    // Evitar la inserción en cascada
                    Contexto.Entry(clienteExistente.UsuarioLogin).State = EntityState.Unchanged;

                    // Actualizar el cliente
                    Contexto.Clientes.Update(clienteExistente);
                    Contexto.SaveChanges();
                }
                else
                {
                    throw new ClienteException("El cliente no existe.");
                }
            }
            catch (ClienteException ex)
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

        public void Actualizar(RepositorioCobros obj, string evento)
        {
            if (evento == "AltaCobro")
            {
                try
                {
                    // Obtener el último cobro recibido
                    var ultimoCobro = obj.Contexto.CobrosRecibidos
                        .Include(cob => cob.ServicioDelCliente)
                        .ThenInclude(ser => ser.Cliente)
                        .ThenInclude(cli => cli.CobrosDelCliente)
                        .OrderByDescending(c => c.Id)
                        .FirstOrDefault();

                    if (ultimoCobro != null)
                    {
                        // Obtener el cliente asociado al cobro
                        var cliente = ultimoCobro.ServicioDelCliente.Cliente;

                        if (cliente != null)
                        {
                            // Agregar el cobro al cliente
                            cliente.CobrosDelCliente.Add(ultimoCobro);
                            obj.Contexto.SaveChanges();
                        }
                        else
                        {
                            throw new Exception("Cliente no encontrado para el cobro recibido.");
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
                    throw new Exception("Error updating Cliente", ex);
                }
            }/*else if(evento == "AltaNotificacion")
            {
                try
                {
                    
                    // Obtener la última notificación
                    Notificacion ultimaNotificacion = obj.Contexto.Notificaciones
                        .Include(n => n.EstadoDeNotificacion)
                        .OrderByDescending(n => n.Id)
                        .FirstOrDefault();

                    if (ultimaNotificacion != null)
                    {
                        CobroRecibido ultimoCobro = obj.Contexto.CobrosRecibidos
                        .Include(c => c.ServicioDelCliente)
                        .ThenInclude(s => s.Cliente)
                        .ThenInclude(c => c.NotificacionesDelCliente)
                        .OrderByDescending(c => c.Id)
                        .FirstOrDefault();

                        // Obtener el cliente asociado al cobro
                        if (ultimoCobro?.ServicioDelCliente?.Cliente != null)
                        {
                            var cliente = ultimoCobro.ServicioDelCliente.Cliente;

                            // Asociar la notificación al cliente
                            cliente.NotificacionesDelCliente.Add(ultimaNotificacion);
                            obj.Contexto.SaveChanges();
                        }
                        else
                        {
                            throw new Exception("Cliente no encontrado para el último cobro recibido.");
                        }
                    }
                    else
                    {
                        throw new Exception("No se encontró ningún cobro recibido.");
                    }
                }
                catch (Exception ex)
                {
                    // Logger.LogError(ex, "Error updating Cliente");
                    throw new Exception("Error updating Cliente", ex);
                }
            }*/
        
        }

        public void HabilitarCliente(int id)
        {
            try
            {
                Cliente cliente = Contexto.Clientes.Find(id);
                if (cliente != null)
                {

                    EstadoCliente estadoActivo = Contexto.EstadosDelCliente.FirstOrDefault(e => e.Nombre == "Activo");
                    if (estadoActivo != null)
                    {
                        cliente.Estado = estadoActivo;
                        Contexto.SaveChanges();
                    }
                    else
                    {
                        throw new ClienteException("Estado 'Activo' no encontrado");
                    }

                }else
                {
                    throw new ClienteException("Cliente no encontrado");
                }

                
            }
            catch (Exception e)
            {
                logAzure.LogError(e.Message);
                throw new ClienteException("Error al habilitar el cliente", e);
            }
        }

        public void DeshabilitarCliente(int id)
        {
            try
            {
                Cliente cliente = Contexto.Clientes.Find(id);
                if (cliente != null)
                {
                    EstadoCliente estadoInactivo = Contexto.EstadosDelCliente.FirstOrDefault(e => e.Nombre == "Inactivo");
                    if (estadoInactivo != null)
                    {
                        cliente.Estado = estadoInactivo;
                        Contexto.SaveChanges();
                    }
                    else
                    {
                        throw new ClienteException("Estado 'Inactivo' no encontrado");
                    }
                }
                else
                {
                    throw new ClienteException("Cliente no encontrado");
                }
            }
            catch (ClienteException ex)
            {
                logAzure.LogError(ex.Message);
                throw;
            }
            catch (Exception e)
            {
                
                logAzure.LogError(e.Message);
                throw new ClienteException("Error al deshabilitar el cliente", e);
            }
        }

        public Cliente UpdatePerfilCliente(Cliente obj)
        {
            try
            {
                // Traigo rol cliente
                Rol elRol = Contexto.Roles.FirstOrDefault(e => e.Nombre == "Cliente");
                // Busco el tipo de documento
                Documento elTipoDocumento = Contexto.Documentos.Find(obj.DocumentoId);
                // Busco el país
                Pais elPais = Contexto.Paises.Find(obj.PaisId);

                // Busco el cliente existente en la base de datos
                Cliente clienteExistente = Contexto.Clientes.Include(c => c.UsuarioLogin)
                    .Include(c => c.Pais)
                    .Include(c => c.DocumentoCliente)
                    .Include(c => c.Estado)
                    .Include(c => c.CobrosDelCliente)
                    .Include(c => c.ServiciosDelCliente)
                    .FirstOrDefault(c => c.Id == obj.Id);

                if (clienteExistente != null)
                {
                    // Actualiza las propiedades del cliente existente
                    clienteExistente.Nombre = obj.Nombre;
                    clienteExistente.Telefono = obj.Telefono;
                    clienteExistente.Direccion = obj.Direccion;
                    clienteExistente.PersonaContacto = obj.PersonaContacto;
                    clienteExistente.PaisId = obj.PaisId;
                

                    // No se permite editar Documento, NumDocumento y Email del usuario
                    clienteExistente.DocumentoCliente = elTipoDocumento;
                    clienteExistente.UsuarioLogin.RolDeUsuario = elRol;
                   
                    // Verificar si la contraseña ha cambiado
                    if (!string.IsNullOrWhiteSpace(obj.UsuarioLogin.Password) &&
                        !BCrypt.Net.BCrypt.Verify(obj.UsuarioLogin.Password, clienteExistente.UsuarioLogin.Password))
                    {
                        obj.UsuarioLogin.ValidarContrasena(obj.UsuarioLogin.Password);
                        clienteExistente.UsuarioLogin.Password = BCrypt.Net.BCrypt.HashPassword(obj.UsuarioLogin.Password);

                        // Marcar la entidad UsuarioLogin como modificada
                        Contexto.Entry(clienteExistente.UsuarioLogin).State = EntityState.Modified;
                    }

                    // Validar el cliente
                    clienteExistente.Validar();

                    Contexto.Clientes.Update(clienteExistente);
                    Contexto.SaveChanges();
                    return clienteExistente;
                }
                else
                {
                    throw new ClienteException("El cliente no existe.");
                }
            }
            catch (ClienteException ex)
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
    }
}