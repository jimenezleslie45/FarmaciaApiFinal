namespace FarmaciaApi.Entities
{
    public class Producto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = "";
         public string Categoria { get; set; } = "";
        public int Stock { get; set; }
        public decimal Precio { get; set; }
    }
}