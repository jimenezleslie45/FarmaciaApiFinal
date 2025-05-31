using System;
using System.Text;
using RabbitMQ.Client;

namespace FarmaciaApi.Services
{
    public class RabbitService
    {
        private readonly ConnectionFactory _factory;

        // Constructor correcto
        public RabbitService()
        {
            _factory = new ConnectionFactory()
            {
                HostName = "localhost",
                Port = 5672,
                UserName = "guest",
                Password = "guest"
            };
        }

        public void EnviarMensaje(string mensaje)
        {
            using var connection = _factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.QueueDeclare(
                queue: "alertas",
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null
            );

            var body = Encoding.UTF8.GetBytes(mensaje);
            channel.BasicPublish(
                exchange: "",
                routingKey: "alertas",
                basicProperties: null,
                body: body
            );

            Console.WriteLine($">>> Enviado a RabbitMQ: {mensaje}");
        }
    }
}
