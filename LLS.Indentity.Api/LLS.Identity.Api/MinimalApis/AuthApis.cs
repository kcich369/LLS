using LLS.Identity.Database.Commands;
using LLS.Identity.Domain.Commands;
using LLS.Identity.Domain.Dtos;
using LLS.Identity.Domain.ExternalServices;
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
            .WithName("user")
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

        routeBuilder.MapGet("auth/send",
                () => { return Results.Ok("OK, it works"); }).WithName("user auth valid").WithOpenApi()
            .RequireAuthorization("admin");

        routeBuilder.MapGet("auth/send-email",
            async (IEmailService emailService) =>
            {
                await emailService.Send(new EmailData()
                    { To = "kcich369@gmail.com", HtmlMessage = "Witam", Subject = "Cud milosci" });
                return Results.Ok("OK, it works");
            }).WithName("user auth send email").WithOpenApi();

        routeBuilder.MapGet("auth/send-sms",
            async (ISmsService smsService) =>
            {
                await smsService.SendSms(new SmsData()
                    { Text = "Witamy w serwisie Cud Milosci.", PhoneNumber = "+48504400336", From = "Cud-Milosci"});
                return Results.Ok("OK, it works");
            }).WithName("user auth send sms").WithOpenApi();
    }
}