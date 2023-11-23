using AutoMapper;
using Biblioteca.Api.ViewModel;
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
    public class FaltaController : ControllerBase
    {
        private readonly IFaltaService _faltaService;
        private readonly IMapper _mapper;
        public FaltaController(
            SignInManager<ApplicationUser> signInManager,
            IFaltaService faltaService,
            IMapper mapper)
            
        {
            _faltaService = faltaService;
            _mapper = mapper;
        }
        [HttpPost]
        [Route("")]
        public IActionResult FaltaPost(FaltaPostViewModel faltaViewModel)
        {
            try
            {
                return Ok(_faltaService.FaltaPost(_mapper.Map<FaltaDTO>(faltaViewModel)));
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
                return Ok(_faltaService.FaltaGetAll());
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
                return Ok(_faltaService.FaltaGetAById(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut]
        [Route("")]
        public async Task<IActionResult> CaixaPut(FaltaDTO faltaDTO)
        {
            try
            {
                return Ok(_faltaService.FaltaPut(faltaDTO));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
