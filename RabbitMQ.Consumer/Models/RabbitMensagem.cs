using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rabbit.Consumer.Model
{
    public class RabbitMensagem
    {
        public long Id { get; set; }
        public long LivroId { get; set; }
        public string LivroNome { get; set; }
        public long ClienteId { get; set; }
        public string ClienteNome { get; set; }
        public string Operacao { get; set; }
    }
}
