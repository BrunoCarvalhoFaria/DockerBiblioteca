using Biblioteca.Domain.Entities.Vendedores;
using Biblioteca.Domain.Entities.Vendedores.Repository;
using DrPay.Infra.Data.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Infra.Data.Repository
{
    public class VendedorRepository : Repository<Vendedor>, IVendedorRepository
    {
        private readonly DbContextOptions<BibliotecaDbContext> _optionsBuilder;

        public VendedorRepository(BibliotecaDbContext context) : base(context)
        {
            _optionsBuilder = new DbContextOptions<BibliotecaDbContext>();
        }
    }
}
