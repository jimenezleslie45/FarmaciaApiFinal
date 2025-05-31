namespace FarmaciaApi.Entities
{
    public class Venta
    {
        public int Id { get; set; }
        public string Producto { get; set; } = "";
        public int Cantidad { get; set; }
        public decimal Total { get; set; }
    }
}
