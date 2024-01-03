namespace LLS.Identity.Database.Commands;

public record RegisterUser(
    string Name,
    string SecondName,
    string Email,
    string Login,
    string PhoneNumber,
    string City,
    string Voivodeship,
    string Country);