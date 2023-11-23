using Biblioteca.Application.DTO;
using Biblioteca.Domain.Entities.Vendedores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Application.Interfaces
{
    public interface IVendedorService
    {
        Task<long> VendedorPost(VendedorDTO dto);
        List<VendedorDTO> VendedorGetAll();
        VendedorDTO? VendedorGetAById(long id);
    }
}
