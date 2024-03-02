using LLS.Identity.Domain.Dtos;
using LLS.Identity.Domain.Results;

namespace LLS.Identity.Domain.ExternalServices;

public interface ISmsService
{
    Task<IResult<bool>> SendSms(SmsData smsData);
}