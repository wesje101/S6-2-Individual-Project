using AuthService;
using AuthService.DAL;
using AuthService.Logic;
using AuthService.Messaging;
using AuthService.Models.Interfaces;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;

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
        builder.Services.AddDbContext<AuthContext>(options => options.UseNpgsql(
            connectionString, 
            x => x.MigrationsAssembly("AuthService")));
        break;
    case ("kubernetes"):
        //builder.Services.AddDbContext<AuthContext>(options => options.UseInMemoryDatabase("AuthService"));
        connectionString = builder.Configuration.GetConnectionString("MySQLConnectionString");
        builder.Services.AddDbContext<AuthContext>(options => options.UseSqlServer(
            connectionString, 
            x => x.MigrationsAssembly("AuthService")));
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

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy=>
    {
        policy.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

builder.Services.AddAuthentication().AddGoogle(googleOptions =>
    {
        googleOptions.ClientId = builder.Configuration["Authentication:Google:ClientId"];
        googleOptions.ClientId = builder.Configuration["Authentication:Google:ClientSecret"];
    });

var app = builder.Build();

switch (runningEnvironment)
{
    case("docker"):
        using (var scope = app.Services.CreateScope())
        {
            var services = scope.ServiceProvider;

            var context = services.GetRequiredService<AuthContext>();
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
//switch (runningEnvironment)
//{
//    case("docker"):
//        using (var scope = app.Services.CreateScope())
//        {
//            var services = scope.ServiceProvider;
//            var context = services.GetRequiredService<AuthContext>();
//            context.Database.EnsureDeleted();
//            context.Database.EnsureCreated();
//        }
//        break;
//    case("kubernetes"):
//        break;
//    default:
//        break;
//}
app.Run();