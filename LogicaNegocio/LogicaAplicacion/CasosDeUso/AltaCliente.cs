using LogicaAplicacion.InterfacesCU;
using LogicaNegocio.Dominio;
using LogicaNegocio.InterfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Text;

namespace LogicaAplicacion.CasosDeUso
{
    public class AltaCliente : IAltaCliente
    {
        public IRepositorioClientes RepositorioClientes { get; set; }
        public AltaCliente(IRepositorioClientes repoClientes)
        {
            RepositorioClientes = repoClientes;
        }
        public void Alta(Cliente nuevo)
        {
            RepositorioClientes.Add(nuevo);
        }
    }
}
