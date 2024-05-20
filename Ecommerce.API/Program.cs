using System.Configuration;
using System.Globalization;
using Ecommerce.API.Controller;
using Ecommerce.API.Extensions;
using Ecommerce.API.Middleware;
using Ecommerce.Application;
using Ecommerce.Application.Services;
using Ecommerce.Application.Services.Interfaces;
using Ecommerce.Application.Services.Interfaces.Estoque;
using Ecommerce.Application.Services.Interfaces.Pedido;
using Ecommerce.Application.Services.Interfaces.Pessoas;
using Ecommerce.Domain.Entities.Produtos;
using Ecommerce.Domain.Interfaces.EFRepository;
using Ecommerce.Domain.Interfaces.Repository;
using Ecommerce.Domain.Services;
using Ecommerce.Infra.Auth.Extensions;
using Ecommerce.Infra.Dapper.Extensions;
using Ecommerce.Infra.Dapper.Seed;
using Ecommerce.Infra.Entity.Repositories;
using Ecommerce.Infra.Entity.Repository;
using Ecommerce.Infra.Logging.Logging;
using Ecommerce.Infra.ServiceBus.Interface;
using Ecommerce.Infra.ServiceBus.Service;
using FluentValidation;
using MassTransit;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.Azure.Amqp;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using IHost = Microsoft.Extensions.Hosting.IHost;


var builder = WebApplication.CreateBuilder(args);

ValidatorOptions.Global.LanguageManager.Culture = new CultureInfo("pt-BR");
builder.Services.AddControllers();
builder.Services.AddDocumentacaoApi();

builder.Services
    .AddRepositories()
    .AddAutenticacaoJwt(builder.Configuration)
    .AddValidatorsFromAssemblyContaining<IApplicationAssemblyMarker>()
    .AddScoped<IClienteService, ClienteService>()
    .AddScoped<IFuncionarioService, FuncionarioService>()
    .AddScoped<IPedidoService, PedidoService>()
    .AddScoped<IEstoqueService, EstoqueService>()
    .AddScoped<ExceptionMiddleware>()
    .AddScoped<IServiceBus, ServiceBus>()
    //.AddScoped(typeof(Ecommerce.Domain.Interfaces.EFRepository.IRepository<>), typeof(Repository<>))

    .AddScoped<IFabricanteEfRepository, FabricanteEfRepository>()
    .AddScoped<ICategoriaEfRepository, CategoriaEfRepository>()
    .AddScoped<IProdutoEfRepository, ProdutoEfRepository>()
    .AddAppServices();


//typeof(IRepository<>), typeof(Repository<>)
builder.Logging.ClearProviders()
    .AddProvider(new CustomLoggerProvider(new CustomLoggerProviderConfiguration(), builder.Configuration));


IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        var configuration = builder.Configuration;
        var connServiceBus = "Endpoint=sb://sb-fiap-tech04.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=g9DcrQR/EIOLMt6TyLyHNK+LVzha4dTjz+ASbKdD1o4=";
        //configuration.GetSection("MassTransit:ServiceBus:ConnectionString").Value;

        //var serviceProvider = new ServiceCollection()

        builder.Services.AddMassTransit()
        .AddMassTransit((x =>
        {
            x.UsingAzureServiceBus((context, cfg) =>
            {
                cfg.Host(connServiceBus);

                cfg.ConfigureEndpoints(context);
            });
        }));

        builder.Services.AddApplicationInsightsTelemetry(builder.Configuration["ApplicationInsights:InstrumentationKey"]);
    }).Build();

var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(config.GetConnectionString("Ecommerce"));

}, ServiceLifetime.Scoped);


//builder.Services.AddDbContext<ApplicationDbContext>(x => x.UseSqlServer(Configuration.GetConnectionString("Ecommerce")));

var app = builder.Build();

app.UseDocumentacaoApi();
if (app.Environment.IsDevelopment())
{
    await app.SeedDatabase();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMiddleware<ExceptionMiddleware>();

app.MapControllers();

app.Run();
