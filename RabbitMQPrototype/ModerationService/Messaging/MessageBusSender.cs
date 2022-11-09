using System.Text;
using RabbitMQ.Client;

namespace ModerationService.Messaging;

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
            channel.QueueDeclare(queue: "task_queue",
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            var body = Encoding.UTF8.GetBytes(message);

            var properties = channel.CreateBasicProperties();
            properties.Persistent = true;
            
            channel.BasicPublish(exchange: "",
                routingKey: "task_queue",
                basicProperties: null,
                body: body);
            Console.WriteLine(" [x] Sent {0}", message);
        }
    }
}