using Biblioteca.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Domain.Interfaces
{
    public interface ILivroRepository : IRepository<Livro>
    {
        List<Livro> ObterTodosComFiltro(string? codigo, string? titulo, string? ano, string? autor, long? livroGeneroId, string? editora);
    }
}
