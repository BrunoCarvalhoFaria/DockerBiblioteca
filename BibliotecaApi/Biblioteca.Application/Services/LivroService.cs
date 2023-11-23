using AutoMapper;
using Biblioteca.Application.DTO;
using Biblioteca.Application.Interfaces;
using Biblioteca.Domain.Entities;
using Biblioteca.Domain.Interfaces;

namespace Biblioteca.Application.Services
{
    public class LivroService : ILivroService
    {
        private readonly IMapper _mapper;
        private readonly ILivroRepository _livroRepository;
        private readonly IUsuarioAutorizacaoService _usuarioAutorizacaoService;
        private readonly ILivroGeneroService _livroGeneroService;
        private readonly IUtilsService _utilsService;
        
        public LivroService(ILivroRepository livroRepository,
            IMapper mapper,
            IUsuarioAutorizacaoService usuarioAutorizacaoService,
            ILivroGeneroService livroGeneroService,
            IUtilsService utilsService
            )
        {
            _livroRepository = livroRepository;
            _usuarioAutorizacaoService = usuarioAutorizacaoService;
            _livroGeneroService = livroGeneroService;
            _mapper = mapper;
            _utilsService = utilsService;
            
        }

        public async Task<long> LivroPost(LivroPostDTO dto)
        {
            try
            {
                if (!_utilsService.TodosPropriedadesPreenchidas(dto))
                    throw new Exception("Todos os campos devem ser preenchidos");
                if(_livroGeneroService.LivroGeneroGetAById(dto.LivroGeneroId) == null)
                    throw new Exception("Gênero do livro não encontrado.");
                Livro livro = _mapper.Map<Livro>(dto);
                await _livroRepository.Add(livro);
                EstoqueDTO estoque = new EstoqueDTO { LivroId = livro.Id, Qtd = 0 };
                return livro.Id;
            }
            catch (Exception)
            {
                throw;
            }
        }
               
        public LivroDTO? LivroGetAById(long id)
        {
            try
            {
                return _mapper.Map<LivroDTO>(_livroRepository.GetById(id));
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string LivroDelete(long id)
        {
            try
            {
                Livro? livro = _livroRepository.GetById(id);
                if (livro == null)
                    throw new Exception("Livro não encontrado");
                livro.Excluir();
                _livroRepository.Update(livro);
                return "Livro excluído com sucesso";
            }
            catch (Exception)
            {

                throw;
            }
        }

        public string LivroPut(LivroDTO dto)
        {
            try
            {
                if (!_utilsService.TodosPropriedadesPreenchidas(dto))
                    throw new Exception("Todos os campos devem ser preenchidos");
                if (dto.Id == null)
                    throw new Exception("Livro não encontrado");
                var livro = _livroRepository.GetById(dto.Id);
                if (livro == null)
                    throw new Exception("Livro não encontrado");
                if (_livroGeneroService.LivroGeneroGetAById(dto.LivroGeneroId) == null)
                    throw new Exception("Gênero do livro não encontrado.");
                
                _livroRepository.Update(_mapper.Map<Livro>(dto));
                return "Sucesso ao alterar o livro.";
            }
            catch (Exception)
            {

                throw;
            }
        }

        public LivroObterTodosDTO ObterTodos(int pagina = 1 , int qtdRegistros = 99999)
        {
            LivroObterTodosDTO resultado = new();
            var livros = _mapper.Map<List<LivroDTO>>(_livroRepository.GetAll().ToList());
            resultado.TotalRegistros = livros.Count;
            resultado.Livros = livros.Skip((pagina - 1 ) * qtdRegistros).Take(qtdRegistros).ToList();
            return resultado;
        }

        public LivroObterTodosDTO ObterTodosComFiltro(string? codigo, string? titulo, string? ano, string? autor, long? generoId, string? editora, int pagina = 1, int qtdRegistros = 99999)
        {
            LivroObterTodosDTO resultado = new();
            var livros = _mapper.Map<List<LivroDTO>>(_livroRepository.ObterTodosComFiltro(codigo, titulo, ano, autor, generoId, editora));
            resultado.TotalRegistros = livros.Count;
            resultado.Livros = livros.Skip((pagina - 1) * qtdRegistros).Take(qtdRegistros).ToList();
            return resultado;
        }


    }
}
