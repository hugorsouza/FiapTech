using Ecommerce.Domain.Entities.Produtos;
using Ecommerce.Domain.Interfaces.EFRepository;
using Ecommerce.Domain.Repository;
using MassTransit;

namespace Ecommerce.Consumer.Background.Queues.FabricanteQueue
{
    public class FabricanteInsertQueue : IConsumer<Fabricante>
    {
        private readonly IFabricanteEfRepository _repository;
        public FabricanteInsertQueue(IFabricanteEfRepository fabricanteRepository)
        {
            _repository = fabricanteRepository;
        }
        public Task Consume(ConsumeContext<Fabricante> context)
        {

            _repository.Cadastrar(context.Message);

            return Task.CompletedTask;
        }
    }
}
