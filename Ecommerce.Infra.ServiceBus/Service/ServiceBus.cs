using Ecommerce.Infra.ServiceBus.Interface;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Infra.ServiceBus.Service
{
    public class ServiceBus : IServiceBus
    {
        private readonly IBus _bus;
        public ServiceBus(IBus bus)
        {
            _bus = bus;
        }
    
        public async Task SendMessage(object Message, string queue)
        {
           
            var endpoint = await _bus.GetSendEndpoint(new Uri($"queue:{queue}"));

            await endpoint.Send(Message);
                                   
        }
    }
}
