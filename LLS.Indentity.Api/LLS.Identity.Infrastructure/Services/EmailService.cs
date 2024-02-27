using LLS.Identity.Domain.Dtos;
using LLS.Identity.Domain.Interfaces;
using LLS.Identity.Domain.Results;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace LLS.Identity.Infrastructure.Services;

public sealed class EmailService(ISendGridClient sendGridClient) : IEmailService
{
    private readonly ISendGridClient _sendGridClient = sendGridClient;


    public async Task<IResult<bool>> Send(EmailData emailData)
    {
       var msg = MailHelper.CreateSingleEmail(new EmailAddress(emailData.EmailTo), new EmailAddress(emailData.EmailTo),
            emailData.Subject, null, emailData.HtmlMessage);
       msg.AddBccs(emailData.Bccs.Select(email =>new EmailAddress(email)).ToList());
       await _sendGridClient.SendEmailAsync(msg);
       return Result<bool>.Success(true);
    }
}