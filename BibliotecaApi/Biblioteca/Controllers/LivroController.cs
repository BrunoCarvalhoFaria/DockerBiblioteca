using AutoMapper;
using Biblioteca.Api.ViewModel;
using Biblioteca.Application.DTO;
using Biblioteca.Application.Interfaces;
using Biblioteca.Application.Services;
using Biblioteca.Domain.Entities;
using Biblioteca.Domain.Entities.ApplicationUsers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Biblioteca.Api.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class LivroController : ControllerBase
    {
        private readonly ILivroService _livroService;
        private readonly IMapper _mapper;
        private readonly IEstoqueService _estoqueService;
        public LivroController(
            IMapper mapper,
            ILivroService livroService,
            IEstoqueService estoqueService)
        {
            _livroService = livroService;
            _mapper = mapper;
            _estoqueService = estoqueService;
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> AdicionarLivro([FromBody]LivroViewModel livro)
        {
            try
            {
                var livroId = await _livroService.LivroPost(_mapper.Map<LivroPostDTO>(livro));
                EstoqueDTO estoque = new EstoqueDTO { LivroId = livroId, Qtd = 0};
                await _estoqueService.PostEstoque(estoque);
                return Ok(new { Sucesso = true, Conteudo = livroId });
            }
            catch (Exception ex)
            {
                if (ex.Message == "Could not save changes. Please configure your entity type accordingly.")
                    return BadRequest("Código inserido já existe.");
                return BadRequest(ex.Message);                
            }
        }

        [HttpGet]
        [Route("")]
        public IActionResult ObterTodosPaginado(int pagina, int qtdRegistros)
        {
            try
            {
                return Ok(new { Sucesso = true, Conteudo = _livroService.ObterTodos(pagina, qtdRegistros) });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("obterComFiltro")]
        public IActionResult ObterComFiltroPaginado([FromBody] ObterLivroComFiltroViewModel viewModel)
        {
            try
            {
                return Ok(new { Sucesso = true, Conteudo = _livroService.ObterTodosComFiltro(viewModel.Codigo,viewModel.Titulo,viewModel.Ano,viewModel.Autor,viewModel.LivroGeneroId,viewModel.Editora,viewModel.Pagina,viewModel.QtdRegistros)});
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        [Route("{id}")]
        public IActionResult ObterPorId(long id)
        {
            try
            {
                return Ok(new { Sucesso = true, Conteudo = _livroService.LivroGetAById(id) });
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            } 
        }

        [HttpPut]
        [Route("")]
        public IActionResult AlterarLivro([FromBody]LivroDTO dto)
        {
            try
            {
               return Ok(new { Sucesso = true, Conteudo = _livroService.LivroPut(dto) }); 
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult DeletarLivro(long id)
        {
            try
            {
                return Ok(new { Sucesso = true, Conteudo = _livroService.LivroDelete(id) });
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
    }
}
