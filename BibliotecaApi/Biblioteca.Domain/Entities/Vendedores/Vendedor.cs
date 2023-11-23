using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Biblioteca.Domain.Core.Models;
using Biblioteca.Domain.Entities.ApplicationUsers;
using Biblioteca.Domain.Entities.Faltas;

namespace Biblioteca.Domain.Entities.Vendedores
{
    public class Vendedor : Entity<Vendedor>
    {
        [StringLength(200)]
        public string Nome { get; set; } = string.Empty;
        public string Cpf { get; set; } = string.Empty;
        public int CodigoDeVenda { get; set; }
        public ICollection<Falta> Faltas { get; set; }

        public override bool EhValido()
        {
            return true;
        }

    }
}
