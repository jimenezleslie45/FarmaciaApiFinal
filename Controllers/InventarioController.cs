using Microsoft.AspNetCore.Mvc;
using FarmaciaApi.Data;
using FarmaciaApi.Entities;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

[ApiController]
[Route("Inventario")]
[Authorize]
public class InventarioController : ControllerBase
{
    private readonly AppDbContext _context;

    public InventarioController(AppDbContext context)
    {
        _context = context;
    }

    // ✔️ Obtener todos los registros del inventario
    [HttpGet]
    public IActionResult Get()
    {
        var inventario = _context.Inventario.ToList();
        return Ok(inventario);
    }

    // ✔️ Agregar un nuevo registro al inventario
    [HttpPost]
    public IActionResult Post([FromBody] Inventario nuevo)
    {
        _context.Inventario.Add(nuevo);
        _context.SaveChanges();
        return Ok("Producto agregado al inventario.");
    }
}
