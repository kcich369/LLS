using LLS.Identity.Database.Commands;
using LLS.Identity.Database.IdentityModels;
using LLS.Identity.Domain.Enumerations.ApiResponseEnumeration;
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
            return Result<string>.Error(UserAuthApiResTypesEnumerations.InvalidLoginData);
        user = await userManager.FindByNameAsync(loginUser.Login);
        if (user is null)
            return Result<string>.Error(UserAuthApiResTypesEnumerations.InvalidLoginData);
        if (!await userManager.CheckPasswordAsync(user, loginUser.Password))
            return Result<string>.Error(UserAuthApiResTypesEnumerations.InvalidLoginData);
        
        var role = await userManager.GetRolesAsync(user);
        return Result<string>.Success(jwtTokenProvider.GenerateToken(user.ToUserData().SetRole(role)));
    }
}