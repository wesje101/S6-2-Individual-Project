using System.Text;
using System.Text.Json;
using AccountService.Messaging.MessagingDTOs;
using AccountService.Models;
using AccountService.Models.Interfaces;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace AccountService.Messaging;

public class MessageBusListener : BackgroundService
{
    private readonly IConfiguration _configuration;
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly ILogger<MessageBusListener> _logger;
    private IConnection _connection;
    private IModel _channel;
    private string _queueName;
    
    public MessageBusListener(IConfiguration configuration, IServiceScopeFactory scopeFactory ,ILogger<MessageBusListener> logger)
    {
        _configuration = configuration;
        _scopeFactory = scopeFactory;
        _logger = logger;

        var factory = new ConnectionFactory() { HostName = _configuration["RabbitMQ:Host"] };
        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();
        _channel.ExchangeDeclare(exchange:"auth_service_out", type: ExchangeType.Fanout);
        _queueName = _channel.QueueDeclare().QueueName;
        _channel.QueueBind(queue: _queueName, exchange: "auth_service_out", routingKey: " ");
    }
    
    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        stoppingToken.ThrowIfCancellationRequested();

        var consumer = new EventingBasicConsumer(_channel);

        consumer.Received += (moduleHandle, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);

            using (var scope = _scopeFactory.CreateScope())
            {
                _logger.LogInformation("Message received: {message}", message);
            }
            
            ParseMessage(message);
        };

        _channel.BasicConsume(queue: _queueName, autoAck: true, consumer: consumer);

        return Task.CompletedTask;
    }

    private void ParseMessage(string message)
    {
        _logger.LogInformation("Parse start");
        
        BaseDTO? receivedData = JsonSerializer.Deserialize<BaseDTO>(message);

        if (receivedData == null) return;
        
        switch (receivedData.Identifier)
        {
            case DTOIdentifier.User:
                UserDTO? receivedUser = JsonSerializer.Deserialize<UserDTO>(message);
                _logger.LogInformation("user null: {userState}", receivedUser == null);
                _logger.LogInformation("username null: {usernameState}", receivedUser.Username == null);
                if (receivedUser?.Username != null)
                {
                    using (var scope = _scopeFactory.CreateScope())
                    {
                        _logger.LogInformation("Accessing Logic");
                        var logic = scope.ServiceProvider.GetRequiredService<IAccountLogic>();
                        _logger.LogInformation("Adding user");
                        logic.AddAccount(new Account {id = receivedUser.Id, name = receivedUser.Username, GoogleId = receivedUser.GoogleId});
                    }
                }
                break;
            default:
                break;
        }
    }
}