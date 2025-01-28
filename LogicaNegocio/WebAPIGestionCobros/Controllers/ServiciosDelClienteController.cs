using Excepciones;
using LogicaNegocio.Dominio;
using LogicaNegocio.InterfacesRepositorios;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPIGestionCobros.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiciosDelClienteController : ControllerBase
    {
        public IRepositorioServiciosDelCliente RepoServiciosDelCliente { get; set; }

        public ServiciosDelClienteController(IRepositorioServiciosDelCliente repoServiciosDelClientes)
        {
            RepoServiciosDelCliente = repoServiciosDelClientes;
        }

        // GET: api/<ServiciosDelClienteController>
        [HttpGet("{idCliente}")]
        public IActionResult GetServiciosDeUnCliente(int idCliente)
        {
            try
            {

                IEnumerable<ServicioDelCliente> losServiciosdelCliente = RepoServiciosDelCliente.ServiciosDeUnCliente(idCliente);
                return Ok(losServiciosdelCliente);
                
               
            }
            catch (ServicioDelClienteException ex)
            {
                return BadRequest(ex);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
            
        }

        [HttpGet("servicio/{idServicioDelCliente}")]
        public IActionResult GetServicioDeClientePorId(int idServicioDelCliente)
        {
            if (idServicioDelCliente == 0)
            {
                return BadRequest("El ID del servicio de cliente es inválido.");
            }
            try
            {
                ServicioDelCliente elServicioDeCliente = RepoServiciosDelCliente.FindById(idServicioDelCliente);
                if (elServicioDeCliente == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(elServicioDeCliente);
                }
            }
            catch (ServicioDelClienteException e)
            {
                return BadRequest(e);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // GET: api/<ServiciosDelClienteController>
        [HttpGet("activos/{idCliente}")]
        public IActionResult GetServiciosActivosDeUnCliente(int idCliente)
        {
            try
            {
                if (idCliente != null || idCliente != 0)
                {
                    IEnumerable<ServicioDelCliente> losServiciosdelCliente = RepoServiciosDelCliente.ServiciosActivosDeUnCliente(idCliente);
                    return Ok(losServiciosdelCliente);
                }
                else
                {
                    return BadRequest("El ID del cliente es inválido.");
                }
                


            }
            catch (ServicioDelClienteException ex)
            {
                return BadRequest(ex);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }

        }

        // GET: api/<ServiciosDelClienteController>
        [HttpGet("pagos/{idCliente}")]
        public IActionResult GetServiciosPagosDeUnCliente(int idCliente)
        {
            try
            {
                if (idCliente != null || idCliente != 0)
                {
                    IEnumerable<ServicioDelCliente> losServiciosdelCliente = RepoServiciosDelCliente.ServiciosPagosDeUnCliente(idCliente);
                    return Ok(losServiciosdelCliente);
                }
                else
                {
                    return BadRequest("El ID del cliente es inválido.");
                }



            }
            catch (ServicioDelClienteException ex)
            {
                return BadRequest(ex);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }

        }

        // GET api/<ServiciosDelClienteController>/5
        [HttpGet("{idCliente}/{idServicioDelCliente}")]
        public IActionResult Get(int idCliente, int idServicioDelCliente)
        {
            if (idCliente == null || idCliente == 0 || idServicioDelCliente == null || idServicioDelCliente == 0)
            {
                return BadRequest();
            }
            try
            {
                ServicioDelCliente elServiciodeCliente = RepoServiciosDelCliente.FindById(idServicioDelCliente);
                if (elServiciodeCliente == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(elServiciodeCliente);
                }
            }
            catch (ServicioDelClienteException e)
            {
                return BadRequest(e);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // POST api/<ServiciosDelClienteController>
        [HttpPost]
        public IActionResult Post([FromBody] ServicioDelCliente nuevo)
        {
            try
            {
                nuevo.Validar();
                RepoServiciosDelCliente.Add(nuevo);
            }
            catch (ServicioDelClienteException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

            return Created("api/serviciosdelcliente" + nuevo.Id, nuevo);

        }

        // PUT api/<ServiciosDelClienteController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] ServicioDelCliente aModificar)
        {
            try
            {
                if (id != null || id != 0)
                {
                    aModificar.Id = id;
                    aModificar.Validar();
                    RepoServiciosDelCliente.Update(aModificar);
                    return Ok(aModificar);
                }
                else
                {
                    return BadRequest("El id del servicio de cliente es obligatorio");
                }
            }
            catch (ServicioDelClienteException ex)
            {
                return BadRequest(ex);
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
        }

        // DELETE api/<ServiciosDelClienteController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                if (id != null && id != 0)
                {
                    ServicioDelCliente elServicioDeClienteAEliminar = RepoServiciosDelCliente.FindById(id);
                    if (elServicioDeClienteAEliminar != null)
                    {
                        RepoServiciosDelCliente.Remove(id);
                        return NoContent();
                    }
                    else
                    {
                        return BadRequest("No existe servicio de cliente con ese ID");
                    }

                }
                else
                {
                    return BadRequest("El id es obligatorio para eliminar un servicio de cliente");
                }
            }
            catch (ServicioDelClienteException ex)
            {
                return BadRequest(ex);
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
        }

        [HttpGet("proximo-vencimiento/{idCliente}")]
        public IActionResult ObtenerProximoServicioActivoAVencerse(int idCliente)
        {
            try
            {
                ServicioDelCliente proximoServicio = RepoServiciosDelCliente.ObtenerProximoServicioActivoAVencerse(idCliente);
                if (proximoServicio == null)
                {
                    return NotFound(new { Mensaje = "No se encontraron servicios próximos a vencerse para el cliente." });
                }
                return Ok(new { ClienteId = idCliente, Servicio = proximoServicio, FechaVencimiento = proximoServicio.FechaVencimiento });
            }
            catch (ServicioDelClienteException ex)
            {
                return BadRequest(ex);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Mensaje = "Error interno del servidor.", Error = ex.Message });
            }
        }


        [HttpGet("ingresos-proximos-365-dias/{idCliente}")]
        public IActionResult CalcularIngresosProximos365Dias(int idCliente)
        {
            try
            {
                decimal totalIngresos = RepoServiciosDelCliente.CalcularIngresosProximos365Dias(idCliente);
                return Ok(new { ClienteId = idCliente, TotalIngresos = totalIngresos });
            }
            catch (ServicioDelClienteException ex)
            {
                return BadRequest(ex);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Mensaje = "Error interno del servidor.", Error = ex.Message });
            }
        }

        //no es solo servicios activos - a corregir
        [HttpGet("activos-suscriptor/{idSuscriptor}")]
        public IActionResult GetServiciosDeClientesDeUnSuscriptor(int idSuscriptor)
        {
            try
            {
                IEnumerable<ServicioDelCliente> servicios = RepoServiciosDelCliente.ServiciosDeClientesDeUnSuscriptor(idSuscriptor);
                return Ok(servicios);
            }
            catch (ServicioDelClienteException ex)
            {
                return BadRequest(new { Mensaje = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Mensaje = "Error interno del servidor.", Error = ex.Message });
            }
        }

        [HttpGet("activos-suscriptor-vencen-este-mes/{idSuscriptor}")]
        public IActionResult GetServiciosActivosDeClientesDeUnSuscriptorQueVencenEsteMes(int idSuscriptor)
        {
            try
            {
                var serviciosQueVencenEsteMes = RepoServiciosDelCliente.ServiciosDeClientesDeUnSuscriptorQueVencenEsteMes(idSuscriptor);
                return Ok(serviciosQueVencenEsteMes);
            }
            catch (ServicioDelClienteException ex)
            {
                return BadRequest(new { Mensaje = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Mensaje = "Error interno del servidor.", Error = ex.Message });
            }
        }

        // INDICADORES REPORTE VENCIMIENTOS DEL MES CORRIENTE
        [HttpGet("indicadores-vencimientos-mes/{idSuscriptor}")]
        public IActionResult GetIndicadoresServiciosVencenEsteMes(int idSuscriptor)
        {
            try
            {
                Dictionary<string, decimal> indicadores = RepoServiciosDelCliente.ObtenerIndicadoresServiciosVencenEsteMes(idSuscriptor);
                return Ok(indicadores);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost("cambiar-estado-servicios-del-cliente-a-vencidos")]
        public IActionResult MarcarServiciosComoVencidos()
        {
            try
            {
                IEnumerable<ServicioDelCliente> serviciosVencidos = RepoServiciosDelCliente.MarcarServiciosComoVencidos();
                return Ok(serviciosVencidos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al marcar servicios como vencidos: {ex.Message}");
            }
        }


    }
}
