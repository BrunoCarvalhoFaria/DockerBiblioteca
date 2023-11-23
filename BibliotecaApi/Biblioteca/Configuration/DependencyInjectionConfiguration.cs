using Biblioteca.Infra.CrossCutting.IoC;

namespace Biblioteca.Api.Configuration
{
    public static class DependencyInjectionConfiguration
    {
        public static void AddDIConfiguration(this IServiceCollection services)
        {
            Injector.RegisterServices(services);
        }
    }
}
