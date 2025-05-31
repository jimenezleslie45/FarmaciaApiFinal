namespace FarmaciaApi.Entities
{
    public class Carrito
    {
        public int Id { get; set; }
        public int ProductoId { get; set; }
      public string Nombre { get; set; } = string.Empty;

        public decimal Precio { get; set; }
        public int Cantidad { get; set; }
        public decimal Total => Precio * Cantidad;
    }
}
