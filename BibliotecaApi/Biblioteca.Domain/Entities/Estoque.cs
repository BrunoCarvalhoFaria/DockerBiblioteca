using Biblioteca.Domain.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Domain.Entities
{
    public class Estoque : Entity<Estoque>
    {
        public required long LivroId { get; set; }
        public long Qtd {  get; set; }

        public virtual Livro Livro { get; set; }
    }
}
