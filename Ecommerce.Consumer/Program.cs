using Ecommerce.Consumer.Background.Queues;
using MassTransit;
using IHost = Microsoft.Extensions.Hosting.IHost;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

var app = builder.Build();



//IHost host = Host.CreateDefaultBuilder(args)
//    .ConfigureServices((hostContext, services) =>
//    {
//        var configuration = builder.Configuration;
//        var connServiceBus = configuration.GetSection("MassTransit:ServiceBus:ConnectionString").Value;

//        builder.Services.AddMassTransit((x =>
//        {
//            x.UsingAzureServiceBus((context, cfg) =>
//            {
//                cfg.Host(connServiceBus);

//                cfg.ReceiveEndpoint(configuration.GetSection("MassTransit:ServiceBus:Queues:Fabricante").Value, e =>
//                {
//                    e.Consumer<FabricanteQueue>();
//                });
                                
//            });

//            x.AddConsumer<FabricanteQueue>();
//        }));

//    }).Build();



IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        var configuration = builder.Configuration;
        var connServiceBus = configuration.GetSection("MassTransit:ServiceBus:ConnectionString").Value;

        var serviceProvider = new ServiceCollection()
        .AddMassTransit((x =>
        {
            x.UsingAzureServiceBus((context, cfg) =>
            {
                cfg.Host(connServiceBus);

                cfg.ReceiveEndpoint(configuration.GetSection("MassTransit:ServiceBus:Queues:Fabricante").Value, e =>
                {
                    e.Consumer<FabricanteQueue>();
                });

            });

            //x.AddConsumer<FabricanteQueue>();
        }));

    }).Build();



// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
