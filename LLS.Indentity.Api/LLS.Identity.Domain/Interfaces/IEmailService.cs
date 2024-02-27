using LLS.Identity.Domain.Dtos;
using LLS.Identity.Domain.Results;

namespace LLS.Identity.Domain.Interfaces;

public interface IEmailService
{
    Task<IResult<bool>> Send(EmailData emailData);
}