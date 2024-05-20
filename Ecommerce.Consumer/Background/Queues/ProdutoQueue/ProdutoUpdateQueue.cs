using Ecommerce.Domain.Entities.Produtos;
using Ecommerce.Domain.Interfaces.EFRepository;
using Ecommerce.Domain.Repository;
using MassTransit;

namespace Ecommerce.Consumer.Background.Queues.ProdutoQueue
{
    public class ProdutoUpdateQueue : IConsumer<Produto>
    {
        private readonly IProdutoEfRepository _repository;
        public ProdutoUpdateQueue(IProdutoEfRepository produtoRepository)
        {
            _repository = produtoRepository;
        }
        public Task Consume(ConsumeContext<Produto> context)
        {

            var entidade = context.Message;

            var obj = _repository.ObterPorId(entidade.Id);

            obj.Descricao = entidade.Descricao;
            obj.Nome = entidade.Nome;
            obj.Ativo = entidade.Ativo;
            obj.Preco = entidade.Preco;

            _repository.Alterar(obj);
       

            return Task.CompletedTask;
        }
    }
}
