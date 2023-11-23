using Biblioteca.Application.DTO;

namespace Biblioteca.Application.Interfaces
{
    public interface ILivroGeneroService
    {
        Task<long> LivroGeneroPost(string descricao);
        LivroGeneroDTO? LivroGeneroGetAById(long id);
        string LivroGeneroDelete(long id);
        string LivroGeneroPut(LivroGeneroDTO dto);
        List<LivroGeneroDTO> Obtertodos();
    }
}
