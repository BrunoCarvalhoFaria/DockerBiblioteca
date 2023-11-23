namespace Biblioteca.Api.ViewModel
{
    public class FiltroEstoqueViewModel
    {
        public long? ClienteId { get; set; }
        public bool? ApenasPendentesDevolucao { get; set; }
        public DateTimeOffset? DataInicial { get; set; }
        public DateTimeOffset? DataFinal { get; set; }
    }
}
