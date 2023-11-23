using Biblioteca.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Application.DTO
{
    public class EmprestimoDTO
    {
        public long Id {  get; set; }
        public long LivroId { get; set; }
        public long ClienteId { get; set; }
        public DateTimeOffset DataEmprestimo { get; set; }
        public DateTimeOffset? DataDevolucao { get; set; }
    }
}
