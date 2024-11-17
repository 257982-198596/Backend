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
    public class RepositorioUsuarios : IRepositorioUsuarios
    {
        public CobrosContext Contexto { get; set; }

        public RepositorioUsuarios(CobrosContext context)
        {
            Contexto = context;
        }

        public Usuario IniciarSesion(string username, string password)
        {
            Usuario usuarioIniciado = null;
            List<Usuario> losUsuarios = Contexto.Usuarios.Include(u => u.RolDeUsuario).ToList();
            if (losUsuarios != null)
            {
                foreach (Usuario item in losUsuarios)
                {
                    if (item.Email == username && item.Password == password)
                    {
                        usuarioIniciado = item;
                    }
                }
            }
            return usuarioIniciado;
        }
    }
}
