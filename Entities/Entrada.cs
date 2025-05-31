using System;

namespace FarmaciaApi.Entities
{
    public class Entrada

    {
        public int Id { get; set; }  // NO lo envías desde frontend, EF lo genera solo
        public string Producto { get; set; } = string.Empty;
        public int Cantidad { get; set; }
        public DateTime Fecha { get; set; }
        public decimal PrecioCompra { get; set; }
        public string Tipo { get; set; } = "entrada";
        public DateTime Timestamp { get; set; } = DateTime.Now;
    }
}
