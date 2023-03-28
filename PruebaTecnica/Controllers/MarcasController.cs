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
        private readonly ApiContext _context;
        private readonly IMarcasRepository _IMarcasRepository;

        public MarcasController(
                                ApiContext context,
                                IMarcasRepository marcasRepository)
        {
            _context= context;
            _IMarcasRepository= marcasRepository;
        }

        [HttpGet]
        public IActionResult Marcas()
        {
            return Ok(_IMarcasRepository.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult Modelos(int id)
        {
            var marca = _IMarcasRepository.GetFirst(e => e.MarId == id);

            return Ok(marca);
        }

        [HttpDelete("{id}")]
        public IActionResult MarcasDelete(int id)
        {
            var marcaEliminar = _IMarcasRepository.GetFirst(e => e.MarId == id);

            if(marcaEliminar != null) {
                _IMarcasRepository.Delete(marcaEliminar);               
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
                MarId = _context.Marcas.Max(e => e.MarId) + 1,

                MarDecripcion = model.MarDecripcion
            };
            if(marca.MarDecripcion !=null)
            {
                _IMarcasRepository.Add(marca);
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
            var marcaActualizar = _IMarcasRepository.GetFirst(e => e.MarId == id);

            if (marcaActualizar == null)
            {
                return NotFound();
            }

            marcaActualizar.MarDecripcion = marcaDTO.MarDescripcion != null ? marcaDTO.MarDescripcion : marcaActualizar.MarDecripcion;

            _IMarcasRepository.Update(marcaActualizar);

            return Ok();


        }
    }
}
