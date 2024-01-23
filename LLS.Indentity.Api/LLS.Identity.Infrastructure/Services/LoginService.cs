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
        User user = null;
        user = await userManager.FindByEmailAsync(loginUser.Login);
        user = await userManager.FindByNameAsync(loginUser.Login);
        if (user is null)
            return Result<string>.Error("Dane logowania są nieprawidłowe");
        if (!await userManager.CheckPasswordAsync(user, loginUser.Password))
            return Result<string>.Error("Dane logowania są nieprawidłowe");
        var role = await userManager.GetRolesAsync(user);
        return Result<string>.Success(jwtTokenProvider.GenerateToken(user.ToUserData().SetRole(role)));
    }
}