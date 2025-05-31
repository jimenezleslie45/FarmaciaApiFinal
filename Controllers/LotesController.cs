using Microsoft.AspNetCore.Mvc;
using FarmaciaApi.Data;
using FarmaciaApi.Entities;

namespace FarmaciaApi.Controllers
{
    [ApiController]
    [Route("Lotes")]
    public class LotesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public LotesController(AppDbContext context)
        {
            _context = context;
        }

        // Obtener todos los lotes
        [HttpGet]
        public IActionResult Get()
        {
            var lotes = _context.Lotes.ToList();
            return Ok(lotes);
        }

        // Agregar un nuevo lote
        [HttpPost]
        public IActionResult Post([FromBody] Lote nuevo)
        {
            _context.Lotes.Add(nuevo);
            _context.SaveChanges();
            return Ok(new { mensaje = "Lote guardado exitosamente", lote = nuevo });
        }
    }
}
