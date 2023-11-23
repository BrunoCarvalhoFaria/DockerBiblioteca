using Biblioteca.Application.DTO;
using Biblioteca.Application.Interfaces;
using Biblioteca.Domain.Entities.ApplicationUsers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Biblioteca.Api.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class CaixaController : ControllerBase
    {
        private readonly ICaixaService _caixaService;
        public CaixaController(
            SignInManager<ApplicationUser> signInManager,
            ICaixaService caixaService)
        {
            _caixaService = caixaService;
        }
        [HttpPost]
        [Route("")]
        public async Task<IActionResult> CaixaPost(CaixaDTO caixa)
        {
            try
            {
                return Ok(await _caixaService.CaixaPost(caixa));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        [Route("")]
        public IActionResult CaixaGetAll()
        {
            try
            {
                return Ok(_caixaService.CaixaGetAll());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult CaixaGetById(long id)
        {
            try
            {
                return Ok(_caixaService.CaixaGetAById(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut]
        [Route("")]
        public async Task<IActionResult> CaixaPut(CaixaDTO caixa)
        {
            try
            {
                return Ok(_caixaService.CaixaPut(caixa));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
