namespace Biblioteca.Api.ViewModel
{
    public class MessageViewModel
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public bool Ativo { get; set; }
        public DateTimeOffset DataCadastro { get; set; }
        public DateTimeOffset DataAlteracao { get; set; }
        public string UserId { get; set; }
    }
}
