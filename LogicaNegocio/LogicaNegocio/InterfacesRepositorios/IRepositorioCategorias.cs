using LogicaNegocio.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.InterfacesRepositorios
{
    public interface IRepositorioCategorias : IRepositorio<Categoria>
    {
        IEnumerable<Categoria> FindAll();

        IEnumerable<Categoria> FindBySuscriptorId(int suscriptorId);
    }
}
