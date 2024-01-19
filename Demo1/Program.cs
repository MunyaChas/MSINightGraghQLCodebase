using Demo1.Common;
using Demo1.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
var services = builder.Services;
var appsettings = new AppSettingsConfiguration();
services.AddOptions<BogusGenerateData>().Bind(configuration.GetSection(appsettings.BogusGenerateData.Config));
services.AddOptions<ConnectionStrings>().Bind(configuration.GetSection(appsettings.ConnectionStrings.Config));

var serviceProvider = services.BuildServiceProvider();

var defaultConnectionString = serviceProvider.GetService<IOptions<ConnectionStrings>>()?.Value.DefaultConnection ?? string.Empty;

services
    .AddDbContextPool<Demo1DbContext>(
        o => o.UseSqlServer(defaultConnectionString).UseLoggerFactory(CreateLoggerFactory()).EnableSensitiveDataLogging());
services.AddCors();

// Add services for GraphQL


var app = builder.Build();
await DatabaseHelper.SeedDatabaseAsync(app);
//app.MapGraphQL();
//app.MapGet("/", () => "Hello World!");

app.Run();

static ILoggerFactory CreateLoggerFactory() =>
    LoggerFactory.Create(builder =>
    {
        builder.AddFilter((category, level) => category == DbLoggerCategory.Database.Command.Name && level == LogLevel.Information).AddConsole();
    });