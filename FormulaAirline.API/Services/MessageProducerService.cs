using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace FormulaAirline.API.Services
{
    public interface IMessageProducerService
    {
        public void SendingMessage<T>(T message);
    }

    public class MessageProducerService : IMessageProducerService
    {
        public void SendingMessage<T>(T message)
        {
            var factory = new ConnectionFactory()
            {
                HostName = "localhost",
                UserName = "user",
                Password = "password",
                VirtualHost = "/"
            };

            var connection = factory.CreateConnection();

            using var channel = connection.CreateModel();

            channel.QueueDeclare("bookings", durable: true, exclusive: false);

            var jsonString = JsonSerializer.Serialize(message);
            var body = Encoding.UTF8.GetBytes(jsonString);


            channel.BasicPublish("", "bookings", body: body);
        }
    }
}
