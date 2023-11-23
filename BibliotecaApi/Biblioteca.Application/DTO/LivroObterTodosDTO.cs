using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Application.DTO
{
    public class LivroObterTodosDTO
    {
        public List<LivroDTO>? Livros {  get; set; }
        public int TotalRegistros { get; set; }
    }
}
