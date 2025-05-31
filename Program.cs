using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using FarmaciaApi.Data;
using FarmaciaApi.Services;

var builder = WebApplication.CreateBuilder(args);

// ▸ CORS
builder.Services.AddCors(opt =>
{
    opt.AddPolicy("Todo", b =>
        b.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

// ▸ Swagger & Controllers
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ▸ DB
builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// ▸ Servicios personalizados
builder.Services.AddScoped<EmailService>();
builder.Services.AddScoped<RabbitService>();

// ▸ JWT + Password
builder.Services.AddScoped<JwtService>();
builder.Services.AddScoped<PasswordService>();

var jwtCfg = builder.Configuration.GetSection("Jwt");
var key = Encoding.UTF8.GetBytes(jwtCfg["Key"]!);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtCfg["Issuer"],
            ValidAudience = jwtCfg["Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(key)
        };
    });

builder.Services.AddAuthorization();

var app = builder.Build();

// ▸ Middleware
app.UseSwagger();
app.UseSwaggerUI();

app.UseCors("Todo");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();