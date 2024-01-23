using LLS.Identity.Database.Commands;
using LLS.Identity.Database.IdentityModels;
using LLS.Identity.Domain.Commands;
using LLS.Identity.Domain.Interfaces;
using LLS.Identity.Domain.Results;
using Microsoft.AspNetCore.Identity;

namespace LLS.Identity.Infrastructure.Services;

public sealed class AuthService : IAuthService
{
    private readonly UserManager<User> _userManager;

    public AuthService(UserManager<User> userManager, IJwtTokenProvider jwtTokenProvider)
    {
        _userManager = userManager;
    }

    public Task<IResult<bool>> Register(RegisterUser registerUser)
    {
        throw new NotImplementedException();
    }
}