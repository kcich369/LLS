using LLS.Identity.Database.Commands;
using LLS.Identity.Database.IdentityModels;
using LLS.Identity.Domain.Interfaces;
using LLS.Identity.Domain.Results;
using Microsoft.AspNetCore.Identity;

namespace LLS.Identity.Infrastructure.Services;

public class LoginService(UserManager<User> userManager, IJwtTokenProvider jwtTokenProvider) : ILoginService
{
    public async Task<IResult<string>> Login(LoginUser loginUser)
    {
        var user = await userManager.FindByEmailAsync(loginUser.Login);
        if (user is null)
            return Result<string>.Error("Dane logowania są nieprawidłowe");
        if (await userManager.CheckPasswordAsync(user, loginUser.Password))
            return Result<string>.Error("Dane logowania są nieprawidłowe");
        return Result<string>.Success(jwtTokenProvider.GenerateToken(user.ToUserData()));
    }
}