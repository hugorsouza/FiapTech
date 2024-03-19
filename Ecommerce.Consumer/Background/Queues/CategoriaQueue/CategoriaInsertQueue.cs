using Ecommerce.Domain.Entities.Produtos;
using Ecommerce.Domain.Repository;
using MassTransit;

namespace Ecommerce.Consumer.Background.Queues.CategoriaQueue
{
    public class CategoriaInsertQueue : IConsumer<Categoria>
    {
        private readonly ICategoriaRepository _repository;
        public CategoriaInsertQueue(ICategoriaRepository categoriaRepository)
        {
            _repository = categoriaRepository;
        }
        public Task Consume(ConsumeContext<Categoria> context)
        {

            _repository.Cadastrar(context.Message);

            return Task.CompletedTask;
        }
    }
}
