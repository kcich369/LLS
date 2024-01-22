using LLS.Identity.Domain.Dtos;
using Microsoft.AspNetCore.Identity;

namespace LLS.Identity.Database.IdentityModels;

public class User : IdentityUser
{
    public string Name { get; set; }

    public string Surname { get; set; }
    
    public Address Address { get; set; }

    private User()
    {
    }
    
    private User(string userName, string email, string phoneNumber, string name, string surname, Address address)
    {
    }

    public static User Create(string userName, string email, string phoneNumber, string name, string surname, Address address) =>
        new User(userName, email, phoneNumber, name, surname, address);


    public UserData ToUserData() => new UserData()
        { Id = Id, UserName = UserName, Email = Email, PhoneNumber = PhoneNumber };
}