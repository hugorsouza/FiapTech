using Ecommerce.Domain.Entities.Produtos;
using Ecommerce.Domain.Repository;
using MassTransit;

namespace Ecommerce.Consumer.Background.Queues.FabricanteQueue
{
    public class FabricanteUpdateQueue : IConsumer<Fabricante>
    {
        private readonly IFabricanteRepository _repository;
        public FabricanteUpdateQueue(IFabricanteRepository fabricanteRepository)
        {
            _repository = fabricanteRepository;
        }
        public Task Consume(ConsumeContext<Fabricante> context)
        {
            var entidade = context.Message;

            var obj = _repository.ObterPorId(entidade.Id);

            
            obj.Nome = entidade.Nome;
            obj.Ativo = entidade.Ativo;



            _repository.Alterar(obj);

            return Task.CompletedTask;
        }
    }
}
