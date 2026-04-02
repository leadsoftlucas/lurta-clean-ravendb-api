using Lurta.Clean.Application.Services;
using Lurta.Clean.Application.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Raven.Client.Http;
using Raven.DependencyInjection;

namespace Lurta.Clean.Infrastructure.IoC
{
    public static class ApplicationBootstrapper
    {
        public static IServiceCollection AddDependencyInjection(this IServiceCollection services)
            => services.AddRavenDB()
                       .AddSingletons()
                       .AddServices()
                       .AddRepositories();

        private static IServiceCollection AddRavenDB(this IServiceCollection services)
        {
            services.AddRavenDbDocStore(options =>
            {
                options.BeforeInitializeDocStore = docStore =>
                {
                    docStore.Conventions.IdentityPartsSeparator = '-';
                    docStore.Conventions.ReadBalanceBehavior = ReadBalanceBehavior.FastestNode;
                };
            });
            services.AddRavenDbAsyncSession();

            return services;
        }

        private static IServiceCollection AddSingletons(this IServiceCollection services)
        {
            return services;
        }

        private static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IUsuariosService, UsuariosService>();

            return services;
        }

        private static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            return services;
        }
    }
}
