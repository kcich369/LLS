using LLS.Identity.Database.Context;
using LLS.Identity.Database.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<LlsIdentityDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("LlsIdentityDb"),x => x.MigrationsAssembly("LLS.Identity.Database")));

builder.Services
    .AddAuthorization()
    .AddIdentityApiEndpoints<User>()
    .AddEntityFrameworkStores<LlsIdentityDbContext>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.MapIdentityApi<User>();
app.UseHttpsRedirection();

app.Run();