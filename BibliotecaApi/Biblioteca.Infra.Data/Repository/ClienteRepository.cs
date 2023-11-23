using Biblioteca.Domain.Entities;
using Biblioteca.Domain.Interfaces;
using Dapper;
using DrPay.Infra.Data.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Infra.Data.Repository
{
    public class ClienteRepository : Repository<Cliente>, IClienteRepository
    {
        private readonly DbContextOptions<BibliotecaDbContext> _optionsBuilder;

        public ClienteRepository(BibliotecaDbContext context) : base(context)
        {
            _optionsBuilder = new DbContextOptions<BibliotecaDbContext>();
        }

        public Cliente ObtemClientePorEmail(string email)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine($@"select * from Cliente a where a.Email like '{email}' ");

            return data.Database.GetDbConnection().Query<Cliente>(sql.ToString()).FirstOrDefault();

        }
    }
}
