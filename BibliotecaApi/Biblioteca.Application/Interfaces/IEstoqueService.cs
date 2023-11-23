using Biblioteca.Application.DTO;
using Biblioteca.Domain.DTO;
using Biblioteca.Domain.Entities;

namespace Biblioteca.Application.Interfaces
{
    public interface IEstoqueService
    {
        long CalcularEstoque(Estoque estoque, long qtd);
        Estoque AlterarEstoque(long livroId, long qtd);

        Task<long> PostEstoque(EstoqueDTO dto);
        List<RetornoEstoqueDTO> ListarEstoque(List<long> livroIdList);
    }
}
