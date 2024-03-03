using LLS.Identity.Domain.Dtos;
using LLS.Identity.Domain.Results;

namespace LLS.Identity.Domain.Interfaces;

public interface IUserEmailAndPhoneVerificationService
{
    Task<IResult<bool>> Send(string userId);
    Task<IResult<bool>> Confirm(string userId, string emailToken, string phoneToken);
}