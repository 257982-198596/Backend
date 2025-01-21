using LogicaNegocio.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.InterfacesRepositorios
{
    public interface IRepositorioUsuarios
    {
        Usuario IniciarSesion(string username, string password);

        void ResetContrasena(Usuario usuario);

        void HashExistingPasswords();
    }
}
