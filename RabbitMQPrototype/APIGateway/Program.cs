using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);
string? runningEnvironment = Environment.GetEnvironmentVariable("HOSTED_ENVIRONMENT");
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
switch (runningEnvironment)
{
    case ("docker"):
        builder.Configuration.SetBasePath(builder.Environment.ContentRootPath)
            .AddJsonFile("Ocelot.Development.json", optional: false, reloadOnChange: true)
            .AddEnvironmentVariables();
        break;
    case ("kubernetes"):
        builder.Configuration.SetBasePath(builder.Environment.ContentRootPath)
            .AddJsonFile("Ocelot.Kubernetes.json", optional: false, reloadOnChange: true)
            .AddEnvironmentVariables();
        break;
    default:
        builder.Configuration.SetBasePath(builder.Environment.ContentRootPath)
            .AddJsonFile("Ocelot.Development.json", optional: false, reloadOnChange: true)
            .AddEnvironmentVariables();
        break;
}

builder.Services.AddOcelot(builder.Configuration);

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

await app.UseOcelot();

app.Run();