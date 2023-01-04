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
string connectionString;
switch (runningEnvironment)
{
    case ("docker"):
        connectionString = builder.Configuration.GetConnectionString("PostgressConnectionString");
        builder.Services.AddDbContext<ChatContext>(options => options.UseNpgsql(
            connectionString, 
            x => x.MigrationsAssembly("ChatService")));
        break;
    case ("kubernetes"):
        //builder.Services.AddDbContext<ChatContext>(options => options.UseInMemoryDatabase("ChatService"));
        connectionString = builder.Configuration.GetConnectionString("MySQLConnectionString");
        builder.Services.AddDbContext<ChatContext>(options => options.UseSqlServer(
            connectionString, 
            x => x.MigrationsAssembly("ChatService")));
        break;
    default:
        builder.Services.AddDbContext<ChatContext>(options => options.UseInMemoryDatabase("ChatService"));
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

builder.Services.AddHostedService<MessageBusListener>();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy=>
    {
        policy.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var app = builder.Build();

switch (runningEnvironment)
{
    case("docker"):
        using (var scope = app.Services.CreateScope())
        {
            var services = scope.ServiceProvider;

            var context = services.GetRequiredService<ChatContext>();
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
        }
        break;
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();