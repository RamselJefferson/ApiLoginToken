using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PruebaTecnica.Interfaces;
using PruebaTecnica.Models;
using PruebaTecnica.ViewModel;
using System.Net;
using System.Security.Claims;
using System.Text.RegularExpressions;

namespace PruebaTecnica.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehiculosController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;
        private readonly ApiContext _context;


        public VehiculosController(IUnitOfWork unitOfWork, IConfiguration configuration, ApiContext context)
        {
            _unitOfWork = unitOfWork; _configuration = configuration;
            _context = context;
            
        }


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


       
        [HttpPost("{id}")]
        public IActionResult Delete(int id)
        {
            var encryptedToken = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            var rToken = new LoginController(_context,_configuration).ValidarToken(encryptedToken);
            var vehiculo = _unitOfWork.IVehiculosRepository.GetFirst(e => e.VehId == id);


            if (vehiculo != null )
            {
                if(rToken.result.Rol == 1)
                {
                    _unitOfWork.IVehiculosRepository.Delete(vehiculo);
                    return Ok();
                }
                else
                {                   
                    return StatusCode((int)HttpStatusCode.Forbidden, "No tiene Permisos"); ;
                }
            }

            return NotFound();

        }

        [HttpPost]
        public IActionResult CrearVehiculo( [FromBody] VehiculosCreate model)
        {
            var maxVehId = _unitOfWork.IVehiculosRepository.ObtenerMaxIdVeh() +1 ;


            Vehiculo vehiculo = new Vehiculo
            {
                VehId = maxVehId,
                ModId = model.ModId,
                MarId = model.MarId,
                VehDecripcion = model.VehDecripcion,
                Estatus = model.Estatus,
                Precio = model.Precio
            };

            _unitOfWork.IVehiculosRepository.Add(vehiculo);
           

            return CreatedAtAction(nameof(Vehiculos), new { id = maxVehId }, vehiculo);
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

