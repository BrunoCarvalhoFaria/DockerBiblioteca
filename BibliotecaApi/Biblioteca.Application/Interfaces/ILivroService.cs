using Biblioteca.Application.DTO;

namespace Biblioteca.Application.Interfaces
{
    public interface ILivroService
    {
        Task<long> LivroPost(LivroPostDTO dto);
        LivroDTO? LivroGetAById(long id);
        string LivroDelete(long id);
        string LivroPut(LivroDTO dto);
        LivroObterTodosDTO ObterTodos(int pagina, int qtdRegistros);
        LivroObterTodosDTO ObterTodosComFiltro(string? codigo, string? titulo, string? ano, string? autor, long? generoId, string? editora, int pagina = 1, int qtdRegistros = 99999);
    }
}
