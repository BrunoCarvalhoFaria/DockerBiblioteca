using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Domain.DTO
{
    public class EstoqueConsultaDTO
    {
        public long Id { get; set; }
        public long ClienteId { get; set; }
        public string ClienteNome{ get; set; }
        public long LivroId { get; set; }
        public string Titulo { get; set; }
        public DateTimeOffset DataEmprestimo { get; set; }
        public DateTimeOffset DataDevolucao { get; set; }
    }
}
