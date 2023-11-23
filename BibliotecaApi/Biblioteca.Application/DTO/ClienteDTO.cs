using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Application.DTO
{
    public class ClienteDTO
    {
        public long? Id {  get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string? UsuarioId { get; set; }
        public ClienteDTO(string nome, string email) {
            Nome = nome;
            Email = email;
        }

        public ClienteDTO(string nome, string email, string usuarioId)
        {
            Nome = nome;
            Email = email;
            UsuarioId = usuarioId;
        }
    }
}
