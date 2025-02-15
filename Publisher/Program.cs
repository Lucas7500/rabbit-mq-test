using RabbitMQ.Client;
using System.Text;

var factory = new ConnectionFactory { HostName = "localhost" };
using var connection = factory.CreateConnection();
using var channel = connection.CreateModel();

channel.QueueDeclare(queue: "hello",
                     durable: false,
                     exclusive: false,
                     autoDelete: false,
                     arguments: null);

Console.WriteLine("Digite sua mensagem e aperte <ENTER>");

while (true)
{
    var message = Console.ReadLine();

    if (string.IsNullOrEmpty(message))
        break;

    channel.BasicPublish(exchange: string.Empty,
                         routingKey: "hello",
                         basicProperties: null,
                         body: Encoding.UTF8.GetBytes(message));

    Console.WriteLine($" [x] Enviado {message}");
}