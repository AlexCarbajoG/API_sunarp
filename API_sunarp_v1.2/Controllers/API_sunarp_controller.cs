using API_sunarp.Data.Dependencias;
using API_sunarp.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_sunarp_v1._2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class API_sunarp_controller : ControllerBase
    {
        private readonly API_sunarp_repository _sunarpRepository;

        public API_sunarp_controller(API_sunarp_repository sunarpRepository)
        {
            _sunarpRepository = sunarpRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllDatos()
        {
            return Ok(await _sunarpRepository.GetAllDatos());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDetails(int id)
        {
            return Ok(await _sunarpRepository.GetDetails(id));
        }

        [HttpPost] 
        public async Task<IActionResult> CreateDatos([FromBody] Datos_sunarp datos_Sunarp)
        {
            if (datos_Sunarp == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var created = await _sunarpRepository.InsertDatos(datos_Sunarp);

            return Created("created", created);
                     
        }

        [HttpPut]
        public async Task<IActionResult> UpdateDatos([FromBody] Datos_sunarp datos_Sunarp)
        {
            if (datos_Sunarp == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _sunarpRepository.UpdateDatos(datos_Sunarp);

            return NoContent();

        }

        [HttpDelete]
        public async Task<IActionResult> DeleteDatos(int id)
        {
            await _sunarpRepository.DeleteDatos(new Datos_sunarp { Id = id });

            return NoContent();
        }



        [HttpGet("api/vehicles/{plate}")]
        public async Task<ActionResult<Datos_sunarp>> GetVehicleByPlate(string plate)
        {
            var vehicle = await _sunarpRepository.GetVehicleByPlate(plate);
            if (vehicle == null)
            {
                return NotFound();
            }
            return Ok(vehicle);
        }

        


    }
}
