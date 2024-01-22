using LLS.Identity.Domain.Dtos;
using LLS.Identity.Domain.Results;

namespace LLS.Identity.Domain.Interfaces;

public interface IRegisterService
{
    Task<IResult<string>> Reqister(RegisterUser userData);
}