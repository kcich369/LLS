using FluentValidation;
using LLS.Identity.Domain.Dtos;

namespace LLS.Identity.Infrastructure.Validators;

public sealed class RegisterUserValidation : AbstractValidator<RegisterUser>
{

    public RegisterUserValidation()
    {
    }
}