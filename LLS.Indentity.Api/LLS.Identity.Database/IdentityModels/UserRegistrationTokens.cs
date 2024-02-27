using System.Security.Cryptography;

namespace LLS.Identity.Database.IdentityModels;

public class UserRegistrationTokens
{
    public int Id { get; private set; }
    public string EmailToken { get; private set; }
    public string PhoneToken { get; private set; }
    public bool Confirmed { get; private set; }
    
    public DateTimeOffset CreatedAt { get; private set; }
    public DateTimeOffset UpdatedAt { get; private set; }
        
    public string UserId { get; private set; }
    public User User { get; private set; }

    private UserRegistrationTokens()
    {
    }
    
    private UserRegistrationTokens(string emailToken, string phoneToken, string userId)
    {
        EmailToken = emailToken;
        PhoneToken = phoneToken;
        CreatedAt = DateTimeOffset.Now;
    }

    public static UserRegistrationTokens Create() => new UserRegistrationTokens();
    
    public UserRegistrationTokens SetAsConfirmed()
    {
        Confirmed = true;
        UpdatedAt = DateTimeOffset.Now;
        return this;
    }
}