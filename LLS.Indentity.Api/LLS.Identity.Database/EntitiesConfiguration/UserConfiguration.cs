using LLS.Identity.Database.IdentityModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LLS.Identity.Database.EntitiesConfiguration;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(x => x.City).HasMaxLength(150);
        builder.Property(x => x.Voivodeship).HasMaxLength(150);
        builder.Property(x => x.Country).HasMaxLength(150);
    }
}