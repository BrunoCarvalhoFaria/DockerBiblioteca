using Biblioteca.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Application.Interfaces
{
    public interface ICaixaService
    {
        Task<long> CaixaPost(CaixaDTO dto);
        List<CaixaDTO> CaixaGetAll();
        CaixaDTO? CaixaGetAById(long id);
        string CaixaPut(CaixaDTO dto);
    }
}
