using Biblioteca.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Domain.Interfaces
{
    public interface IRabbitMensagemRepository : IRepository<RabbitMensagem>
    {
        void EnviarMensagem(RabbitMensagem mensagem);
    }
}
