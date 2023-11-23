using Biblioteca.Domain.DTO;
using Biblioteca.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Domain.Interfaces
{
    public interface IEmprestimoRepository : IRepository<Emprestimo>
    {
        List<EstoqueConsultaDTO> ObterEmprestimos(long? clienteId, bool apenasPendentesDevolucao, DateTimeOffset? dataInicial, DateTimeOffset? dataFinal);
    }
}
