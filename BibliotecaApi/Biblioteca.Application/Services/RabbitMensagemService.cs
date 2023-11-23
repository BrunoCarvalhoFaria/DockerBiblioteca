using AutoMapper;
using Biblioteca.Application.Interfaces;
using Biblioteca.Domain.Entities;
using Biblioteca.Domain.Interfaces;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Biblioteca.Application.Services
{
    public class RabbitMensagemService : IRabbitMensagemService
    {
        private readonly IMapper _mapper;
        private readonly IRabbitMensagemRepository _rabbitMensagemRepository;

        public RabbitMensagemService(
            IMapper mapper,
            IRabbitMensagemRepository rabbitMensagemRepository
            )
        {
            _mapper = mapper;
            _rabbitMensagemRepository = rabbitMensagemRepository;
        }
        public void EnviarMensagem(RabbitMensagemDTO mensagem)
        {
            var factory = new ConnectionFactory
            {
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
