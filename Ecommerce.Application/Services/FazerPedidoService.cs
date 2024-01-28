using Ecommerce.Application.DTO;
using Ecommerce.Application.Services.Interfaces;

namespace Ecommerce.Application.Services
{
    public class FazerPedidoService : IFazerPedidoService
    {
        private readonly IFazerPedidoService _fazerPedidoService;
        public FazerPedidoService()
        {
        }
        public List<FazerPedidoDTO> Pedidos()
        {
            var pedidos = new List<FazerPedidoDTO>();

            pedidos.Add(new FazerPedidoDTO
            {
                Id=1,
                Usuario="Lucas Rodrigues",
                DataPedido=DateTime.Now.AddDays(-1),
                Status="Separação",
                TipoPedido="Site"
            });

            pedidos.Add(new FazerPedidoDTO
            {
                Id = 2,
                Usuario = "Hugo Souza",
                DataPedido = DateTime.Now.AddDays(-3),
                Status = "Entregue",
                TipoPedido = "Loja"
            });

            return pedidos;
        }
    }
}
