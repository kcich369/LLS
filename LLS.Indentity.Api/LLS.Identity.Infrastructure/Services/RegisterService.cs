﻿using FluentValidation;
using LLS.Identity.Database.IdentityModels;
using LLS.Identity.Domain.Commands;
using LLS.Identity.Domain.Enumerations;
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
    public async Task<IResult<string>> Reqister(RegisterUser usrData)
    {
        var validation = await validator.ValidateAsync(usrData);
        if (!validation.IsValid)
            return Result<string>.Error(string.Join(';',
                validation.Errors.Select(x => $"{x.PropertyName}: {x.ErrorMessage}")));
        var userEmail = await userManager.FindByEmailAsync(usrData.Email);
        if (userEmail is not null)
            return Result<string>.Error("User with given email already exists");
        var userLogin = await userManager.FindByNameAsync(usrData.UserName);
        if (userLogin is not null)
            return Result<string>.Error("User with given login already exists");
        var userPhoneNumber = await userManager.Users.FirstOrDefaultAsync(u => u.PhoneNumber == usrData.PhoneNumber);
        if (userPhoneNumber is not null)
            return Result<string>.Error("User with given phone number already exists");

        var user = User.Create(usrData.UserName, usrData.Email, usrData.PhoneNumber, usrData.Name, usrData.Surname,
            new Address(usrData.Street, usrData.BuildingNumber, usrData.City, usrData.Voivodeship,
                usrData.Country, usrData.ZipCode));
        var registrationResult = await userManager.CreateAsync(user, usrData.Password);
        if (!registrationResult.Succeeded)
            return Result<string>.Error(string.Join(';',
                registrationResult.Errors.Select(x => $"{x.Code}: {x.Description}")));
        await userManager.AddToRoleAsync(user, RoleEnum.User);
        return Result<string>.Success("User was created");
    }
}