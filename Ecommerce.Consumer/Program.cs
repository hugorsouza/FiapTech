using Ecommerce.Consumer.Background.Queues.CategoriaQueue;
using Ecommerce.Domain.Interfaces;
using Ecommerce.Domain.Repository;
using Ecommerce.Infra.Dapper.Factory;
using Ecommerce.Infra.Dapper.Interfaces;
using Ecommerce.Infra.Dapper.Repositories;
using Ecommerce.Infra.Dapper.Services;
using MassTransit;
using IHost = Microsoft.Extensions.Hosting.IHost;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddScoped<IDbConnectionFactory, DbConnectionFactory>()
                .AddScoped<UnitOfWork>()
                .AddScoped<ITransactionService>(sp => sp.GetService<UnitOfWork>())
                .AddScoped<IUnitOfWork>(sp => sp.GetService<UnitOfWork>())
                .AddScoped<ICategoriaRepository, CategoriaRepository>();

builder.Services.AddControllers();


IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        var configuration = builder.Configuration;
        var connServiceBus = "Endpoint=sb://sb-fiap-tech.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=Ql9xGRVUDFW3XcKYq/spXyQ+tZcstUVMC+ASbF+wlOs=";
        //configuration.GetSection("MassTransit:ServiceBus:ConnectionString").Value;

        //var serviceProvider = new ServiceCollection()

        builder.Services
        .AddMassTransit((x =>
        {
            x.AddConsumer<CategoriaInsertQueue>();
            x.AddConsumer<CategoriaUpdateQueue>();

            x.UsingAzureServiceBus((context, cfg) =>
            {
                cfg.Host(connServiceBus);

                cfg.ReceiveEndpoint("categoriainsertqueue", e => { e.ConfigureConsumer<CategoriaInsertQueue>(context); });

                cfg.ReceiveEndpoint("categoriaupdatequeue", e => { e.ConfigureConsumer<CategoriaUpdateQueue>(context); });

            });

        }));

       
    }).Build();




builder.Services.AddMassTransitHostedService();


var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
