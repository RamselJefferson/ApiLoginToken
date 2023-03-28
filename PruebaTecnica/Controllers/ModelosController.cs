using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PruebaTecnica.Interfaces;
using PruebaTecnica.Models;
using PruebaTecnica.Repositories;

namespace PruebaTecnica.Controllers
{
    [Route("api/marcas/{marId}/[controller]")]
    [ApiController]
    public class ModelosController : ControllerBase
    {
        private readonly ApiContext _context;
        private readonly IModelosRepository _modelosRepository;
        private readonly IvwModelosRepository _IvwModelosRepository;
        public ModelosController(
            ApiContext context,
            IModelosRepository IModelosRepository,
            IvwModelosRepository IvwModelosRepository
            )
        {
            _context = context;
            _modelosRepository = IModelosRepository;
            _IvwModelosRepository = IvwModelosRepository;
        }

        [HttpGet]
        public IActionResult Modelos(int marId)
        {
            var modelos = _IvwModelosRepository.Where(e => e.MarId == marId);

            if (modelos == null) {
                return NotFound();
            }

            return Ok(modelos);

        }

        [HttpGet("{modId}")]
        public IActionResult Modelos(int marId, int modId)
        {
            var modelos = _IvwModelosRepository.GetFirst(e => e.MarId == marId && e.ModId == modId);

            if (modelos == null)
            {
                return NotFound();
            }

            return Ok(modelos);
        }


        [HttpDelete("{modId}")]
        public IActionResult BorrarModelo(int marId, int modId)
        {
            var modelos = _modelosRepository.GetFirst(e => e.MarId == marId && e.ModId == modId);

            if (modelos == null)
            {
                return NotFound();
            }

            _modelosRepository.Delete(modelos);
            

            return Ok();

        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public IActionResult CrearModelo(int marId,[FromBody] ModeloDTO modeloDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Modelo modelo = new Modelo()
            {
                MarId = marId,
                ModId = _context.Modelos.Max(e => e.ModId) + 1,
                ModDescripcion = modeloDTO.ModDescripcion
            };

            _modelosRepository.Add(modelo);
           
            
            return CreatedAtAction(nameof(CrearModelo), modelo);


        }
        [HttpPut]
        public IActionResult ActualizarModelo(int id, [FromBody] ModeloDTO modeloDTO)
        {
            var modeloActualizar = _modelosRepository.GetFirst(e => e.ModId == id);

            if (modeloActualizar == null)
            {
                return NotFound();
            }

            modeloActualizar.ModDescripcion = modeloDTO.ModDescripcion != null ? modeloDTO.ModDescripcion : modeloActualizar.ModDescripcion;
           
            _modelosRepository.Update(modeloActualizar);

            return Ok();


        }

    }
}
