using Biblioteca.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Biblioteca.Application.Services
{
    public sealed class UsuarioAutorizacaoService : IUsuarioAutorizacaoService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UsuarioAutorizacaoService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string ObterUsuarioLogado()
        {
            return _httpContextAccessor?.HttpContext?.User?.FindFirst("usuarioId")?.Value ?? string.Empty;
        }

        public bool UsuarioLogadoAdministrador()
        {
            return _httpContextAccessor?.HttpContext?.User?.FindFirst("tipoUsuario")?.Value == "Administrador"; 
        }
    }
}
