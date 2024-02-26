using LLS.Identity.Domain.Enumerations;
using Microsoft.Extensions.DependencyInjection;

namespace LLS.Identity.Infrastructure;

public static class AddPolicyExtension
{
    public static IServiceCollection AddPolicies(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddAuthorizationBuilder()
            .AddPolicy("admin",p=>p.RequireRole(RoleEnum.Admin))
            .AddPolicy("user",p=>p.RequireRole(RoleEnum.User));
        return serviceCollection;
    }
}