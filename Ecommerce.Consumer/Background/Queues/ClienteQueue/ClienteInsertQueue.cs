using Ecommerce.Domain.Entities.Pessoas.Fisica;
using Ecommerce.Domain.Interfaces.Repository;
using MassTransit;
using System.Collections.Concurrent;

namespace Ecommerce.Consumer.Background.Queues.ClienteQueue
{
    public class ClienteInsertQueue : IConsumer<Cliente>
    {
        private readonly IClienteRepository _repository;

        public ClienteInsertQueue( IClienteRepository clienteRepository)
        {
            _repository = clienteRepository;
        }
        public Task Consume(ConsumeContext<Cliente> context)
        {
            _repository.Cadastrar(context.Message);

            return Task.CompletedTask;
        }
    }
}
