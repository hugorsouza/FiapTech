using Ecommerce.Domain.Entities.Produtos;
using Ecommerce.Domain.Interfaces.EFRepository;
using Ecommerce.Infra.Entity.Repositories;
using Ecommerce.Infra.Entity.Repository;

namespace Ecommerce.Infra.Entity.Repositories
{
    public class EnderecoRepository : Repository<Endereco>, IEnderecoEfRepository
    {

        public EnderecoRepository(ApplicationDbContext context) : base(context)
        {

        }

        public void CadastrarEndereco(Endereco entidade)
        {
            _dbSet.Add(entidade);
            _context.SaveChanges();
        }

        
        public override Endereco ObterPorId(int id)
        {
            return _dbSet.FirstOrDefault(p => p.Id == id);
        }

    }
}

