using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using API_sunarp.Data.Repositories;
using API_sunarp.Model;
using API_sunarp.Data.Dependencias;

namespace API_sunarp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpleadoController : ControllerBase
    {
        private readonly API_empleadoRepository _empleadoRepository;

        public EmpleadoController(API_empleadoRepository empleadoRepository)
        {
            _empleadoRepository = empleadoRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEmpleados()
        {
            var empleados = await _empleadoRepository.GetAllDatos();
            return Ok(empleados);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmpleado(int id)
        {
            var empleado = await _empleadoRepository.GetDetails(id);

            if (empleado == null)
            {
                return NotFound();
            }

            return Ok(empleado);
        }

        [HttpPost]
        public async Task<IActionResult> CreateEmpleado([FromBody] Empleado empleado)
        {
            if (empleado == null)
            {
                return BadRequest();
            }

            var created = await _empleadoRepository.InsertDatos(empleado);

            if (created)
            {
                return CreatedAtAction(nameof(GetEmpleado), new { id = empleado.id }, empleado);
            }
            else
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmpleado(int id, [FromBody] Empleado empleado)
        {
            if (empleado == null || empleado.id != id)
            {
                return BadRequest();
            }

            var existingEmpleado = await _empleadoRepository.GetDetails(id);
            if (existingEmpleado == null)
            {
                return NotFound();
            }

            var updated = await _empleadoRepository.UpdateDatos(empleado);
            if (updated)
            {
                return NoContent();
            }
            else
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmpleado(int id)
        {
            var empleado = await _empleadoRepository.GetDetails(id);
            if (empleado == null)
            {
                return NotFound();
            }

            var deleted = await _empleadoRepository.DeleteDatos(empleado);
            if (deleted)
            {
                return NoContent();
            }
            else
            {
                return StatusCode(500, "Internal server error");
            }
        }




        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] Empleado model)
        {
            if (model == null || string.IsNullOrEmpty(model.nombre) || string.IsNullOrEmpty(model.clave))
            {
                return BadRequest("Invalid login request.");
            }

            var empleado = await _empleadoRepository.GetEmpleadoByCredentials(model.nombre, model.clave);

            if (empleado == null)
            {
                return Unauthorized("Invalid credentials.");
            }

            return Ok(new { empleado.id, empleado.nombre });
        }

        // Incluir en API_sunarp_controller
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] Empleado empleado)
        {
            if (empleado == null || string.IsNullOrEmpty(empleado.nombre) || string.IsNullOrEmpty(empleado.clave))
            {
                return BadRequest("Nombre y clave son requeridos.");
            }

            var authenticatedEmpleado = await _empleadoRepository.GetEmpleadoByCredentials(empleado.nombre, empleado.clave);

            if (authenticatedEmpleado == null)
            {
                return Unauthorized(); // Retorna un 401 si las credenciales son incorrectas
            }

            return Ok(authenticatedEmpleado); // Devuelve el empleado autenticado
        }


    }
}
