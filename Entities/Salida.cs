namespace FarmaciaApi.Entities
{
    public class Salida
    {
        public int Id { get; set; }
        public int ProductoId { get; set; }
        public int Cantidad { get; set; }
        public DateTime FechaSalida { get; set; }
    }
}
