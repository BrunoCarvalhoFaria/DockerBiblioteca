using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Domain.DTO
{
    public class RetornoEstoqueDTO
    {
        public string Titulo { get; set; }
        public string Autor {  get; set; }
        public long Qtd { get; set; }
        public long LivroId { get; set; }
    }
}
