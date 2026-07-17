using FluentValidation.AspNetCore;
using LibraryManagementSystem.Api.Extensions;
using LibraryManagementSystem.Application;
using LibraryManagementSystem.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddPersistenceServices(builder.Configuration)
                .AddApplicationServices()
                .AddIdentity()
                .AddIdentityAuth(builder.Configuration);

var app = builder.Build();

app.UseHttpsRedirection();
app.AddIdentityAuthMiddlewares();

await app.SeedRolesAsync();
await app.SeedAdminUserAsync(builder.Configuration);

app.MapControllers();

app.Run();
