using Lurta.Clean.Application.Services;
using Lurta.Clean.Application.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Lurta.Clean.Application
{
    public static class Register
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IAuthenticationService, AuthenticationService>();
        }
    }
}
