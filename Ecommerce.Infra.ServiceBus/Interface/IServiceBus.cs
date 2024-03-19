using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Infra.ServiceBus.Interface
{
    public interface IServiceBus
    {
        Task SendMessage(Object Message, string queue);
    }
}
