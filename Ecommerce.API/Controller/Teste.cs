using Ecommerce.Infra.ServiceBus.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MassTransit;
using Ecommerce.Domain.Entities.Pedidos;
using System.Text.Json;
using Ecommerce.Domain.Entities.Produtos;
using System.Text;

namespace Ecommerce.API.Controller
{
    [Route("api/[Controller]")]
    [ApiController]
    public class TesteController : ControllerBase
    {
        private readonly IBus _bus;
        public TesteController(IBus bus)
        {
            _bus = bus;
        }
        
        [HttpPost]        
        public async Task<IActionResult> ServicoDeOnibus(int id)
        {
            var categoria = new Categoria
            {
                Id = 1,
                Descricao = "Teste",
                Nome = "Teste",
                Ativo = true
            };


            var queue = "FilaTeste";

            var endpoint = await _bus.GetSendEndpoint(new Uri($"queue:{queue}"));
                 

            await endpoint.Send(categoria);
            
            //  await endpoint.Send(new Pedido{Id=1});


            
            

            return Ok();
        }
    }
}
