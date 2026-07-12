using LibraryManagementSystem.Api.Extensions;
using LibraryManagementSystem.Application.DependencyInjection;
using LibraryManagementSystem.Persistence.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddPersistenseServices(builder.Configuration)
                .AddApplicationServices()
                .AddIdentity()
                .AddIdentityAuth(builder.Configuration);

var app = builder.Build();

app.UseHttpsRedirection();
app.AddIdentityAuthMiddlewares();

app.MapControllers();

app.Run();
