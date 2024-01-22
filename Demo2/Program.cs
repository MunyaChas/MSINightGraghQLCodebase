using Demo2.Common;
using Demo2.Data;
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
    .AddDbContextPool<Demo2DbContext>(
        o => o.UseSqlServer(defaultConnectionString).UseLoggerFactory(CreateLoggerFactory()).EnableSensitiveDataLogging());
services.AddCors();
services.AddGraphQLServer()
        .AddFiltering()
        .AddSorting()
        .AddTypes()
        .AddGlobalObjectIdentification()
        .AddProjections()
        .RegisterDbContext<Demo2DbContext>();
var app = builder.Build();

//app.MapGet("/", () => "Hello World!");
app.MapGraphQL();
app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
app.Run();

static ILoggerFactory CreateLoggerFactory() =>
    LoggerFactory.Create(builder =>
    {
        builder.AddFilter((category, level) => category == DbLoggerCategory.Database.Command.Name && level == LogLevel.Information).AddConsole();
    });