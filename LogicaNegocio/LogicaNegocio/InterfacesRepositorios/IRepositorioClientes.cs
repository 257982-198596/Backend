using LogicaNegocio.Dominio;
using System;
using System.Collections.Generic;
using System.Text;

namespace LogicaNegocio.InterfacesRepositorios
{
    public interface IRepositorioClientes : IRepositorio<Cliente>
    {

        void HabilitarCliente(int id);

        void DeshabilitarCliente(int id);

        IEnumerable<Cliente> FindAllBySuscriptorId(int suscriptorId);

        Cliente UpdatePerfilCliente(Cliente obj);
    }
}
