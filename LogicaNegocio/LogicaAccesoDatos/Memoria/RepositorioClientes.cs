using Excepciones;
using LogicaNegocio.Dominio;
using LogicaNegocio.InterfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Text;

namespace LogicaAccesoDatos.Memoria
{
    public class RepositorioClientes : IRepositorioClientes
    {
        public static List<Cliente> ClientesEnMemoria { get; set; } = new List<Cliente>();



        public void Add(Cliente obj)
        {
            if(obj != null)
            {
                obj.Validar();
                ClientesEnMemoria.Add(obj);
            }
            else
            {
                throw new ClienteException("El cliente no puede ser nulo");
            }
        }

        public IEnumerable<Cliente> FindAll()
        {
            return ClientesEnMemoria;
        }

        public Cliente FindById(int id)
        {
            throw new NotImplementedException();
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
