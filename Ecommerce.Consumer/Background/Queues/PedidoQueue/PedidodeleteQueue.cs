using Ecommerce.Domain.Entities.Pedidos;
using Ecommerce.Domain.Entities.Produtos;
using Ecommerce.Domain.Interfaces.Repository;
using Ecommerce.Domain.Repository;
using MassTransit;

namespace Ecommerce.Consumer.Background.Queues.PedidoQueue
{
    public class PedidodeleteQueue : IConsumer<Pedido>
    {
        private readonly IPedidoRepository _repository;
        public PedidodeleteQueue(IPedidoRepository pedidoRepository)
        {
            _repository = pedidoRepository;
        }
        public Task Consume(ConsumeContext<Pedido> context)
        {
            _repository.Deletar(context.Message.Id);

            return Task.CompletedTask;
        }
    }
}
