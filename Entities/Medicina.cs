namespace FarmaciaApi.Entities
{
    public class Medicina
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Categoria { get; set; } = string.Empty;
    public int Stock { get; set; }
    public decimal Precio { get; set; }
}

}
