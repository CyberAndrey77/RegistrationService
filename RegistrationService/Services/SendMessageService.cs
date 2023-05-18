using RabbitMQ.Client;
using RegistrationService.Services.Interface;
using System.Text;

namespace RegistrationService.Services
{
    //public class SendMessageService : ISendMessageService
    //{
    //    private readonly IConnection _connection;
    //    public SendMessageService()
    //    {
    //        var connectionFactory = new ConnectionFactory()
    //        {
    //            //HostName = "rabbitmq",
    //            //Port = 5672,
    //            //UserName = "guest",
    //            //Password = "guest"
    //        };
    //        connectionFactory.Uri = new Uri("amqp://guest:guest@localhost:5672");
    //        _connection = connectionFactory.CreateConnection();
    //    }

    //    public async Task<string> SendMessage(string message)
    //    {
    //        using (var channel = _connection.CreateModel())
    //        {
    //            channel.QueueDeclare(queue: "MyQueue",
    //                                 durable: false,
    //                                 exclusive: false,
    //                                 autoDelete: false,
    //                                 arguments: null);

    //            var body = Encoding.UTF8.GetBytes(message);

    //            channel.BasicPublish(exchange: "",
    //                                 routingKey: "MyQueue",
    //                                 basicProperties: null,
    //                                 body: body);
    //        }

    //        return "Сообщение отправлено";
    //    }
    //}
}
