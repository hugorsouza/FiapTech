using Ecommerce.Domain.Entity;
using Ecommerce.Domain.Interfaces.EFRepository;
using Ecommerce.Infra.Entity.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Infra.Entity.Repositories
{
    public class Repository<T> : IEfRepository<T> where T : class
    {
        protected ApplicationDbContext _context;

        protected DbSet<T> _dbSet;

        public Repository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public void Alterar(T entidade)
        {

            _dbSet.Update(entidade);
            _context.SaveChanges();
        }

        public void Cadastrar(T entidade)
        {
            _dbSet.Add(entidade);
            _context.SaveChanges();
        }

        public void Deletar(int id)
        {
            _dbSet.Remove(ObterPorId(id));
            _context.SaveChanges();
        }

        public virtual T ObterPorId(int id)
        {
            throw new NotImplementedException();
        }

        public IList<T> ObterTodos()
        {
            return _dbSet.ToList();
        }
    }
}
