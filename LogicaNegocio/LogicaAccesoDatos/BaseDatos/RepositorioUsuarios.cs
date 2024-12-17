using LogicaNegocio.Dominio;
using LogicaNegocio.InterfacesRepositorios;
using Microsoft.EntityFrameworkCore;
using SistemaDeNotificaciones;
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

        private readonly EnviarCorreo SistemaEnviarCorreo;

        public RepositorioUsuarios(CobrosContext context, EnviarCorreo sistemaCorreos)
        {
            Contexto = context;
            SistemaEnviarCorreo = sistemaCorreos;
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

        public void ResetContrasena(Usuario usuario)
        {
            Usuario elUsuario = Contexto.Usuarios.Find(usuario.Id);
            if (elUsuario == null)
            {
                throw new Exception("Usuario no encontrado");
            }

            string contrasenaTemporal = elUsuario.GenerarContrasenaTemporal();
            elUsuario.Password = contrasenaTemporal; 
            Contexto.Usuarios.Update(elUsuario);
            Contexto.SaveChanges();

            SistemaEnviarCorreo.EnviarContrasenaTemporal(new Cliente { UsuarioLogin = elUsuario }, contrasenaTemporal);
        }
    }
}
