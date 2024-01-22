namespace LLS.Identity.Database.IdentityModels;

public class Address
{
    public string Street { get; set; }
    public string BuildingNumber { get; set; }
    public string City { get; set; }
    public string Voivodeship { get; set; }
    public string Country { get; set; }

    private Address()
    {
    }

    public Address(string street, string buildingNumber, string city, string voivodeship, string country)
    {
        Street = street;
        BuildingNumber = buildingNumber;
        City = city;
        Voivodeship = voivodeship;
        Country = country;
    }
}