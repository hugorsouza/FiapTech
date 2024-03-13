using Ecommerce.Domain.Entities.Produtos;
using MassTransit;

namespace Ecommerce.Consumer.Background.Queues
{
    public class FabricanteQueue : IConsumer<Fabricante>
    {
        public Task Consume(ConsumeContext<Fabricante> context)
        {
            Console.WriteLine(context.Message);

            return Task.CompletedTask;
        }
    }
}
