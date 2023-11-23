using Biblioteca.Domain.Core.Enums;

namespace Biblioteca.Api.ViewModel
{
    public class FaltaPostViewModel
    {
        public string CodigoDeBarras { get; set; } = "";
        public string NomeProduto { get; set; } = "";
        public string? Laboratorio { get; set; }
        public int Quantidade { get; set; }
        public string NomeCliente { get; set; } = "";
        public long VendedorId { get; set; }
        public string TelefoneCliente { get; set; } = "";
        public StatusPagamento Pago { get; set; }
        public decimal? ValorPago { get; set; }
        public TipoFalta Tipo { get; set; }
    }
}
