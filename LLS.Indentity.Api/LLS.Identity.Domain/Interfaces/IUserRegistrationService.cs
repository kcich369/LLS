using LLS.Identity.Domain.Commands;
using LLS.Identity.Domain.Dtos;
using LLS.Identity.Domain.Results;

namespace LLS.Identity.Domain.Interfaces;

public interface IUserRegistrationService
{
    Task<IResult<UserCreatedDto>> Reqister(RegisterUser usrData);
}