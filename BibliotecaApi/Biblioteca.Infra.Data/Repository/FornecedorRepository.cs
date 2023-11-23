using Biblioteca.Domain.Entities.Caixas;
using Biblioteca.Domain.Entities.Fornecedores;
using Biblioteca.Domain.Interfaces;
using DrPay.Infra.Data.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Infra.Data.Repository
{
    public class FornecedorRepository : Repository<Fornecedor>, IFornecedorRepository
    {
        private readonly DbContextOptions<BibliotecaDbContext> _optionsBuilder;

        public FornecedorRepository(BibliotecaDbContext context) : base(context)
        {
            _optionsBuilder = new DbContextOptions<BibliotecaDbContext>();
        }
    }
}
