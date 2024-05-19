using Ecommerce.Domain.Entities.Produtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Domain.Interfaces.EFRepository
{
    public interface IEnderecoEfRepository : IEfRepository<Endereco>
    {
        void CadastrarEndereco(Endereco entidade);

    }
}
