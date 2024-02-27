using FluentValidation;
using LLS.Identity.Domain.Interfaces;
using LLS.Identity.Infrastructure.Services;
using LLS.Identity.Infrastructure.Validators;
using Microsoft.Extensions.DependencyInjection;
using SendGrid.Extensions.DependencyInjection;

namespace LLS.Identity.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection RegisterInfrastructure(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddValidatorsFromAssemblyContaining<RegisterUserValidation>();
        serviceCollection.AddScoped<IAuthService, AuthService>();
        serviceCollection.AddScoped<IJwtTokenProvider, JwtTokenProvider>();
        serviceCollection.AddScoped<ILoginService, LoginService>();
        serviceCollection.AddScoped<IRegisterService, RegisterService>();
        serviceCollection.AddScoped<IEmailService, EmailService>();
        serviceCollection.AddScoped<IRegistrationEmailService, RegistrationEmailService>();
        
        serviceCollection.AddSendGrid(options =>
        {
            options.ApiKey = Environment.GetEnvironmentVariable("SENDGRID_API_KEY");
        });
        return serviceCollection;
    }
}