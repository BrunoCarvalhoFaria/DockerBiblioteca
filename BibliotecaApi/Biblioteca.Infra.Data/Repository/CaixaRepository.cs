
using DrPay.Infra.Data.Repository;
using Microsoft.EntityFrameworkCore;
using Biblioteca.Domain.Entities.Caixas;
using Biblioteca.Domain.Interfaces;

namespace Biblioteca.Infra.Data.Repository
{
    public class CaixaRepository : Repository<Caixa>, ICaixaRepository
    {
        private readonly DbContextOptions<BibliotecaDbContext> _optionsBuilder;

        public CaixaRepository(BibliotecaDbContext context) : base(context)
        {
            _optionsBuilder = new DbContextOptions<BibliotecaDbContext>();
        }
    }
}
