namespace FarmaciaApi.Entities
{
    public class HistorialAlerta
    {
        public int Id { get; set; }
        public string Mensaje { get; set; } = string.Empty;
        public DateTime Fecha { get; set; } = DateTime.Now;
        public bool EnviadoRabbit { get; set; }
        public bool EnviadoCorreo { get; set; }
    }
}
