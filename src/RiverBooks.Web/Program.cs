using RiverBooks.Books;
using FastEndpoints;
using RiverBooks.Users;
using Serilog;
using FastEndpoints.Security;
using FastEndpoints.Swagger;
using System.Reflection;

var logger = Log.Logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .CreateLogger();

logger.Information("Starting web host");

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((_, config) =>
  config.ReadFrom.Configuration(builder.Configuration));

builder.Services.AddFastEndpoints()
  .AddAuthenticationJwtBearer(o =>
  {
    o.SigningKey = builder.Configuration["Auth:JwtSecret"];
  })
  .AddAuthorization()
  .SwaggerDocument();

// Add Module Services
List<Assembly> mediatRAssemblies = [typeof(Program).Assembly];
builder.Services.AddBookServices(builder.Environment, logger, mediatRAssemblies);
builder.Services.AddUserModuleServices(builder.Environment, logger, mediatRAssemblies);

// Set MediatR
builder.Services.AddMediatR(cfg =>
{
  cfg.RegisterServicesFromAssemblies(mediatRAssemblies.ToArray());
});

var app = builder.Build();

app.UseAuthentication()
  .UseAuthorization();

app.UseFastEndpoints()
  .UseSwaggerGen();

await app.RunAsync();

public partial class Program { } //needed for tests
