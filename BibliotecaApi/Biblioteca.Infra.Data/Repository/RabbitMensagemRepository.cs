using Biblioteca.Domain.Entities;
using Biblioteca.Domain.Interfaces;
using DrPay.Infra.Data.Repository;
using Microsoft.AspNetCore.Connections;
using Microsoft.EntityFrameworkCore;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;
namespace Biblioteca.Infra.Data.Repository
{
    public class RabbitMensagemRepository : Repository<RabbitMensagem>, IRabbitMensagemRepository
    {
        private readonly DbContextOptions<BibliotecaDbContext> _optionsBuilder;

        public RabbitMensagemRepository(BibliotecaDbContext context) : base(context)
        {
            _optionsBuilder = new DbContextOptions<BibliotecaDbContext>();
        }

        public void EnviarMensagem(RabbitMensagem mensagem)
        {
            var factory = new ConnectionFactory { 
                HostName = "rabbitMQ",
                UserName = "admin",
                Password = "admin"
                
            };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.QueueDeclare(queue: "bibliotecaMensagem",
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            string json = JsonSerializer.Serialize(mensagem);
            var body = Encoding.UTF8.GetBytes(json);

            channel.BasicPublish(exchange: string.Empty,
                                 routingKey: "bibliotecaMensagem",
                                 basicProperties: null,
                                 body: body);

        }

    }
}
