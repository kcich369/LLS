using LLS.Identity.Database.Commands;
using LLS.Identity.Domain.Commands;
using LLS.Identity.Domain.Results;

namespace LLS.Identity.Domain.Interfaces;

public interface IAuthService
{
    public Task<IResult<bool>> Register(RegisterUser registerUser);
}