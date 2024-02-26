using LLS.Identity.Domain.Dtos;
using Microsoft.AspNetCore.Identity;

namespace LLS.Identity.Database.IdentityModels;

public sealed class User : IdentityUser
{
    public string Name { get; private set; }

    public string Surname { get; private set; }
    
    public bool Active { get; private set; }

    public DateTimeOffset CreatedAt { get; private set; }
    public DateTimeOffset UpdatedAt { get; private set; }

    public Address Address { get; private set; }
    
    public int RegistrationTokenId { get; private set; }
    public UserRegistrationTokens RegistrationTokens { get; private set; }

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
        RegistrationTokens = UserRegistrationTokens.Create();
    }

    public static User Create(string userName, string email, string phoneNumber, string name, string surname,
        Address address) =>
        new User(userName, email, phoneNumber, name, surname, address);

    public UserData ToUserData() => new()
        { Id = Id, UserName = UserName, Email = Email, PhoneNumber = PhoneNumber };
}