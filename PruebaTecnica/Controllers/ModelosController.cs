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
        private readonly IUnitOfWork _unitOfWork;

        public ModelosController( IUnitOfWork unitOfWork )
        {
            _unitOfWork = unitOfWork;
        }


        [HttpGet]
        public IActionResult Modelos(int marId)
        {
            var modelos = _unitOfWork.IvwModelosRepository.Where(e => e.MarId == marId);

            if (modelos == null) {
                return NotFound();
            }

            return Ok(modelos);

        }

        [HttpGet("{modId}")]
        public IActionResult Modelos(int marId, int modId)
        {
            var modelos = _unitOfWork.IvwModelosRepository.GetFirst(e => e.MarId == marId && e.ModId == modId);

            if (modelos == null)
            {
                return NotFound();
            }

            return Ok(modelos);
        }


        [HttpDelete("{modId}")]
        public IActionResult BorrarModelo(int marId, int modId)
        {
            var modelos = _unitOfWork.IModelosRepository.GetFirst(e => e.MarId == marId && e.ModId == modId);

            if (modelos == null)
            {
                return NotFound();
            }

            _unitOfWork.IModelosRepository.Delete(modelos);
            

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
                //ModId = _context.Modelos.Max(e => e.ModId) + 1,
                ModDescripcion = modeloDTO.ModDescripcion
            };

            _unitOfWork.IModelosRepository.Add(modelo);
           
            
            return CreatedAtAction(nameof(CrearModelo), modelo);


        }
        [HttpPut]
        public IActionResult ActualizarModelo(int id, [FromBody] ModeloDTO modeloDTO)
        {
            var modeloActualizar = _unitOfWork.IModelosRepository.GetFirst(e => e.ModId == id);

            if (modeloActualizar == null)
            {
                return NotFound();
            }

            modeloActualizar.ModDescripcion = modeloDTO.ModDescripcion != null ? modeloDTO.ModDescripcion : modeloActualizar.ModDescripcion;

            _unitOfWork.IModelosRepository.Update(modeloActualizar);

            return Ok();


        }

    }
}
