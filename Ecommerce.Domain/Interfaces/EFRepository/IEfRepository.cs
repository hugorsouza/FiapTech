using Ecommerce.Domain.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Domain.Interfaces.EFRepository
{
    public interface IEfRepository<T> : IRepository where T : class
    {
        public void Cadastrar(T entidade);
        public T ObterPorId(int id);
        public IList<T> ObterTodos();
        public void Alterar(T entidade);
        public void Deletar(int id);
    }
}
