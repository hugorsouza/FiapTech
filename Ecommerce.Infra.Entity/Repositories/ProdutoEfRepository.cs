using Ecommerce.Domain.Entities.Produtos;
using Ecommerce.Domain.Interfaces.EFRepository;
using Ecommerce.Infra.Entity.Repositories;
using Ecommerce.Infra.Entity.Repository;

namespace Ecommerce.Infra.Entity.Repositories
{
    public class ProdutoEfRepository : Repository<Produto>, IProdutoEfRepository
    {

        public ProdutoEfRepository(ApplicationDbContext context) : base(context)
        {

        }

        public override Produto ObterPorId(int id)
        {
            return _dbSet.FirstOrDefault(p => p.Id == id);
        }
    }
}

