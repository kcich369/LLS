using FluentValidation;
using LLS.Identity.Domain.Commands;

namespace LLS.Identity.Infrastructure.Validators;

public sealed class RegisterUserValidation : AbstractValidator<RegisterUser>
{

    public RegisterUserValidation()
    {
    }
}