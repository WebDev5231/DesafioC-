using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Text.Json;
using Consumer.Models;
using System.Configuration;

namespace Consumer
{
    class Program
    {
        static void Main(string[] args)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            var protocoloRepository = new ProtocoloRepository(connectionString);

            var factory = new ConnectionFactory() { HostName = "localhost" };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.QueueDeclare(queue: "protocolos", durable: false, exclusive: false, autoDelete: false, arguments: null);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    Console.WriteLine($"Recebido: {message}");

                    var protocolo = JsonSerializer.Deserialize<Protocolo>(message);

                    if (protocolo != null && ValidarProtocolo(protocolo))
                    {
                        Console.WriteLine($"Protocolo válido: {protocolo.NumeroProtocolo}");
                        protocoloRepository.AdicionarProtocolo(protocolo);

                        Console.WriteLine($"Protocolo {protocolo.NumeroProtocolo} persistido no banco de dados.");
                    }
                    else
                    {
                        Console.WriteLine("Protocolo inválido.");
                    }
                };

            channel.BasicConsume(queue: "protocolos", autoAck: true, consumer: consumer);

            Console.WriteLine("Pressione [enter] para sair.");
            Console.ReadLine();
        }

        private static bool ValidarProtocolo(Protocolo protocolo)
        {
            return !string.IsNullOrWhiteSpace(protocolo.NumeroProtocolo);
        }
    }
}