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
            _repository.Alterar(context.Message);

            return Task.CompletedTask;
        }
    }
}
