using Excepciones;
using LogicaAccesoDatos.BaseDatos;
using LogicaNegocio.Dominio;
using LogicaNegocio.InterfacesRepositorios;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPIGestionCobros.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        public IRepositorioUsuarios RepoUsuarios { get; set; }

        private readonly ILogger<RepositorioUsuarios> logAzure;

        public UsuariosController(IRepositorioUsuarios repoUsuarios, ILogger<RepositorioUsuarios> logger)
        {
            RepoUsuarios = repoUsuarios;
            logAzure = logger;
        }

        // GET: api/usuarios/iniciarsesion
        [HttpPost]
        [Route("iniciarsesion")]
        public IActionResult IniciarSesion([FromBody] Usuario usuario)
        {
            try
            {
                if (usuario.Email != null && usuario.Password != null)
                {
                    Usuario elusuario = RepoUsuarios.IniciarSesion(usuario.Email, usuario.Password);

                    if (elusuario != null)
                    {
                        return Ok(elusuario);

                    }
                    else
                    {
                        throw new UsuarioException("Usuario o contraseña incorrectos");
                    }

                }
                else
                {
                    throw new UsuarioException("Usuario y contraseña so obligatorios");
                }
            }catch(UsuarioException ex)
            {
                logAzure.LogError(ex.Message);
                return BadRequest(ex);
            }
            catch(Exception ex)
            {
                logAzure.LogError(ex.Message);
                return BadRequest(ex);
            }

        }

        // GET api/<UsuariosController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<UsuariosController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<UsuariosController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<UsuariosController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        [HttpPost]
        [Route("reset")]
        public IActionResult ResetContrasena([FromBody] Usuario usuario)
        {
            try
            {
                RepoUsuarios.ResetContrasena(usuario);
                return Ok("Contraseña temporal generada y enviada por correo.");
            }
            catch (UsuarioException ex)
            {
                logAzure.LogError(ex.Message);
                return BadRequest(ex);
            }
            catch (Exception ex)
            {
                logAzure.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("hashpasswords")]
        public IActionResult HashExistingPasswords()
        {
            try
            {
                RepoUsuarios.HashExistingPasswords();
                return Ok("Contraseñas hasheadas correctamente.");
            }
            catch (UsuarioException ex)
            {
                logAzure.LogError(ex.Message);
                return BadRequest(ex);
            }
            catch (Exception ex)
            {
                logAzure.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}
