using Biblioteca.Domain.Core.Enums;
using Biblioteca.Domain.Core.Models;
using Biblioteca.Domain.Entities.ApplicationUsers;
using Biblioteca.Domain.Entities.Fornecedores;
using Biblioteca.Domain.Entities.Vendedores;

namespace Biblioteca.Domain.Entities.Faltas
{
    public class Falta : Entity<Falta>
    {
        public DateTimeOffset DataCriacao { get; set; }
        public string UsuarioCriacaoId { get; set; } = "";
        public string CodigoDeBarras { get; set; } = "";
        public string NomeProduto { get; set; } = "";
        public string? Laboratorio { get; set; }
        public int Quantidade { get; set; }
        public string NomeCliente { get; set; } = "";
        public string TelefoneCliente { get; set; } = "";
        public StatusPagamento Pago { get; set; }
        public decimal? ValorPago { get; set; }
        public long VendedorId { get; set; }
        public  StatusFalta Status { get; set; }
        public TipoFalta Tipo { get; set; }
        public long? FornecedorId { get; set; }
        public string? ObservacaoComprador { get; set; }

        public virtual Vendedor Vendedor {  set; get; }
        public virtual ApplicationUser Usuario {  get; set; }
        public virtual Fornecedor Fornecedor { get; set; }
        public override bool EhValido()
        {
            return true;
        }

    }
}
