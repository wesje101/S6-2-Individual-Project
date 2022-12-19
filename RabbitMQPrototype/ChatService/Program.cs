using ChatService;
using ChatService.DAL;
using ChatService.Messaging;
using ChatService.Models.Factories;
using ChatService.Models.Interfaces;
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
        builder.Services.AddDbContext<ChatContext>(options => options.UseNpgsql(
            connectionString, 
            x => x.MigrationsAssembly("AuthService")));
        break;
    case ("kubernetes"):
        builder.Services.AddDbContext<ChatContext>(options => options.UseInMemoryDatabase("AuthService"));
        break;
    default:
        builder.Services.AddDbContext<ChatContext>(options => options.UseInMemoryDatabase("AuthService"));
        break;
}

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IChatLogic, ChatLogic>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IChatMessageRepository, ChatMessageRepository>();
builder.Services.AddScoped<IChatRoomRepository, ChatRoomRepository>();
builder.Services.AddScoped<IChatMessageFactory, ChatMessageFactory>();


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