using LLS.Identity.Domain.Interfaces;
using LLS.Identity.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace LLS.Identity.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection RegisterInfrastructure(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IAuthService, AuthService>();
        serviceCollection.AddScoped<IJwtTokenProvider, JwtTokenProvider>();
        serviceCollection.AddScoped<ILoginService, LoginService>();
        return serviceCollection;
    }
}