using Microsoft.AspNetCore.Mvc;
using FarmaciaApi.Data;
using FarmaciaApi.Entities;

[ApiController]
[Route("productos")]
public class ProductosController : ControllerBase
{
    private readonly AppDbContext _context;
    public ProductosController(AppDbContext context) => _context = context;

    
    [HttpGet]
    public IActionResult Get() => Ok(_context.Productos.ToList());

    
    [HttpPost]
    public IActionResult Post(Producto p)
    {
        _context.Productos.Add(p);
        _context.SaveChanges();
        return Ok(p);
    }

    
    [HttpPut("{id}")]
    public IActionResult Put(int id, Producto p)
    {
        var prod = _context.Productos.Find(id);
        if (prod is null) return NotFound();

        prod.Nombre = p.Nombre;
        prod.Categoria = p.Categoria; 
        prod.Stock = p.Stock;
        prod.Precio = p.Precio;

        _context.SaveChanges();
        return Ok(prod);
    }

    
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var p = _context.Productos.Find(id);
        if (p is null) return NotFound();

        _context.Productos.Remove(p);
        _context.SaveChanges();
        return Ok();
    }
}
