using AutoMapper;
using Biblioteca.Api.ViewModel;
using Biblioteca.Application.DTO;
using Biblioteca.Application.Interfaces;
using Biblioteca.Application.Services;
using Biblioteca.Domain.Entities.ApplicationUsers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Biblioteca.Api.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class LivroGeneroController : ControllerBase
    {
        private readonly ILivroGeneroService _livroGeneroService;
        public LivroGeneroController(
            ILivroGeneroService livroGeneroService)
        {
            _livroGeneroService = livroGeneroService;
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> AdicionarLivroGenero(string descricao)
        {
            try
            {
                return Ok(new { Sucesso = true, Conteudo = await _livroGeneroService.LivroGeneroPost(descricao) });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("")]
        public IActionResult ObtemTodosLivroGenero()
        {
            try
            {
                return Ok(new { Sucesso = true, Conteudo = _livroGeneroService.Obtertodos() });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        [Route("{id}")]
        public IActionResult ObterLivroGeneroPorId(long id)
        {
            try
            {
                return Ok(new { Sucesso = true, Conteudo = _livroGeneroService.LivroGeneroGetAById(id) });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete]
        [Route("{id}")]
        public IActionResult DeletarLivroGenero(long id)
        {
            try
            {
                return Ok(new { Sucesso = true, Conteudo = _livroGeneroService.LivroGeneroDelete(id) });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
                throw;
            }
        }
        [HttpPut]
        [Route("")]
        public IActionResult AlterarLivroGenero([FromBody]LivroGeneroDTO dto)
        {
            try
            {
                return Ok(new { Sucesso = true, Conteudo = _livroGeneroService.LivroGeneroPut(dto) });
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
    }

}
