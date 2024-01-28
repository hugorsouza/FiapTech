using Ecommerce.Application.DTO;
using Ecommerce.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controller
{
    [ApiController]
    [Route("Pedidos")]
    public class FazerPedidosController : ControllerBase
    {
        private readonly IFazerPedidoService _PedidoService;
        public FazerPedidosController(IFazerPedidoService fazerPedidoService)
        {
            _PedidoService = fazerPedidoService;

            Console.WriteLine();

        }


        [HttpGet("GetPedidos")]
        public IActionResult FazerPedido()
        {
            var retorno = _PedidoService.Pedidos();
            return Ok(retorno);
        }
    }
}
