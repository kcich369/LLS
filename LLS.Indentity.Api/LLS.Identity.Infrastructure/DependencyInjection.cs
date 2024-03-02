using System.Net.Http.Headers;
using FluentValidation;
using LLS.Identity.Domain.Configurations;
using LLS.Identity.Domain.ExternalServices;
using LLS.Identity.Domain.Interfaces;
using LLS.Identity.Infrastructure.Extensions;
using LLS.Identity.Infrastructure.ExternalServices;
using LLS.Identity.Infrastructure.Services;
using LLS.Identity.Infrastructure.Validators;
using Mailjet.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LLS.Identity.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection RegisterInfrastructure(this IServiceCollection serviceCollection,
        IConfigurationManager configurationBuilder)
    {
        serviceCollection.AddValidatorsFromAssemblyContaining<RegisterUserValidation>();
        serviceCollection.AddScoped<IAuthService, AuthService>();
        serviceCollection.AddScoped<IJwtTokenProvider, JwtTokenProvider>();
        serviceCollection.AddScoped<ILoginService, LoginService>();
        serviceCollection.AddScoped<IRegisterService, RegisterService>();
        serviceCollection.AddScoped<IEmailService, MailjetEmailService>();
        serviceCollection.AddScoped<IRegistrationEmailService, RegistrationEmailService>();
        serviceCollection.AddScoped<ISmsService, SmsPlanetService>();

        var mailJetConfig = configurationBuilder.BindSection<MailJetConfiguration>();
        serviceCollection.AddHttpClient<IMailjetClient, MailjetClient>(client =>
        {
            client.UseBasicAuthentication(mailJetConfig.ApiKey, mailJetConfig.ApiKeySecret);
        });

        var smsPlanetConfig = configurationBuilder.BindSection<SmsPlanetConfiguration>();
        serviceCollection.AddHttpClient<ISmsService, SmsPlanetService>(httpClient =>
        {
            httpClient.BaseAddress = new Uri(smsPlanetConfig.BaseUrl);
            httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", smsPlanetConfig.ApiKey);
        });
        return serviceCollection;
    }
}