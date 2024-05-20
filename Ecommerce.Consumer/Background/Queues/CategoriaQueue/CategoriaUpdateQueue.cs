using Ecommerce.Domain.Entities.Produtos;
using Ecommerce.Domain.Interfaces.EFRepository;
using Ecommerce.Domain.Repository;
using MassTransit;

namespace Ecommerce.Consumer.Background.Queues.CategoriaQueue
{
    public class CategoriaUpdateQueue : IConsumer<Categoria>
    {
        private readonly ICategoriaEfRepository _repository;
        public CategoriaUpdateQueue(ICategoriaEfRepository categoriaRepository)
        {
            _repository = categoriaRepository;
        }
        public Task Consume(ConsumeContext<Categoria> context)
        {
            var entidade = context.Message;

            var obj = _repository.ObterPorId(entidade.Id);

            obj.Descricao = entidade.Descricao;
            obj.Nome = entidade.Nome;
            obj.Ativo = entidade.Ativo;

                    

            _repository.Alterar(obj);

            return Task.CompletedTask;
        }
    }
}
