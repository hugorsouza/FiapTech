using Ecommerce.Infra.ServiceBus.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MassTransit;

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
            var queue = "FilaTeste";

            var endpoint = await _bus.GetSendEndpoint(new Uri($"queue:{queue}"));

            await endpoint.Send(new {Id=1});

            return Ok();
        }
    }
}
