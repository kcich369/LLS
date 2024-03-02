using LLS.Identity.Domain.Configurations.Base;

namespace LLS.Identity.Domain.Configurations;

public class SmsPlanetConfiguration: IConfiguration
{
    public string BaseUrl { get; set; }
    public string ApiKey { get; set; }
    public string From { get; set; }
}