using AutoMapper;
using Biblioteca.Api.ViewModel;
using Biblioteca.Application.Interfaces;
using Biblioteca.Domain.Entities.ApplicationUsers;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Biblioteca.Api.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class EstoqueController : ControllerBase
    {
        private readonly IEstoqueService _estoqueService;
        public EstoqueController(
            IEstoqueService estoqueService)
        {
            _estoqueService = estoqueService;
        }

        [HttpPut]
        [Route("")]
        public IActionResult PutEstoque(long livroId, long qtdInserida)
        {
            try
            {
                if (livroId == 0 || qtdInserida == 0)
                    throw new Exception("Todos parâmetros devem ser preenchidos");
                _estoqueService.AlterarEstoque(livroId, qtdInserida);
                return Ok(new { Sucesso = true, Conteudo = "Quantidade alterada com sucesso!" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("ObterEstoque")]
        public IActionResult ObterEstoque([FromBody] ObterEstoqueViewModel obterEstoqueViewModel)
        {
            try
            {
                return Ok(new { Sucesso = true, Conteudo = _estoqueService.ListarEstoque(obterEstoqueViewModel.LivroIds) });
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
    }
}
