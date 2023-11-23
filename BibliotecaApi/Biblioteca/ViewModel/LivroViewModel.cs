namespace Biblioteca.Api.ViewModel
{
    public class LivroViewModel
    {
        public string Codigo { get; set; }
        public string Titulo { get; set; }
        public string Autor { get; set; }
        public string Ano { get; set; }
        public long LivroGeneroId { get; set; }
        public string Editora { get; set; }
    }
}
