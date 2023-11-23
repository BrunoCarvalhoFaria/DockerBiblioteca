using Biblioteca.Domain.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Application.DTO
{
    public class FaltaDTO
    {
        public long Id { get; set; }
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
        public StatusFalta Status { get; set; }
        public TipoFalta Tipo { get; set; }
        public long? FornecedorId { get; set; }
        public string? ObservacaoComprador { get; set; }
    }
}
