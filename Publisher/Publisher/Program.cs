using RabbitMQ.Client;
using System;
using System.Text;
using System.Text.Json;

namespace Publisher
{
    class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.QueueDeclare(queue: "protocolos", durable: false, exclusive: false, autoDelete: false, arguments: null);

            var protocolos = new[]
            {
                new { NumeroProtocolo = "001", NumeroVia = 1, Cpf = "12345678901", Rg = "12345678", Nome = "João Silva", NomeMae = "Maria Silva", NomePai = "José Silva", Foto = "foto1.jpg" },
                new { NumeroProtocolo = "002", NumeroVia = 1, Cpf = "23456789012", Rg = "23456789", Nome = "Ana Costa", NomeMae = "Elena Costa", NomePai = "Carlos Costa", Foto = "foto2.jpg" },
                new { NumeroProtocolo = "003", NumeroVia = 2, Cpf = "34567890123", Rg = "34567890", Nome = "Pedro Lima", NomeMae = "Clara Lima", NomePai = "Roberto Lima", Foto = "foto3.jpg" }
            };

            foreach (var protocolo in protocolos)
            {
                var json = JsonSerializer.Serialize(protocolo);
                var body = Encoding.UTF8.GetBytes(json);
                channel.BasicPublish(exchange: "", routingKey: "protocolos", basicProperties: null, body: body);

                Console.WriteLine($"Enviado: {json}");
            }

            Console.WriteLine("Protocolos enviados para a fila.");
        }
    }
}
