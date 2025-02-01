using Excepciones;
using LogicaAccesoDatos.BaseDatos;
using LogicaNegocio.Dominio;
using LogicaNegocio.InterfacesRepositorios;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using WebAPIGestionCobros.Configuration;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPIGestionCobros.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        public IRepositorioUsuarios RepoUsuarios { get; set; }

        private readonly ILogger<RepositorioUsuarios> logAzure;

        private readonly string apiKeyConfig;

        public UsuariosController(IRepositorioUsuarios repoUsuarios, ILogger<RepositorioUsuarios> logger, IOptions<ApiSettings> apiSettings)
        {
            RepoUsuarios = repoUsuarios;
            logAzure = logger;
            apiKeyConfig = apiSettings.Value.ApiKey;
        }

        private bool EsApiKeyValida()
        {
            if (!Request.Headers.TryGetValue("ApiKey", out var apiKeyHeader))
            {
                return false;
            }

            return apiKeyHeader == apiKeyConfig;
        }

        // GET: api/usuarios/iniciarsesion
        [HttpPost]
        [Route("iniciarsesion")]
        public IActionResult IniciarSesion([FromBody] Usuario usuario)
        {
            if (!EsApiKeyValida())
            {
                return Unauthorized("API Key inválida o no proporcionada.");
            }

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
            if (!EsApiKeyValida())
            {
                return Unauthorized("API Key inválida o no proporcionada.");
            }
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
                return BadRequest(ex);
            }
        }

        [HttpPost]
        [Route("hashpasswords")]
        public IActionResult HashExistingPasswords()
        {
            if (!EsApiKeyValida())
            {
                return Unauthorized("API Key inválida o no proporcionada.");
            }
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
                return BadRequest(ex);
            }
        }

        [HttpGet]
        [Route("obtener-cliente-por-email")]
        public IActionResult ObtenerClientePorEmail([FromQuery] string email)
        {
            if (!EsApiKeyValida())
            {
                return Unauthorized("API Key inválida o no proporcionada.");
            }

            try
            {
                if (string.IsNullOrWhiteSpace(email))
                {
                    throw new UsuarioException("El email es obligatorio.");
                }

                Cliente cliente = RepoUsuarios.ObtenerClientePorEmail(email);

                if (cliente != null)
                {
                    return Ok(cliente);
                }
                else
                {
                    throw new UsuarioException("Cliente no encontrado.");
                }
            }
            catch (UsuarioException ex)
            {
                logAzure.LogError(ex.Message);
                return BadRequest(ex);
            }
            catch (Exception ex)
            {
                logAzure.LogError(ex.Message);
                return BadRequest(ex);
            }
        }
    }
}
