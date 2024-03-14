using Ecommerce.Consumer.Background.Queues;
using MassTransit;
using IHost = Microsoft.Extensions.Hosting.IHost;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

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
            x.UsingAzureServiceBus((context, cfg) =>
            {
                cfg.Host(connServiceBus);

                cfg.ReceiveEndpoint("filateste", e =>
                {
                    e.Consumer<CategoriaQueue>();
                    
                    
                });

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
