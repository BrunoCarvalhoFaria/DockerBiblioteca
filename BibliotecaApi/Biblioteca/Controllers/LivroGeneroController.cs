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
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILivroGeneroService _livroGeneroService;
        private readonly IMapper _mapper;
        public LivroGeneroController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IMapper mapper,
            ILivroGeneroService livroGeneroService)
        {
            _userManager = userManager;
            _livroGeneroService = livroGeneroService;
            _signInManager = signInManager;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> AdicionarLivroGenero(string descricao)
        {
            try
            {
                return Ok(await _livroGeneroService.LivroGeneroPost(descricao));
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
                return Ok(_livroGeneroService.Obtertodos());
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
                return Ok(_livroGeneroService.LivroGeneroGetAById(id));
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
                return Ok(_livroGeneroService.LivroGeneroDelete(id));
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
                return Ok(_livroGeneroService.LivroGeneroPut(dto));
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
    }

}
