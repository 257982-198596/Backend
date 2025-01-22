using LogicaNegocio.Dominio;
using LogicaNegocio.InterfacesRepositorios;
using Microsoft.EntityFrameworkCore;
using SistemaDeNotificaciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BCrypt.Net;

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
                    if (item.Email == username && BCrypt.Net.BCrypt.Verify(password, item.Password))
                    {
                        usuarioIniciado = item;
                        break;
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
            elUsuario.Password = BCrypt.Net.BCrypt.HashPassword(contrasenaTemporal); 
            Contexto.Usuarios.Update(elUsuario);
            Contexto.SaveChanges();

            SistemaEnviarCorreo.EnviarContrasenaTemporal(new Cliente { UsuarioLogin = elUsuario }, contrasenaTemporal);
        }

        public void HashExistingPasswords()
        {
            var usuarios = Contexto.Usuarios.ToList();
            String pass = "";
            foreach (var usuario in usuarios)
            {
                pass = usuario.Password;
                // Verificar si la contraseña ya está hasheada
                if (!usuario.Password.StartsWith("$2a$") && !usuario.Password.StartsWith("$2b$") && !usuario.Password.StartsWith("$2y$"))
                {
                    // Si no está hasheada, hashearla
                    usuario.Password = BCrypt.Net.BCrypt.HashPassword(usuario.Password);
                }
            }

            Contexto.SaveChanges();
        }
    }
}
