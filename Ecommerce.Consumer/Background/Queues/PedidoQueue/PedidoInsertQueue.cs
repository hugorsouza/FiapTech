using Ecommerce.Domain.Entities.Pedidos;
using Ecommerce.Domain.Entities.Produtos;
using Ecommerce.Domain.Interfaces.Repository;
using Ecommerce.Domain.Repository;
using MassTransit;

namespace Ecommerce.Consumer.Background.QueuesPedidoQueue
{
    public class PedidoInsertQueue : IConsumer<Pedido>
    {
        private readonly IPedidoRepository _repository;
        public PedidoInsertQueue(IPedidoRepository pedidoRepository)
        {
            _repository = pedidoRepository;
        }
        public Task Consume(ConsumeContext<Pedido> context)
        {

            _repository.Cadastrar(context.Message);

            return Task.CompletedTask;
        }
    }
}
