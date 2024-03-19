using Ecommerce.Domain.Entities.Produtos;
using Ecommerce.Domain.Repository;
using MassTransit;

namespace Ecommerce.Consumer.Background.Queues.ProdutoQueue
{
    public class ProdutoUpdateQueue : IConsumer<Produto>
    {
        private readonly IProdutoRepository _repository;
        public ProdutoUpdateQueue(IProdutoRepository produtoRepository)
        {
            _repository = produtoRepository;
        }
        public Task Consume(ConsumeContext<Produto> context)
        {
            _repository.Alterar(context.Message);

            return Task.CompletedTask;
        }
    }
}
