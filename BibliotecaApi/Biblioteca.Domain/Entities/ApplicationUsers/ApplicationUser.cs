using Biblioteca.Domain.Core.Enums;
using Microsoft.AspNetCore.Identity;

namespace Biblioteca.Domain.Entities.ApplicationUsers
{
    public class ApplicationUser : IdentityUser
    {
        public string Email { get; set; } = "";
        public string Nome { get; set; } = "";
        public TipoUsuarioEnum TipoUsuario { get; set; } = TipoUsuarioEnum.Cliente;
        public ICollection<Cliente> Clientes { get; set; }
    }
}
