
namespace FarmaciaApi.Services
{
    public class EmailService
    {
        public void EnviarCorreo(string to, string subject, string body)
        {
            Console.WriteLine($">>> Correo a {to}: {subject} - {body}");
        }
    }
}
