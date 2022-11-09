using System.Text;
using RabbitMQ.Client;

namespace AuthService.Messaging;

public class MessageBusSender
{
    public MessageBusSender()
    {
        
    }
    
    public void SendMessage(string message)
    {
        var factory = new ConnectionFactory() { HostName = "rabbitmq" };
        using (var connection = factory.CreateConnection())
        using (var channel = connection.CreateModel())
        {
            channel.ExchangeDeclare(exchange: "auth_service_out", ExchangeType.Fanout);
            
            
            var body = Encoding.UTF8.GetBytes(message);

            var properties = channel.CreateBasicProperties();
            properties.Persistent = true;
            
            channel.BasicPublish(exchange: "auth_service_out",
                routingKey: "auth_service_out",
                basicProperties: null,
                body: body);
            Console.WriteLine(" [x] Sent {0}", message);
        }
    }
}