using Microsoft.EntityFrameworkCore;
using FarmaciaApi.Entities;

namespace FarmaciaApi.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Producto> Productos => Set<Producto>();
    public DbSet<Factura> Facturas => Set<Factura>();
    public DbSet<DetalleFactura> DetalleFacturas => Set<DetalleFactura>();
    public DbSet<Venta> Ventas => Set<Venta>();
    public DbSet<HistorialAlerta> HistorialAlertas => Set<HistorialAlerta>();
    public DbSet<Inventario> Inventario => Set<Inventario>();
    public DbSet<Carrito> Carrito => Set<Carrito>();
    public DbSet<Entrada> Entradas => Set<Entrada>();
    public DbSet<Salida> Salidas => Set<Salida>();
    public DbSet<Medicina> Medicinas => Set<Medicina>();
    public DbSet<Lote> Lotes => Set<Lote>();

    public DbSet<Usuario> Usuarios => Set<Usuario>();
}