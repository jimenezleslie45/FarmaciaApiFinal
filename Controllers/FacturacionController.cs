using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FarmaciaApi.Data;
using FarmaciaApi.Entities;

[ApiController]
[Route("facturacion")]
public class FacturacionController : ControllerBase
{
    private readonly AppDbContext _context;
    public FacturacionController(AppDbContext context) => _context = context;

    
    [HttpGet]
    public IActionResult Get()
    {
        var facturas = _context.Facturas
            .Include(f => f.Detalles) 
            .ToList();

        return Ok(facturas);
    }

    
    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var f = _context.Facturas
            .Include(f => f.Detalles)
            .FirstOrDefault(f => f.Id == id);

        if (f is null) return NotFound();
        return Ok(f);
    }

    
    [HttpPost]
    public IActionResult Post([FromBody] Factura f)
    {
        if (f == null || f.Detalles == null || !f.Detalles.Any())
            return BadRequest("Debe incluir al menos un producto en la factura.");

        _context.Facturas.Add(f);
        _context.SaveChanges();
        return Ok(f);
    }

    
    [HttpPut("{id}")]
    public IActionResult Put(int id, [FromBody] Factura updated)
    {
        var f = _context.Facturas
            .Include(f => f.Detalles)
            .FirstOrDefault(f => f.Id == id);

        if (f is null) return NotFound();

        f.Cliente = updated.Cliente;
        f.Total = updated.Total;
        f.Fecha = updated.Fecha;

        
        f.Detalles.Clear();
        foreach (var detalle in updated.Detalles)
        {
            f.Detalles.Add(detalle);
        }

        _context.SaveChanges();
        return Ok(f);
    }

    
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var f = _context.Facturas
            .Include(f => f.Detalles)
            .FirstOrDefault(f => f.Id == id);

        if (f is null) return NotFound();

        _context.Facturas.Remove(f);
        _context.SaveChanges();
        return Ok();
    }
}
