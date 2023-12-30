using LLS.Identity.Database.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LLS.Identity.Database.Context;

public class LlsIdentityDbContext : IdentityDbContext<User>
{
    public LlsIdentityDbContext(DbContextOptions<LlsIdentityDbContext> options) : base(options)
    {
    }
}