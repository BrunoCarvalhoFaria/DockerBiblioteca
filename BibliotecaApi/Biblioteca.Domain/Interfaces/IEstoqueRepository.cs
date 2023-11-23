using Biblioteca.Domain.DTO;
using Biblioteca.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Domain.Interfaces
{
    public interface IEstoqueRepository : IRepository<Estoque>
    {
        List<RetornoEstoqueDTO> ListarEstoque(List<long> livroIdList);
        Estoque BuscarPorLivroId(long livroId);
    }
}
