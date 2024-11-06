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

                Documento elDocumento = Contexto.Documentos.Find(obj.DocumentoId);
                //EstadoCliente elEstado = 
                Pais elPais = Contexto.Paises.Find(obj.PaisId);
                //Falta Suscriptor
                if(elDocumento != null)
                {
                    if(elPais != null)
                    {
                        obj.DocumentoCliente = elDocumento;
                        obj.Pais = elPais;
                        Contexto.Add(obj);
                        Contexto.SaveChanges();
                    }
                    else
                    {
                        throw new ClienteException("");
                    }
                }
                else
                {
                    throw new ClienteException("");
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
            return Contexto.Clientes.ToList();
        }

        public Cliente FindById(int id)
        {
            return Contexto.Clientes.Include(cli => cli.DocumentoCliente).Where(cli => cli.Id == id).SingleOrDefault();
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Cliente obj)
        {
            throw new NotImplementedException();
        }
    }
}
