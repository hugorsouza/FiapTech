using Ecommerce.Domain.Entities.Produtos;
using Ecommerce.Domain.Repository;
using MassTransit;

namespace Ecommerce.Consumer.Background.Queues.CategoriaQueue
{
    public class CategoriaUpdateQueue : IConsumer<Categoria>
    {
        private readonly ICategoriaRepository _repository;
        public CategoriaUpdateQueue(ICategoriaRepository categoriaRepository)
        {
            _repository = categoriaRepository;
        }
        public Task Consume(ConsumeContext<Categoria> context)
        {
            _repository.Alterar(context.Message);

            return Task.CompletedTask;
        }
    }
}
