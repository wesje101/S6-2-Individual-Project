using AccountService;
using AccountService.DAL;
using AccountService.Messaging;
using AccountService.Models.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
string connectionString = builder.Configuration.GetConnectionString("PostgressConnectionString");
builder.Services.AddDbContext<AccountContext>(options => options.UseNpgsql(
    connectionString,
    x => x.MigrationsAssembly("AccountService")));

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