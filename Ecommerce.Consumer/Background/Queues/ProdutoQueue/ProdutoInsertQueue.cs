using Ecommerce.Domain.Entities.Estoque;
using Ecommerce.Domain.Entities.Produtos;
using Ecommerce.Domain.Repository;
using Ecommerce.Infra.Dapper.Repositories;
using MassTransit;

namespace Ecommerce.Consumer.Background.Queues.ProdutoQueue
{
    public class ProdutoInsertQueue : IConsumer<Produto>
    {
        private readonly IProdutoRepository _repository;
        public ProdutoInsertQueue(IProdutoRepository produtoRepository)
        {
            _repository = produtoRepository;
        }
        public Task Consume(ConsumeContext<Produto> context)
        {
             _repository.Cadastrar(context.Message);

            return Task.CompletedTask;
        }
    }
}
