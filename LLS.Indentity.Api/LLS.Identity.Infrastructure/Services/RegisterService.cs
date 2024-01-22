using FluentValidation;
using LLS.Identity.Database.IdentityModels;
using LLS.Identity.Domain.Dtos;
using LLS.Identity.Domain.Interfaces;
using LLS.Identity.Domain.Results;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LLS.Identity.Infrastructure.Services;

public sealed class RegisterService(
    UserManager<User> userManager,
    IValidator<RegisterUser> validator)
    : IRegisterService
{
    public async Task<IResult<string>> Reqister(RegisterUser userData)
    {
        var validation = await validator.ValidateAsync(userData);
        if (!validation.IsValid)
            return Result<string>.Error(string.Join(';',
                validation.Errors.Select(x => $"{x.PropertyName}: {x.ErrorMessage}")));
        var userEmail = await userManager.FindByEmailAsync(userData.Email);
        if (userEmail is not null)
            return Result<string>.Error("User with given email already exists");
        var userLogin = await userManager.FindByNameAsync(userData.UserName);
        if (userLogin is not null)
            return Result<string>.Error("User with given login already exists");
        var userPhoneNumber = await userManager.Users.FirstOrDefaultAsync(u => u.PhoneNumber == userData.PhoneNumber);
        if (userPhoneNumber is not null)
            return Result<string>.Error("User with given phone number already exists");

        var user = User.Create(userData.UserName, userData.Email, userData.PhoneNumber, userData.Name, userData.Surname,
            new Address(userData.Street, userData.BuildingNumber, userData.City, userData.Voivodeship,
                userData.Country));
        await userManager.CreateAsync(user, userData.Password);
        return Result<string>.Success("User was created");
    }
}