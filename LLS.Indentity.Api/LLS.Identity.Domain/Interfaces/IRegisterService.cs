using LLS.Identity.Domain.Commands;
using LLS.Identity.Domain.Results;

namespace LLS.Identity.Domain.Interfaces;

public interface IRegisterService
{
    Task<IResult<string>> Reqister(RegisterUser usrData);
}