using LogicaAplicacion.InterfacesCU;
using LogicaNegocio.Dominio;
using LogicaNegocio.InterfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Text;

namespace LogicaAplicacion.CasosDeUso
{
    public class ListadoClientes : IListadoClientes
    {
        public IRepositorioClientes RepositorioClientes { get; set; }

        public ListadoClientes(IRepositorioClientes repoClientes)
        {
            RepositorioClientes = repoClientes;
        }

        public IEnumerable<Cliente> ObtenerListado()
        {
            return RepositorioClientes.FindAll();
        }
    }
}
