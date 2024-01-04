using LLS.Identity.Database.Context;
using LLS.Identity.Database.Extensions;
using LLS.Identity.Database.IdentityModels;
using LLS.Identity.Domain.Configurations;
using LLS.Identity.Infrastructure;
using LLS.Identity.Infrastructure.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

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

builder.Services.AddSingleton<JwtConfiguration>(builder.Configuration);

builder.Services.AddAuthentication(builder.Configuration.BindSection<JwtConfiguration>("JWT"));

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