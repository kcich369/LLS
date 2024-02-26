using Microsoft.Extensions.DependencyInjection;

namespace LLS.Identity.Infrastructure;

public static class AddPolicyExtension
{
    public static IServiceCollection AddPolicies(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddAuthorizationBuilder()
            .AddPolicy("admin",p=>p.RequireRole("Admin"))
            .AddPolicy("user",p=>p.RequireRole("User"));
        return serviceCollection;
    }
}