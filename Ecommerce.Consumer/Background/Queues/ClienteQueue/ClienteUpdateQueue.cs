using Ecommerce.Domain.Entities.Pessoas.Fisica;
using Ecommerce.Domain.Interfaces.Repository;
using MassTransit;

namespace Ecommerce.Consumer.Background.Queues.ClienteQueue
{
    public class ClienteUpdateQueue : IConsumer<Cliente>
    {
        private readonly IClienteRepository _repository;

        public ClienteUpdateQueue(IClienteRepository clienteRepository)
        {
            _repository = clienteRepository;
        }
        public Task Consume(ConsumeContext<Cliente> context)
        {
            _repository.Alterar(context.Message);

            return Task.CompletedTask;
        }
    }
}
