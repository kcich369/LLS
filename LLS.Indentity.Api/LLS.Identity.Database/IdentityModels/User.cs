using LLS.Identity.Domain.Dtos;
using Microsoft.AspNetCore.Identity;

namespace LLS.Identity.Database.IdentityModels;

public class User : IdentityUser
{
    public string City { get; set; }
    public string Voivodeship { get; set; }
    public string Country { get; set; }

    public UserData ToUserData() => new UserData() { Id = Id,UserName = UserName, Email = Email, PhoneNumber = PhoneNumber };
}