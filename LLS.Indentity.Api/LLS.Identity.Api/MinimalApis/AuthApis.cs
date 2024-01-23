using LLS.Identity.Database.Commands;
using LLS.Identity.Domain.Commands;
using LLS.Identity.Domain.Interfaces;

namespace LLS.Identity.Api.MinimalApis;

public static class AuthApis
{
    public static void AddAuthorisationApis(this IEndpointRouteBuilder routeBuilder)
    {
        routeBuilder.MapPost("auth/login",
                async (LoginUser loginUser, ILoginService loginService) =>
                {
                    var result = await loginService.Login(loginUser);
                    if (result.IsError)
                        return Results.BadRequest(result.ErrorMessage);
                    return Results.Ok(new { Token = result.Data });
                })
            .WithName("User auth")
            .WithOpenApi();
        
        routeBuilder.MapPost("auth/register",
                async (RegisterUser registerUser, IRegisterService registerService) =>
                {
                    var result = await registerService.Reqister(registerUser);
                    if (result.IsError)
                        return Results.BadRequest(result.ErrorMessage);
                    return Results.Ok(result);
                })
            .WithName("User registration")
            .WithOpenApi();
    }
}