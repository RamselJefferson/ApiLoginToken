using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PruebaTecnica.Models;
using System.Text.RegularExpressions;

namespace PruebaTecnica.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehiculosController : ControllerBase
    {
        private readonly ApiContext _context;

        public VehiculosController(ApiContext context)
        {
            _context = context;
        }


        [HttpGet]
        
        public IActionResult Vehiculos()
        {
            return Ok(_context.vwVehiculos.ToList());
        }

        [HttpGet("{id}")]

        public IActionResult Vehiculos(int id)
        {
            var vehiculo = _context.vwVehiculos.FirstOrDefault(e => e.VehId == id);

            return Ok(vehiculo);
        }






        [HttpDelete("{id}")]

        public IActionResult Delete(int id)
        {
            var vehiculo = _context.Vehiculos.FirstOrDefault(e => e.VehId == id);

            if(vehiculo != null)
            {
                _context.Vehiculos.Remove(vehiculo);
                _context.SaveChanges();
             }


            return Ok(vehiculo);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public IActionResult CrearVehiculo( [FromBody] Vehiculo model)
        {
            _context.Vehiculos.Add(model);
            _context.SaveChanges();

            return CreatedAtAction(nameof(Vehiculos), new { id = model.VehId }, model);
        }


        [HttpPut("{id}")]
        public IActionResult ActualizarVehiculo(int id,[FromBody] VehiculoUpdateDTO vehiculoUpdateDTO)
        {
            var vehiculoActualizar = _context.Vehiculos.FirstOrDefault(e =>e.VehId == id);

            if(vehiculoActualizar == null)
            {
                return NotFound();
            }

            vehiculoActualizar.VehDecripcion = vehiculoUpdateDTO.VehDecripcion;
            vehiculoActualizar.Año = vehiculoUpdateDTO.Año;
            vehiculoActualizar.Estatus = vehiculoUpdateDTO.Estatus;
            vehiculoActualizar.Precio = vehiculoUpdateDTO.Precio;

            _context.Vehiculos.Update(vehiculoActualizar);
            _context.SaveChanges();

            return NoContent();
            

        }

    }


}

