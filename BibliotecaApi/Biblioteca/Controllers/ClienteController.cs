using AutoMapper;
using Biblioteca.Api.ViewModel;
using Biblioteca.Application.DTO;
using Biblioteca.Application.Interfaces;
using Biblioteca.Domain.DTO;
using Biblioteca.Domain.Entities.ApplicationUsers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Biblioteca.Api.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteService _clienteService;
        private readonly IMapper _mapper;
        public ClienteController(
            IMapper mapper,
            IClienteService clienteService)
        {
            _clienteService = clienteService;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> AdicionarCliente([FromBody] ClientePostViewModel dto)
        {
            try
            {
                long id = await _clienteService.ClientePost(_mapper.Map<ClienteDTO>(dto));
                return Ok(new { Sucesso = true, Conteudo = id });
            }
            catch (Exception ex )
            {

                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        [Route("")]
        public IActionResult ObterClientes()
        {
            try
            {
                return Ok(new { Sucesso = true, Conteudo = _clienteService.ObterTodos() });
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        [Route("{id}")]
        public IActionResult ObterClientePorId(long id)
        {
            try
            {
                return Ok(new { Sucesso = true, Conteudo = _clienteService.ClienteGetAById(id) });
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        [HttpDelete]
        [Route("{id}")]
        public IActionResult DeletarCliente(long id)
        {
            try
            {
                return Ok(new { Sucesso = true, Conteudo = _clienteService.ClienteDelete(id) });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut]
        [Route("")]
        public IActionResult AlterarCliente([FromBody] ClientePutViewModel dto)
        {
            try
            {
                return Ok(new { Sucesso = true, Conteudo = _clienteService.ClientePut(_mapper.Map<ClienteDTO>(dto)) });
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
    }
}
