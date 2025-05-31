using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FarmaciaApi.Data;
using FarmaciaApi.Dtos;
using FarmaciaApi.Entities;
using FarmaciaApi.Services;

namespace FarmaciaApi.Controllers;

[ApiController]
[Route("auth")]
public class AuthController : ControllerBase
{
    private readonly AppDbContext _db;
    private readonly PasswordService _pwdSvc;
    private readonly JwtService _jwtSvc;

    public AuthController(AppDbContext db, PasswordService pwdSvc, JwtService jwtSvc)
    {
        _db = db;
        _pwdSvc = pwdSvc;
        _jwtSvc = jwtSvc;
    }

    // POST /auth/register
    [HttpPost("register")]
    public async Task<IActionResult> Register(UserRegisterDto dto)
    {
        if (await _db.Usuarios.AnyAsync(u => u.Email == dto.Email))
            return Conflict("El correo ya está registrado.");

        var user = new Usuario
        {
            Nombre = dto.Nombre,
            Email = dto.Email,
            PasswordHash = _pwdSvc.Hash(dto.Password)
        };

        _db.Usuarios.Add(user);
        await _db.SaveChangesAsync();

        return StatusCode(201);
    }

    // POST /auth/login
    [HttpPost("login")]
    public async Task<IActionResult> Login(UserLoginDto dto)
    {
        var user = await _db.Usuarios.FirstOrDefaultAsync(u => u.Email == dto.Email);
        if (user is null || !_pwdSvc.Verify(user.PasswordHash, dto.Password))
            return Unauthorized("Credenciales inválidas.");

        var token = _jwtSvc.GenerateToken(user);
        return Ok(new { token });
    }
}