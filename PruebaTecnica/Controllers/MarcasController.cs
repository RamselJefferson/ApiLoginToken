using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PruebaTecnica.Models;

namespace PruebaTecnica.Controllers
{
    [ApiController]
    [Route("api/marcas")]
    public class MarcasController : Controller
    {
        private readonly ApiContext _context;

        public MarcasController(ApiContext context)
        {
            _context= context;
        }


        [HttpGet("/Marcas")]

        public IActionResult Marcas()
        {
            return Ok(_context.Marcas.ToList());
        }


        [HttpGet("/Marcas/{id}")]

        public IActionResult Modelos(int id)
        {
            var marca = _context.Marcas.FirstOrDefault(e => e.MarId == id);

            return Ok(marca);
        }

        [HttpDelete("/Marcas/{id}")]
        public IActionResult MarcasDelete(int id)
        {
            var marcaEliminar = _context.Marcas.FirstOrDefault(e => e.MarId == id);

            if(marcaEliminar != null) {
                _context.Marcas.Remove(marcaEliminar);
                _context.SaveChanges();
            }

            return Ok(marcaEliminar);
        }

    }
}
