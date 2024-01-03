using Microsoft.AspNetCore.Identity;

namespace LLS.Identity.Database.Models;

public class User : IdentityUser
{
    public string City { get; set; }
    public string Voivodeship { get; set; }
    public string Country { get; set; }
}