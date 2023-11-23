using Biblioteca.Application.Interfaces;
using Biblioteca.Application.Services;
using Biblioteca.Domain.Interfaces;
using Biblioteca.Infra.Data.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;


namespace Biblioteca.Infra.CrossCutting.IoC
{
    public class Injector
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddScoped<ILivroRepository, LivroRepository>();
            services.AddScoped<IClienteRepository, ClienteRepository>();
            services.AddScoped<ILivroGeneroRepository, LivroGeneroRepository>();
            services.AddScoped<IEstoqueRepository, EstoqueRepository>();
            services.AddScoped<IEmprestimoRepository, EmprestimoRepository>();
            services.AddScoped<IRabbitMensagemRepository, RabbitMensagemRepository>();

            services.AddScoped<IUsuarioAutorizacaoService,  UsuarioAutorizacaoService>();
            services.AddScoped<ILivroService, LivroService>();
            services.AddScoped<IClienteService, ClienteService>();
            services.AddScoped<IUtilsService, UtilsService>();
            services.AddScoped<ILivroGeneroService, LivroGeneroService>();
            services.AddScoped<IEstoqueService, EstoqueService>();
            services.AddScoped<IEmprestimoService, EmprestimoService>();
            services.AddScoped<IRabbitMensagemService, RabbitMensagemService>();
        }
    }
}
