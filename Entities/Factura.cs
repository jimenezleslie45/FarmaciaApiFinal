namespace FarmaciaApi.Entities
{
    public class Factura
    {
        public int Id { get; set; }
        public string Cliente { get; set; } = "";
        public string Direccion { get; set; } = "";
        public string Lugar { get; set; } = "";
        public string Telefono { get; set; } = "";
        public string Nit { get; set; } = "";
        public DateTime Fecha { get; set; }
        public decimal Total { get; set; }

        public List<DetalleFactura> Detalles { get; set; } = new();
    }
}
