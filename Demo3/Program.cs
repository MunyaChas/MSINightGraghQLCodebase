using Demo3.Common;
using Demo3.Data;
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
    .AddDbContextPool<Demo3DbContext>(
        o => o.UseSqlServer(defaultConnectionString).UseLoggerFactory(CreateLoggerFactory()).EnableSensitiveDataLogging());

services.AddCors();

services
    .AddGraphQLServer()
    .BindRuntimeType<EmployeeId, UuidType>()
    .AddTypeConverter<EmployeeId, Guid>(_ => _.Value)
    .AddTypeConverter<Guid, EmployeeId>(_ => EmployeeId.FromGuid(_))
    .BindRuntimeType<OpenRequestId, UuidType>()
    .AddTypeConverter<OpenRequestId, Guid>(_ => _.Value)
    .AddTypeConverter<Guid, OpenRequestId>(_ => OpenRequestId.FromGuid(_))
    .AddFiltering()
    .AddSorting()
    .AddTypes()
    .AddGlobalObjectIdentification()
    .AddProjections()
    .AddMutationConventions(applyToAllMutations: true)
    .RegisterDbContext<Demo3DbContext>();
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