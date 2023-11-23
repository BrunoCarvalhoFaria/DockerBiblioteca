using Biblioteca.Domain.Core.Models;

namespace Biblioteca.Domain.Entities
{
    public class Livro : Entity<Livro>
    {
        public required string Codigo { get; set; }
        public required string Titulo { get; set; }
        public required string Autor { get; set; }
        public required string Ano { get; set; }
        public required long LivroGeneroId { get; set; }
        public required string Editora { get; set; }

        public virtual LivroGenero LivroGenero { get; set; }
        public ICollection<Estoque> Estoques { get; set; }
        public ICollection<Emprestimo> Emprestimos { get; set; }
    }
}
