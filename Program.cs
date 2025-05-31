using Microsoft.EntityFrameworkCore;
using FarmaciaApi.Data;
using FarmaciaApi.Services;

var builder = WebApplication.CreateBuilder(args);

// ✔️ Configuración de CORS
builder.Services.AddCors(opt =>
{
    opt.AddPolicy("Todo", b =>
    {
        b.AllowAnyOrigin()
         .AllowAnyMethod()
         .AllowAnyHeader();
    });
});

// ✔️ Servicios
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ✔️ Conexión a SQL Server (usa DefaultConnection del appsettings.json)
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// ✔️ Servicios personalizados
builder.Services.AddScoped<EmailService>();
builder.Services.AddScoped<RabbitService>();

var app = builder.Build();

// ✔️ Middleware
app.UseSwagger();
app.UseSwaggerUI();

app.UseCors("Todo");
app.UseAuthorization();

app.MapControllers();

app.Run();
