using LLS.Identity.Database.Context;
using LLS.Identity.Database.Extensions;
using LLS.Identity.Database.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<LlsIdentityDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("LlsIdentityDb")));

// builder.Services
//     .AddAuthorization()
//     .AddIdentityApiEndpoints<User>()
//     .AddEntityFrameworkStores<LlsIdentityDbContext>();

builder.Services
    .AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<LlsIdentityDbContext>()
    .AddDefaultTokenProviders();

builder.Services
    .AddAuthorization()
    .AddAuthentication();
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


// app.MapIdentityApi<User>();
app.UseAuthentication()
    .UseAuthorization();

app.UseHttpsRedirection();

await builder.Services
    .MigrateDatabase();

app.Run();