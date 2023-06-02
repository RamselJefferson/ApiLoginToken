using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PruebaTecnica.Interfaces;
using PruebaTecnica.Models;
using PruebaTecnica.ViewModel;
using System.Security.Claims;
using System.Text.RegularExpressions;

namespace PruebaTecnica.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehiculosController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;


        public VehiculosController(IUnitOfWork unitOfWork){_unitOfWork = unitOfWork;}


        [HttpGet]     
        public IActionResult Vehiculos()
        {
            var vehiculos = _unitOfWork.IvwVehiculosRepository.GetAll();
            return Ok(vehiculos);
        }


        [HttpGet("{id}")]
        public IActionResult Vehiculos(int id)
        {
            var vehiculo = _unitOfWork.IvwVehiculosRepository.GetFirst(e => e.VehId == id);

            return Ok(vehiculo);
        }



        [HttpDelete("{id}")]
        public IActionResult Delete(int id, string token)
        {

            
            var rToken = JwtValidarToken.ValidarToken(token);

            if (!rToken.success) return rToken;

            Usuario usuario = rToken.result;

            if(usuario.Rol == 1)
            {
                var vehiculo = _unitOfWork.IVehiculosRepository.GetFirst(e => e.VehId == id);

                if (vehiculo != null)
                {
                    _unitOfWork.IVehiculosRepository.Delete(vehiculo);
                }


                return Ok();

            }

            return new JsonResult(new
            {
                success = false,
                message = "No tiene permisos para eliminar",
                result = ""
            });

          
        }

        [HttpPost]
        public IActionResult CrearVehiculo( [FromBody] Vehiculo model)
        {
            var maxVehId = _unitOfWork.IVehiculosRepository.ObtenerMaxIdVeh() ;

            
            model.VehId = maxVehId + 1;

            _unitOfWork.IVehiculosRepository.Add(model);
           

            return CreatedAtAction(nameof(Vehiculos), new { id = model.VehId }, model);
        }


        [HttpPut("{id}")]
        public IActionResult ActualizarVehiculo(int id,[FromBody] VehiculoUpdateDTO vehiculoUpdateDTO)
        {
            var vehiculoActualizar = _unitOfWork.IVehiculosRepository.GetFirst(e => e.VehId == id);

            if(vehiculoActualizar == null)
            {
                return NotFound();
            }

            vehiculoActualizar.VehDecripcion = vehiculoUpdateDTO.VehDecripcion != null ? vehiculoUpdateDTO.VehDecripcion : vehiculoActualizar.VehDecripcion;
            vehiculoActualizar.Año = vehiculoUpdateDTO.Año != null ? vehiculoUpdateDTO.Año : vehiculoActualizar.Año;
            vehiculoActualizar.Estatus = vehiculoUpdateDTO.Estatus != null ? vehiculoUpdateDTO.Estatus : vehiculoActualizar.Estatus;
            vehiculoActualizar.Precio = vehiculoUpdateDTO.Precio != null ? vehiculoUpdateDTO.Precio : vehiculoActualizar.Precio;

            _unitOfWork.IVehiculosRepository.Update(vehiculoActualizar);

            return Ok();
            

        }

    }


}

