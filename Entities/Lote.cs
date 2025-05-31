namespace FarmaciaApi.Entities
{
    public class Lote
    {
        public int Id { get; set; }
        public string Medicamento { get; set; } = string.Empty;

        public int Cantidad { get; set; }
        public DateTime FechaVencimiento { get; set; }
    }

}


