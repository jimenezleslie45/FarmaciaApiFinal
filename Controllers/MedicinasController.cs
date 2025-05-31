using Microsoft.AspNetCore.Mvc;
using FarmaciaApi.Data;
using FarmaciaApi.Entities;
using Microsoft.AspNetCore.Authorization;

[ApiController]
[Route("Medicinas")]
[Authorize]
public class MedicinasController : ControllerBase
{
    private readonly AppDbContext _context;

    public MedicinasController(AppDbContext context)
    {
        _context = context;
    }

    
    [HttpGet]
    public IActionResult Get()
    {
        var medicinas = _context.Medicinas.ToList();
        return Ok(medicinas);
    }

    
    [HttpPost]
    public IActionResult Post([FromBody] Medicina nueva)
    {
        if (nueva == null)
            return BadRequest("Datos inválidos.");

        _context.Medicinas.Add(nueva);
        _context.SaveChanges();
        return Ok("Medicina registrada con éxito.");
    }
}
