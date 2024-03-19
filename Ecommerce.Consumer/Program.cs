using Ecommerce.Consumer.Background.Queues.CategoriaQueue;
using Ecommerce.Consumer.Background.Queues.ClienteQueue;
using Ecommerce.Consumer.Background.Queues.FabricanteQueue;
using Ecommerce.Consumer.Background.Queues.PedidoQueue;
using Ecommerce.Consumer.Background.Queues.ProdutoQueue;
using Ecommerce.Consumer.Background.QueuesPedidoQueue;
using Ecommerce.Domain.Interfaces;
using Ecommerce.Domain.Interfaces.Repository;
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
                .AddScoped<ICategoriaRepository, CategoriaRepository>()
                .AddScoped<IFabricanteRepository, FabricanteRepository>()
                .AddScoped<IProdutoRepository, ProdutoRepository>()
                .AddScoped<IPedidoRepository, PedidoRepository>()
                ;

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

            x.AddConsumer<FabricanteInsertQueue>();
            x.AddConsumer<FabricanteUpdateQueue>();

            x.AddConsumer<ClienteInsertQueue>();
            x.AddConsumer<ClienteUpdateQueue>();

            x.AddConsumer<ProdutoInsertQueue>();
            x.AddConsumer<ProdutoUpdateQueue>();

            x.AddConsumer<PedidoInsertQueue>();
            x.AddConsumer<PedidodeleteQueue>();

            x.UsingAzureServiceBus((context, cfg) =>
            {
                cfg.Host(connServiceBus);

                cfg.ReceiveEndpoint("categoriainsertqueue", e => { e.ConfigureConsumer<CategoriaInsertQueue>(context); });
                cfg.ReceiveEndpoint("categoriaupdatequeue", e => { e.ConfigureConsumer<CategoriaUpdateQueue>(context); });

                cfg.ReceiveEndpoint("fabricanteinsertqueue", e => { e.ConfigureConsumer<FabricanteInsertQueue>(context); });
                cfg.ReceiveEndpoint("fabricanteupdatequeue", e => { e.ConfigureConsumer<FabricanteUpdateQueue>(context); });


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
