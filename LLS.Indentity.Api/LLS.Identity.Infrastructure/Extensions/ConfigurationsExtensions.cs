using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using IConfiguration = LLS.Identity.Domain.Configurations.Base.IConfiguration;

namespace LLS.Identity.Infrastructure.Extensions;

public static class ConfigurationsExtensions
{
    public static T BindSection<T>(this IConfigurationManager configurationBuilder, string sectionName)
        where T : IConfiguration, new()

    {
        var section = configurationBuilder.GetSection<T>();
        var config = new T();
        section.Bind(config);
        return config;
    }

    public static void AddSingleton<T>(this IServiceCollection services, IConfigurationManager configurationBuilder)
        where T : class, IConfiguration, new()
    {
        var section = configurationBuilder.GetSection<T>();
        var config = new T();
        section.Bind(config);
        services.AddSingleton<T>(config);
    }

    private static IConfigurationSection GetSection<T>(this Microsoft.Extensions.Configuration.IConfiguration configurationBuilder)
        where T : IConfiguration, new() =>
        configurationBuilder.GetSection(typeof(T).Name.Replace("Configuration", string.Empty));
}