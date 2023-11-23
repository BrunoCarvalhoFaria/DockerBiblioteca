using Biblioteca.Domain.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Domain.Entities
{
    public class LivroGenero : Entity<LivroGenero>
    {
        public LivroGenero(string descricao) { 
            Descricao = descricao;
        }
        public string Descricao { get; set; }

        public ICollection<Livro> Livros { get; set; }
    }
}
