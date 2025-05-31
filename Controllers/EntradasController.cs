using Microsoft.AspNetCore.Mvc;
using FarmaciaApi.Data;
using FarmaciaApi.Entities;

namespace FarmaciaApi.Controllers
{
    [ApiController]
    [Route("Entradas")]
    public class EntradasController : ControllerBase
    {
        private readonly AppDbContext _context;

        public EntradasController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var entradas = _context.Entradas.ToList();
            return Ok(entradas);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Entrada nueva)
        {
            nueva.Timestamp = DateTime.Now;
            _context.Entradas.Add(nueva);
            _context.SaveChanges();
            return Ok(new { mensaje = "Entrada registrada correctamente", entrada = nueva });
        }
    }
}
