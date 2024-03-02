using LLS.Identity.Domain.Dtos;
using LLS.Identity.Domain.Results;

namespace LLS.Identity.Domain.ExternalServices;

public interface IEmailService
{
    Task<IResult<bool>> Send(EmailData emailData);
}