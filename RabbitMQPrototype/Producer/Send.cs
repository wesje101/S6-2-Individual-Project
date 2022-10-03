namespace Producer;
using System;
using RabbitMQ.Client;
using System.Text;

public class Send
{
    public static void SendMessage()
    {
        var factory = new ConnectionFactory() { HostName = "rabbitmq" };
        using (var connection = factory.CreateConnection())
        using (var channel = connection.CreateModel())
        {
            channel.QueueDeclare(queue: "hello",
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            string message = "Hello world";
            var body = Encoding.UTF8.GetBytes(message);
            
            channel.BasicPublish(exchange: "",
                routingKey: "hello",
                basicProperties: null,
                body: body);
            Console.WriteLine(" [x] Sent {0}", message);
        }
    }
}