using LLS.Identity.Database.Commands;
using LLS.Identity.Domain.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

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
                    return Results.Ok(new { Token = result.IsError });
                })
            .WithName("User auth")
            .WithOpenApi();
    }
}