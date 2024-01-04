using LLS.Identity.Api.MinimalApis;

namespace LLS.Identity.Api;

public static class RegisterMinimalApisExtensions
{
    public static void RegisterMinimalApis(this IEndpointRouteBuilder routeBuilder)
    {
        routeBuilder.AddAuthorisationApis();
    }
}