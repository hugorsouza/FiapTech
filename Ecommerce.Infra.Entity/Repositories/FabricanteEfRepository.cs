using Ecommerce.Domain.Entities.Produtos;

using Ecommerce.Domain.Interfaces.EFRepository;
using Ecommerce.Infra.Entity.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Infra.Entity.Repositories
{
    public class FabricanteEfRepository : Repository<Fabricante> , IFabricanteEfRepository
    {
        public FabricanteEfRepository(ApplicationDbContext context) : base(context)
        {

        }

        //teste
        public override Fabricante ObterPorId(int id)
        {
           return _dbSet.FirstOrDefault(p => p.Id == id);
        }

    }
}
