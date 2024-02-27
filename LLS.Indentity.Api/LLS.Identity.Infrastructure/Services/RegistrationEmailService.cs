using LLS.Identity.Domain.Dtos;
using LLS.Identity.Domain.Interfaces;
using LLS.Identity.Domain.Results;

namespace LLS.Identity.Infrastructure.Services;

public class RegistrationEmailService(IEmailService emailService) : IRegistrationEmailService
{
    private readonly IEmailService _emailService = emailService;

    public async Task<IResult<bool>> RegistrationEmail(UserEmailRegData userEmailData)
    {
        await _emailService.Send(new EmailData() {EmailTo = userEmailData.Email,Subject = "Rejestracja do systemu LLS", HtmlMessage = "Treść wiadomości"});
        return Result<bool>.Success(true);
    }
}