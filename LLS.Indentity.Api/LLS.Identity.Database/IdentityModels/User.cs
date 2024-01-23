using LLS.Identity.Domain.Dtos;
using Microsoft.AspNetCore.Identity;

namespace LLS.Identity.Database.IdentityModels;

public sealed class User : IdentityUser
{
    public string Name { get; set; }

    public string Surname { get; set; }
    
    public bool Active { get; set; }

    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }

    public Address Address { get; set; }

    private User()
    {
    }

    private User(string userName, string email, string phoneNumber, string name, string surname,
        Address address) : base()
    {
        UserName = userName;
        Email = email;
        PhoneNumber = phoneNumber;
        Name = name;
        Surname = surname;
        Address = address;
        CreatedAt = DateTimeOffset.Now;
    }

    public static User Create(string userName, string email, string phoneNumber, string name, string surname,
        Address address) =>
        new User(userName, email, phoneNumber, name, surname, address);

    public UserData ToUserData() => new()
        { Id = Id, UserName = UserName, Email = Email, PhoneNumber = PhoneNumber };
}