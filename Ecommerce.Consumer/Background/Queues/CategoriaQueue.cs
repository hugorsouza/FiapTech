using Ecommerce.Domain.Entities.Produtos;
using MassTransit;

namespace Ecommerce.Consumer.Background.Queues
{
    public class CategoriaQueue : IConsumer<Categoria>
    {
        public Task Consume(ConsumeContext<Categoria> context)
        {
            Console.WriteLine(context.Message.Nome);

            var teste = context.Message;

            return Task.CompletedTask;
        }
    }
}
