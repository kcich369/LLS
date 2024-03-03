using LLS.Identity.Database.IdentityModels;
using LLS.Identity.Domain.Dtos;
using LLS.Identity.Domain.Enumerations;
using LLS.Identity.Domain.ExternalServices;
using LLS.Identity.Domain.Interfaces;
using LLS.Identity.Domain.Results;
using Microsoft.AspNetCore.Identity;

namespace LLS.Identity.Infrastructure.Services;

public class UserEmailAndPhoneVerificationService : IUserEmailAndPhoneVerificationService
{
    private readonly UserManager<User> _userManager;
    private readonly IUserTokenService _userTokenService;
    private readonly IRandomStringGenerator _randomStringGenerator;
    private readonly IEmailService _emailService;
    private readonly ISmsService _smsService;

    private static TimeSpan _expirationSpan = new TimeSpan(1, 0, 0);


    public UserEmailAndPhoneVerificationService(UserManager<User> userManager,
        IUserTokenService userTokenService,
        IRandomStringGenerator randomStringGenerator,
        IEmailService emailService,
        ISmsService smsService)
    {
        _userManager = userManager;
        _userTokenService = userTokenService;
        _randomStringGenerator = randomStringGenerator;
        _emailService = emailService;
        _smsService = smsService;
    }

    public async Task<IResult<bool>> Send(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user is null)
            return Result<bool>.Error($"User with given id {userId} does not exist.");
        await ProcessEmailToken(user);
        await ProcessSendSmsToken(user);
        return Result<bool>.Success(true);
    }

    public async Task<IResult<bool>> Confirm(string userId, string emailToken, string phoneToken)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user is null)
            return Result<bool>.Error($"User with given id {userId} does not exist.");
        var emailActiveToken =
            await _userTokenService.GetActive(user, UserTokenEnum.EmailConfirmation, _expirationSpan);
        var phoneActiveToken =
            await _userTokenService.GetActive(user, UserTokenEnum.PhoneConfirmation, _expirationSpan);
        
        if (!string.IsNullOrEmpty(emailActiveToken) || string.IsNullOrEmpty(phoneActiveToken))
            return Result<bool>.Error("Inactive tokens");
        if (!string.Equals(emailToken, emailActiveToken) || !string.Equals(phoneToken, phoneActiveToken))
            return Result<bool>.Error("Incorrect tokens");
        
        await _userManager.UpdateAsync(user.Confirmed());
        await _userTokenService.Remove(user, UserTokenEnum.EmailConfirmation);
        await _userTokenService.Remove(user, UserTokenEnum.PhoneConfirmation);
        return Result<bool>.Success(true);
    }

    public async Task<IResult<bool>> ReSend(string userId, string emailUserToken, string phoneUserToken)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user is null)
            return Result<bool>.Error($"User with given id {userId} does not exist.");

        var emailActiveToken =
            await _userTokenService.GetActive(user, UserTokenEnum.EmailConfirmation, _expirationSpan);
        var phoneActiveToken =
            await _userTokenService.GetActive(user, UserTokenEnum.PhoneConfirmation, _expirationSpan);
        if (!string.IsNullOrEmpty(emailActiveToken) || string.IsNullOrEmpty(phoneActiveToken))
            return Result<bool>.Error("Tokens are active");

        var emailToken = await _userTokenService.Get(user, UserTokenEnum.EmailConfirmation);
        var phoneToken = await _userTokenService.Get(user, UserTokenEnum.PhoneConfirmation);
        if (!string.IsNullOrEmpty(emailToken) || string.IsNullOrEmpty(phoneToken))
            return Result<bool>.Error("Incorrect tokens");

        if (!string.Equals(emailUserToken, emailToken) || !string.Equals(phoneUserToken, phoneToken))
            return Result<bool>.Error("Incorrect tokens");

        await _userTokenService.Remove(user, UserTokenEnum.EmailConfirmation);
        await _userTokenService.Remove(user, UserTokenEnum.PhoneConfirmation);

        var result = await Send(userId);
        return result.IsError ? Result<bool>.Error(result.ErrorMessage) : Result<bool>.Success(true);
    }

    private async Task<IResult<string>> ProcessEmailToken(User user)
    {
        var emailToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);
        await _userTokenService.Set(user, UserTokenEnum.EmailConfirmation, emailToken);
        await _emailService.Send(new EmailData());
        return Result<string>.Success(emailToken);
    }

    private async Task<IResult<string>> ProcessSendSmsToken(User user)
    {
        var phoneToken = _randomStringGenerator.Generate(6);
        await _userTokenService.Set(user, UserTokenEnum.PhoneConfirmation, phoneToken);
        await _smsService.SendSms(new SmsData());
        return Result<string>.Success(phoneToken);
    }
}