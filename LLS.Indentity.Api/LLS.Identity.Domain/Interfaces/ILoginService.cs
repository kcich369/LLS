using LLS.Identity.Database.Commands;
using LLS.Identity.Domain.Results;

namespace LLS.Identity.Domain.Interfaces;

public interface ILoginService
{
    Task<IResult<string>> Login(LoginUser loginUser);
}