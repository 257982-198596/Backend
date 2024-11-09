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
    public class RepositorioClientes : IRepositorioClientes
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
                //EstadoCliente elEstado = 
                Pais elPais = Contexto.Paises.Find(obj.PaisId);
                //Falta Suscriptor
                if(elTipoDocumento != null)
                {
                    if(elPais != null)
                    {
                        Cliente elCliente = FindByNumDocumento(obj.NumDocumento);
                        if (elCliente == null)
                        {
                            obj.DocumentoCliente = elTipoDocumento;
                            obj.Pais = elPais;
                            Contexto.Add(obj);
                            Contexto.SaveChanges();
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
                List<Cliente> losClientes = Contexto.Clientes.ToList();
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
            try
            {
                //Valida Cliente
                obj.Validar();
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
    }
}
