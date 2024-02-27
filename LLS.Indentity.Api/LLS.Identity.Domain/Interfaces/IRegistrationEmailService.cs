using LLS.Identity.Domain.Dtos;
using LLS.Identity.Domain.Results;

namespace LLS.Identity.Domain.Interfaces;

public interface IRegistrationEmailService
{
    Task<IResult<bool>> RegistrationEmail(UserEmailRegData userEmailData);
}