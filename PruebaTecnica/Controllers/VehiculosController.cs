using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PruebaTecnica.Interfaces;
using PruebaTecnica.Models;
using PruebaTecnica.ViewModel;
using System.Text.RegularExpressions;

namespace PruebaTecnica.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehiculosController : ControllerBase
    {
        private readonly ApiContext _context;
        private readonly IVehiculosRepository  _IVehiculosRepository;
        private readonly IvwVehiculosRepository _IvwVehiculosRepository;


        public VehiculosController(
            ApiContext context,
            IVehiculosRepository IVehiculosRepository,
            IvwVehiculosRepository IvwVehiculosRepository
            )
        {
            _context = context;
            _IVehiculosRepository = IVehiculosRepository;
            _IvwVehiculosRepository = IvwVehiculosRepository;
        }


        [HttpGet]     
        public IActionResult Vehiculos()
        {
            var vehiculos = _IvwVehiculosRepository.GetAll();
            return Ok(vehiculos);
        }

        [HttpGet("{id}")]
        public IActionResult Vehiculos(int id)
        {
            var vehiculo = _IvwVehiculosRepository.GetFirst(e => e.VehId == id);

            return Ok(vehiculo);
        }






        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var vehiculo = _IVehiculosRepository.GetFirst(e => e.VehId == id);

            if(vehiculo != null)
            {
                _IVehiculosRepository.Delete(vehiculo);
             }


            return Ok();
        }

        [HttpPost]
        public IActionResult CrearVehiculo( [FromBody] Vehiculo model)
        {
            _IVehiculosRepository.Add(model);
           

            return CreatedAtAction(nameof(Vehiculos), new { id = model.VehId }, model);
        }


        [HttpPut("{id}")]
        public IActionResult ActualizarVehiculo(int id,[FromBody] VehiculoUpdateDTO vehiculoUpdateDTO)
        {
            var vehiculoActualizar = _IVehiculosRepository.GetFirst(e => e.VehId == id);

            if(vehiculoActualizar == null)
            {
                return NotFound();
            }

            vehiculoActualizar.VehDecripcion = vehiculoUpdateDTO.VehDecripcion != null ? vehiculoUpdateDTO.VehDecripcion : vehiculoActualizar.VehDecripcion;
            vehiculoActualizar.Año = vehiculoUpdateDTO.Año != null ? vehiculoUpdateDTO.Año : vehiculoActualizar.Año;
            vehiculoActualizar.Estatus = vehiculoUpdateDTO.Estatus != null ? vehiculoUpdateDTO.Estatus : vehiculoActualizar.Estatus;
            vehiculoActualizar.Precio = vehiculoUpdateDTO.Precio != null ? vehiculoUpdateDTO.Precio : vehiculoActualizar.Precio;

            _IVehiculosRepository.Update(vehiculoActualizar);

            return Ok();
            

        }

    }


}

