using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PruebaTecnica.Models;

namespace PruebaTecnica.Controllers
{
    [Route("api/marcas/{marId}/[controller]")]
    [ApiController]
    public class ModelosController : ControllerBase
    {
        private readonly ApiContext _context;
        public ModelosController(ApiContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Modelos(int marId)
        {
            var modelos = _context.vwModelos.Where(e => e.MarId == marId).ToList();

            if (modelos == null) {
                return NotFound();
            }

            return Ok(modelos);

        }

        [HttpGet("{modId}")]
        public IActionResult Modelos(int marId, int modId)
        {
            var modelos = _context.vwModelos.FirstOrDefault(e => e.MarId == marId && e.ModId == modId);

            if (modelos == null)
            {
                return NotFound();
            }

            return Ok(modelos);
        }


        [HttpDelete("{modId}")]
        public IActionResult BorrarModelo(int marId, int modId)
        {
            var modelos = _context.Modelos.FirstOrDefault(e => e.MarId == marId && e.ModId == modId);

            if (modelos == null)
            {
                return NotFound();
            }

            _context.Modelos.Remove(modelos);
            _context.SaveChanges();

            return Ok(modelos);

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

            _context.Modelos.Add(modelo);
            _context.SaveChanges();

            
            return CreatedAtAction(nameof(CrearModelo), modelo);


        }

    }
}
