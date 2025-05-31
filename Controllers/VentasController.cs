using Microsoft.AspNetCore.Mvc;
using FarmaciaApi.Data;
using FarmaciaApi.Entities;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace FarmaciaApi.Controllers
{
    [ApiController]
    [Route("ventas")]
    [Authorize]
    public class VentasController : ControllerBase
    {
        private readonly AppDbContext _context;

        public VentasController(AppDbContext context)
        {
            _context = context;
        }

        
        [HttpGet]
        public ActionResult<IEnumerable<Venta>> Get()
        {
            var ventas = _context.Ventas.ToList();
            return Ok(ventas);
        }

        
        [HttpPost]
        public ActionResult<Venta> Post([FromBody] Venta v)
        {
            if (v == null || string.IsNullOrWhiteSpace(v.Producto) || v.Cantidad <= 0 || v.Total <= 0)
                return BadRequest("Datos inválidos para registrar la venta.");

            _context.Ventas.Add(v);
            _context.SaveChanges();

            return CreatedAtAction(nameof(Get), new { id = v.Id }, v);
        }
    }
}
