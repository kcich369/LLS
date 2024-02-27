using LLS.Identity.Database.IdentityModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LLS.Identity.Database.EntitiesConfiguration;

public class UserRegistrationTokensConfiguration: IEntityTypeConfiguration<UserRegistrationTokens>
{
    public void Configure(EntityTypeBuilder<UserRegistrationTokens> builder)
    {
        builder.Property(x => x.EmailToken).HasMaxLength(25);
        builder.Property(x => x.PhoneToken).HasMaxLength(6);

        builder.HasOne(x => x.User)
            .WithOne()
            .HasForeignKey<UserRegistrationTokens>(x => x.UserId);
    }
}