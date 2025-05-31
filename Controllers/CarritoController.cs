using Microsoft.AspNetCore.Mvc;
using FarmaciaApi.Data;
using FarmaciaApi.Entities;

namespace FarmaciaApi.Controllers
{
    [ApiController]
    [Route("Carrito")]
    public class CarritoController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CarritoController(AppDbContext context)
        {
            _context = context;
        }

        
        [HttpGet]
        public IActionResult Get()
        {
            var carrito = _context.Carrito.ToList();
            return Ok(carrito);
        }

        
        [HttpPost]
        public IActionResult Post([FromBody] Carrito nuevo)
        {
            _context.Carrito.Add(nuevo);
            _context.SaveChanges();
            return Ok("Producto agregado al carrito.");
        }
    }
}

