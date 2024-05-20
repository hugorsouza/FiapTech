using Ecommerce.Domain.Entities.Produtos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Domain.Interfaces.EFRepository
{
    public interface IFabricanteEfRepository : IEfRepository<Fabricante>
    {

        
    }
}
