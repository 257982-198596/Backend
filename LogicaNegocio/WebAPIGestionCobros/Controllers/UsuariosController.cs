using LogicaNegocio.Dominio;
using LogicaNegocio.InterfacesRepositorios;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPIGestionCobros.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        public IRepositorioUsuarios RepoUsuarios { get; set; }

        public UsuariosController(IRepositorioUsuarios repoUsuarios)
        {
            RepoUsuarios = repoUsuarios;
        }

        // GET: api/usuarios/iniciarsesion
        [HttpPost]
        [Route("iniciarsesion")]
        public IActionResult IniciarSesion([FromBody] Usuario usuario)
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
                    return NotFound("Usuario o contraseña inválidos");
                }

            }
            else
            {
                return BadRequest();
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
    }
}
