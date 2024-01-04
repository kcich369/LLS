using LLS.Identity.Database.IdentityModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LLS.Identity.Database.Context;

public class LlsIdentityDbContext : IdentityDbContext<User>
{
    public LlsIdentityDbContext(DbContextOptions<LlsIdentityDbContext> options) : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(LlsIdentityDbContext).Assembly);
    }
}