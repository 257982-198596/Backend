using LogicaNegocio.Dominio;
using System;
using System.Collections.Generic;
using System.Text;

namespace LogicaAplicacion.InterfacesCU
{
    public interface IListadoClientes
    {
        IEnumerable<Cliente> ObtenerListado();
    }
}
