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
using Microsoft.Extensions.Logging;
using Excepciones;

namespace LogicaAccesoDatos.BaseDatos
{
    public class RepositorioUsuarios : IRepositorioUsuarios
    {
        public CobrosContext Contexto { get; set; }

        private readonly EnviarCorreo SistemaEnviarCorreo;

        private readonly ILogger<RepositorioUsuarios> logAzure;

        public RepositorioUsuarios(CobrosContext context, EnviarCorreo sistemaCorreos, ILogger<RepositorioUsuarios> logger)
        {
            Contexto = context;
            SistemaEnviarCorreo = sistemaCorreos;
            logAzure = logger;
        }

        public Usuario IniciarSesion(string username, string password)
        {
            try
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
            catch (UsuarioException ex)
            {
                logAzure.LogError(ex.Message);
                throw;
            }
            catch (Exception e)
            {
                logAzure.LogError(e.Message);
                throw;
            }

        }

        public void ResetContrasena(Usuario usuario)
        {
            try
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
            catch (UsuarioException ex)
            {
                logAzure.LogError(ex.Message);
                throw;
            }
            catch (Exception e)
            {
                logAzure.LogError(e.Message);
                throw;
            }

        }

        public void HashExistingPasswords()
        {
            try
            {
                List<Usuario> usuarios = Contexto.Usuarios.ToList();
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
            catch (UsuarioException ex)
            {
                logAzure.LogError(ex.Message);
                throw;
            }
            catch (Exception e)
            {
                logAzure.LogError(e.Message);
                throw;
            }

        }
    }
}
