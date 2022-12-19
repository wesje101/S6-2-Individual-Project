using AuthService;
using AuthService.DAL;
using AuthService.Messaging;
using AuthService.Models.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
string? runningEnvironment = Environment.GetEnvironmentVariable("HOSTED_ENVIRONMENT");

// Switch database depending on where we're running.
// While running locally or debugging, an in-memory database is used.
// When running (locally) in docker, a dockerized postgres database is used.
// When running in kubernetes, a cloud database is used.
switch (runningEnvironment)
{
    case ("docker"):
        string connectionString = builder.Configuration.GetConnectionString("PostgressConnectionString");
        builder.Services.AddDbContext<AuthContext>(options => options.UseNpgsql(
            connectionString, 
            x => x.MigrationsAssembly("AuthService")));
        break;
    case ("kubernetes"):
        builder.Services.AddDbContext<AuthContext>(options => options.UseInMemoryDatabase("AuthService"));
        break;
    default:
        builder.Services.AddDbContext<AuthContext>(options => options.UseInMemoryDatabase("AuthService"));
        break;
}
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IAuthLogic, AuthLogic>();
builder.Services.AddScoped<IAuthRepository, AuthRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();