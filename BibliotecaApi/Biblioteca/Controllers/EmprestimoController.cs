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
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmprestimoService _emprestimoService;
        private readonly IMapper _mapper;
        public EmprestimoController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IMapper mapper,
            IEmprestimoService emprestimoService)
        {
            _userManager = userManager;
            _emprestimoService = emprestimoService;
            _signInManager = signInManager;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("realizarEmprestimo")]
        public async Task<IActionResult> RealizarEmprestimo(long clienteId, long livroId)
        {
            try
            {
                return Ok(await _emprestimoService.RealizarEmprestimo(clienteId, livroId));
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
                return Ok(_emprestimoService.RealizarDevolucao(emprestimoId));
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
                return Ok(_emprestimoService.ObterEmprestimos(filtros.ClienteId, apenasSemDevolucao, filtros.DataInicial, filtros.DataFinal));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
