using AccountService;
using AccountService.DAL;
using AccountService.Messaging;
using AccountService.Models.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
string? runningEnvironment = Environment.GetEnvironmentVariable("HOSTED_ENVIRONMENT");

// Switch database depending on where we're running.
// While running locally or debugging, an in-memory database is used.
// When running (locally) in docker, a dockerized postgres database is used.
// When running in kubernetes, a cloud database is used.
switch (runningEnvironment)
{
    case ("docker"):
        string connectionString = builder.Configuration.GetConnectionString("PostgressConnectionString");
        builder.Services.AddDbContext<AccountContext>(options => options.UseNpgsql(
            connectionString, 
            x => x.MigrationsAssembly("AccountService")));
        break;
    case ("kubernetes"):
        builder.Services.AddDbContext<AccountContext>(options => options.UseInMemoryDatabase("AccountService"));
        break;
    default:
        builder.Services.AddDbContext<AccountContext>(options => options.UseInMemoryDatabase("AccountService"));
        break;
}

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IAccountLogic, AccountLogic>();
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddHostedService<MessageBusListener>();

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