using Biblioteca.Api.Token;
using Biblioteca.Api.ViewModel;
using Biblioteca.Application.DTO;
using Biblioteca.Application.Interfaces;
using Biblioteca.Domain.Core.Enums;
using Biblioteca.Domain.Entities.ApplicationUsers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Biblioteca.Api.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IClienteService _clienteService;
        public UsersController(UserManager<ApplicationUser> userManager, 
            SignInManager<ApplicationUser> signInManager,
            IClienteService clienteService)
        {
            _userManager = userManager;
            _clienteService = clienteService;
            _signInManager = signInManager;
        }
        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<IActionResult> CriarTokenIdentity([FromBody] LoginViewModel login)
        {
            try
            {
                if(string.IsNullOrWhiteSpace(login.Senha) || string.IsNullOrWhiteSpace(login.Email))
                    return Unauthorized();
                var resultado = await _signInManager.PasswordSignInAsync(login.Email, login.Senha, false, lockoutOnFailure: false);
                if (resultado.Succeeded)
                {
                    var user = new ApplicationUser
                    {
                        UserName = login.Email
                    };
                    var userCurrent = await _userManager.FindByNameAsync(login.Email);

                    if (userCurrent == null)
                        return BadRequest("Falha ao buscar usuario");
                    
                    string usuarioId = userCurrent.Id;

                    var userClaims = await _userManager.GetClaimsAsync(userCurrent);

                    userClaims.Add(new Claim("usuarioId", usuarioId));
                    var key = Encoding.ASCII.GetBytes("BrunoFaria0123456789_JWTToken_2023_Biblioteca");
                    var tokenConfig = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(userClaims),
                        Expires = DateTime.UtcNow.AddHours(3),
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
                    };

                    var tokenHandler = new JwtSecurityTokenHandler();
                    var token = tokenHandler.CreateToken(tokenConfig);                    

                    return Ok(tokenHandler.WriteToken(token));
                }
                else { return Unauthorized(); }
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("criarUsuarioCliente")]
        public async Task<IActionResult> CriarUsuarioCliente([FromBody] NovoUsuarioViewModel novoUsuario)
        {
            if (string.IsNullOrWhiteSpace(novoUsuario.Email) || string.IsNullOrWhiteSpace(novoUsuario.Nome))
                return BadRequest("Preencha todos os campos.");

            var usuario = new ApplicationUser { 
                UserName = novoUsuario.Email,
                Nome = novoUsuario.Nome,
                Email = novoUsuario.Email,    
                TipoUsuario = TipoUsuarioEnum.Cliente
            };

            var resultado = await _userManager.CreateAsync(usuario, novoUsuario.Senha);
            if(resultado.Errors.Any())
                return Ok(resultado.Errors);
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(usuario);
            var resultado2 = await _userManager.ConfirmEmailAsync(usuario, code);

            if (resultado2.Succeeded)
            {
                ClienteDTO cliente = _clienteService.ObtemClientePorEmail(usuario.Email);
                if (cliente == null)
                {
                    cliente = new ClienteDTO(usuario.Nome, usuario.Email, usuario.Id);
                    await _clienteService.ClientePost(cliente);
                } else
                {
                    cliente.UsuarioId = usuario.Id;
                    _clienteService.ClientePut(cliente);
                }

                return Ok("Usuário criado com sucesso!");
            }
            else
                return BadRequest("Erro ao criar usuário");
        }
    }
}
