using LLS.Identity.Database.Commands;

namespace LLS.Identity.Domain.Interfaces;

public interface IAuthService
{
    public Task<bool> Login(LoginUser loginUser);
    public Task<bool> Register(RegisterUser registerUser);
}