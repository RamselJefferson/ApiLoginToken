using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PruebaTecnica.Interfaces;
using PruebaTecnica.Models;

namespace PruebaTecnica.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MarcasController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public MarcasController(IUnitOfWork unitOfWork)
        {

            _unitOfWork = unitOfWork;
       
        }

        [HttpGet]
        public IActionResult Marcas()
        {
            return Ok(_unitOfWork.IMarcasRepository.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult Modelos(int id)
        {
            var marca = _unitOfWork.IMarcasRepository.GetFirst(e => e.MarId == id);

            return Ok(marca);
        }

        [HttpDelete("{id}")]
        public IActionResult MarcasDelete(int id)
        {
            var marcaEliminar = _unitOfWork.IMarcasRepository.GetFirst(e => e.MarId == id);

            if(marcaEliminar != null) {
                _unitOfWork.IMarcasRepository.Delete(marcaEliminar);               
            }
            return Ok();
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public IActionResult CrearMarca(int marId, [FromBody] Marca model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Marca marca = new Marca()
            {
                //MarId = _context.Marcas.Max(e => e.MarId) + 1,

                MarDecripcion = model.MarDecripcion
            };
            if(marca.MarDecripcion !=null)
            {
                _unitOfWork.IMarcasRepository.Add(marca);
            }
            else
            {
                return BadRequest();
            }
            return CreatedAtAction(nameof(CrearMarca), marca);
        }


        [HttpPut("{id}")]
        public IActionResult ActualizarMarca(int id, [FromBody] MarcaDTO marcaDTO)
        {
            var marcaActualizar = _unitOfWork.IMarcasRepository.GetFirst(e => e.MarId == id);

            if (marcaActualizar == null)
            {
                return NotFound();
            }

            marcaActualizar.MarDecripcion = marcaDTO.MarDescripcion != null ? marcaDTO.MarDescripcion : marcaActualizar.MarDecripcion;

            _unitOfWork.IMarcasRepository.Update(marcaActualizar);

            return Ok();


        }
    }
}
