using AutoMapper;
using Biblioteca.Api.ViewModel;
using Biblioteca.Application.Interfaces;
using Biblioteca.Domain.Entities.ApplicationUsers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Biblioteca.Api.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class EmprestimoController : ControllerBase
    {
        private readonly IEmprestimoService _emprestimoService;
        public EmprestimoController(
            IEmprestimoService emprestimoService)
        {
            _emprestimoService = emprestimoService;
        }

        [HttpPost]
        [Route("realizarEmprestimo")]
        public async Task<IActionResult> RealizarEmprestimo(long clienteId, long livroId)
        {
            try
            {
                return Ok(new { Sucesso = true, Conteudo = await _emprestimoService.RealizarEmprestimo(clienteId, livroId) });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);                
            }
        }

        [HttpPut]
        [Route("realizarDevolucao/{emprestimoId}")]
        public IActionResult RealizarDevolucao(long emprestimoId)
        {
            try
            {
                return Ok(new { Sucesso = true, Conteudo = _emprestimoService.RealizarDevolucao(emprestimoId) });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("ObterEmprestimosComFiltro")]
        public IActionResult ObterEmprestimos([FromBody] FiltroEstoqueViewModel filtros)
        {
            try
            {
                bool apenasSemDevolucao = filtros.ApenasPendentesDevolucao ?? false;
                return Ok(new { Sucesso = true, Conteudo = _emprestimoService.ObterEmprestimos(filtros.ClienteId, apenasSemDevolucao, filtros.DataInicial, filtros.DataFinal) });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
