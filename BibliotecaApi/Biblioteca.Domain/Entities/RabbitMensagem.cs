using Biblioteca.Domain.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Domain.Entities
{
    public class RabbitMensagem : Entity<RabbitMensagem>
    {
        public long LivroId { get; set; }
        public string LivroNome { get; set; }
        public long ClienteId { get; set; }
        public string ClienteNome { get; set; }
        public string Operacao { get; set; }
    }
}
