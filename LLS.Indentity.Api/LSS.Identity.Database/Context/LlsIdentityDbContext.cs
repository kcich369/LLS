using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LSS.Identity.Database.Context;

public class LlsIdentityDbContext(DbContextOptions<LlsIdentityDbContext> options)
    : IdentityDbContext<IdentityUser>(options);