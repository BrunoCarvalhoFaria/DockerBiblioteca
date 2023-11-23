using Biblioteca.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Domain.Interfaces
{
    public interface IClienteRepository : IRepository<Cliente>
    {
        Cliente ObtemClientePorEmail(string email);
    }
}
