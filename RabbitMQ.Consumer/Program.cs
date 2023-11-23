using System.Text;
using System.Text.Json;
using Rabbit.Consumer.Model;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

//var factory = new ConnectionFactory
//{
//    HostName = "localhost",
//    UserName = "admin",
//    Password = "admin"
//};
//using var connection = factory.CreateConnection();
//using var channel = connection.CreateModel();

//channel.QueueDeclare(queue: "bibliotecaMensagem",
//                     durable: false,
//                     exclusive: false,
//                     autoDelete: false,
//                     arguments: null);

//string json = JsonSerializer.Serialize("teste");
//var body = Encoding.UTF8.GetBytes(json);

//channel.BasicPublish(exchange: string.Empty,
//                     routingKey: "bibliotecaMensagem",
//                     basicProperties: null,
//                     body: body);

var factory = new ConnectionFactory
{
    HostName = "localhost",
    UserName = "admin",
    Password = "admin",
};
using var connection = factory.CreateConnection();
using var channel = connection.CreateModel();

channel.QueueDeclare(queue: "bibliotecaMensagem",
                     durable: false,
                     exclusive: false,
                     autoDelete: false,
                     arguments: null);

Console.WriteLine(" [*] Waiting for messages.");

var consumer = new EventingBasicConsumer(channel);
consumer.Received += (model, ea) =>
{
    var body = ea.Body.ToArray();
    var message = Encoding.UTF8.GetString(body);

    RabbitMensagem mensagem = JsonSerializer.Deserialize<RabbitMensagem>(message);

    Console.WriteLine($@" [x] Received : Id: {mensagem.Id} / 
                                        LivroId: {mensagem.LivroId} / 
                                        Livro nome: {mensagem.LivroNome} / 
                                        ClienteId: {mensagem.ClienteId} / 
                                        ClienteNome: {mensagem.ClienteNome}");
};
channel.BasicConsume(queue: "bibliotecaMensagem",
                     autoAck: true,
                     consumer: consumer);

Console.WriteLine(" Press [enter] to exit.");
Console.ReadLine();