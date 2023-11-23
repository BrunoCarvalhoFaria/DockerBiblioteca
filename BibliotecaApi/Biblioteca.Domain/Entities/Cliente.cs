using Biblioteca.Domain.Core.Models;
using Biblioteca.Domain.Entities.ApplicationUsers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Domain.Entities
{
    public class Cliente : Entity<Cliente>
    {
        public required string Nome { get; set; }
        public required string Email { get; set; }
        public string? UsuarioId { get; set; }
        public virtual ApplicationUser? Usuario { get; set; }

        public ICollection<Emprestimo> Emprestimos { get; set; }
    }
}
