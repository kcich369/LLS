using LLS.Identity.Api;
using LLS.Identity.Database.Context;
using LLS.Identity.Database.Extensions;
using LLS.Identity.Database.IdentityModels;
using LLS.Identity.Domain.Configurations;
using LLS.Identity.Infrastructure;
using LLS.Identity.Infrastructure.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<LlsIdentityDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("LlsIdentityDb")));

builder.Services
    .AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<LlsIdentityDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddSingleton<JwtConfiguration>(builder.Configuration);
builder.Services.AddAuthentication(builder.Configuration.BindSection<JwtConfiguration>("JWT")).AddAuthorization();
builder.Services.RegisterInfrastructure();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.RegisterMinimalApis();

app.UseAuthentication();
app.UseAuthorization();

app.UseHttpsRedirection();


await builder.Services.MigrateDatabase();

app.Run();