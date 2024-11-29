﻿using Excepciones;
using LogicaNegocio.Dominio;
using LogicaNegocio.InterfacesDominio;
using LogicaNegocio.InterfacesRepositorios;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAccesoDatos.BaseDatos
{
    public class RepositorioClientes : IRepositorioClientes, IObservador<RepositorioCobros>
    {

        public CobrosContext Contexto { get; set; }

        public RepositorioClientes(CobrosContext context)
        {
            Contexto = context;
        }

        public void Add(Cliente obj)
        {
            try
            {
                obj.Validar();

                Documento elTipoDocumento = Contexto.Documentos.Find(obj.DocumentoId);
                EstadoCliente elEstado = Contexto.EstadosDelCliente.FirstOrDefault(e => e.Nombre == "Activo");
                Pais elPais = Contexto.Paises.Find(obj.PaisId);
                Suscriptor elSuscriptor = Contexto.Suscriptores.Find(obj.SuscriptorId);
                Rol elRol = Contexto.Roles.FirstOrDefault(e => e.Nombre == "Cliente");
                if (elTipoDocumento != null)
                {
                    if(elPais != null)
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
                        throw new ClienteException("El pais es un campo obligatorio");
                    }
                }
                else
                {
                    throw new ClienteException("Debe seleccionar un tipo de documento");
                }
            }
            catch (ClienteException ex)
            {
                throw;
            }
            catch (Exception e)
            {
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
                throw;
            }
            catch (Exception e)
            {
                throw;
            }
            
        }

        public Cliente FindById(int id)
        {
            return Contexto.Clientes.Include(cli => cli.DocumentoCliente).Where(cli => cli.Id == id).SingleOrDefault();
        }


        public Cliente FindByNumDocumento(string numDocumento)
        {
            Cliente elCliente = null;
            List<Cliente> losClientes = Contexto.Clientes.ToList();
            if (losClientes.Count > 0)
            {
                foreach (Cliente item in losClientes)
                {
                    if(item.NumDocumento == numDocumento)
                    {
                        elCliente = item;
                    }
                }
            }
            return elCliente;
            
        }
        public void Remove(int id)
        {
            Cliente elClienteAEliminar = Contexto.Clientes.Find(id);
            try
            {
               if(elClienteAEliminar != null)
                {
                    //validar registros en otras tablas
                    Contexto.Remove(elClienteAEliminar);
                    Contexto.SaveChanges();
                }else
                {
                    throw new ClienteException("No se pudo dar la baja, el cliente no existe en el sistema");
                }
                
            }
            catch (ClienteException ex)
            {
                throw;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public void Update(Cliente obj)
        {
            //Cliente elClienteBD = Contexto.Clientes.Where(c => c.Id == obj.Id)
            //   .Include(c => c.UsuarioLogin)
            //    .ThenInclude(u => u.RolDeUsuario)
            //   .FirstOrDefault();
            Rol elRol = Contexto.Roles.FirstOrDefault(e => e.Nombre == "Cliente");
            Documento elTipoDocumento = Contexto.Documentos.Find(obj.DocumentoId);
            
            Pais elPais = Contexto.Paises.Find(obj.PaisId);

            try
            {
                //Valida Cliente
                obj.Validar();
                obj.UsuarioLogin.RolDeUsuario = elRol;
                obj.DocumentoCliente = elTipoDocumento;
                
                Contexto.Clientes.Update(obj);
                Contexto.SaveChanges();
            }
            catch (ClienteException ce)
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
                    // Logger.LogError(ex, "Error updating Cliente");
                    throw new Exception("Error updating Cliente", ex);
                }
            }
        }
    }
}
