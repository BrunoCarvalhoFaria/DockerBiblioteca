using Biblioteca.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Application.DTO
{
    public class EstoqueDTO
    {
        public long Id { get; set; }
        public required long LivroId { get; set; }
        public long Qtd { get; set; }

    }
}
