namespace Biblioteca.Api.ViewModel
{
    public class ObterLivroComFiltroViewModel
    {
        public int Pagina { get; set; }
        public int QtdRegistros { get; set; }
        public string? Codigo { get; set; }
        public string? Titulo { get; set; }
        public string? Autor { get; set; }
        public string? Ano { get; set; }
        public long? LivroGeneroId { get; set; }
        public string? Editora { get; set; }
    }
}
