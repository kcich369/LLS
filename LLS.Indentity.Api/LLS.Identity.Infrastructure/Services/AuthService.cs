using LLS.Identity.Database.Commands;
using LLS.Identity.Domain.Interfaces;

namespace LLS.Identity.Infrastructure.Services;

public sealed class AuthService : IAuthService
{
    public Task<bool> Login(LoginUser loginUser)
    {
        throw new NotImplementedException();
    }

    public Task<bool> Register(RegisterUser registerUser)
    {
        throw new NotImplementedException();
    }
}