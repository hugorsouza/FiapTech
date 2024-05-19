using Ecommerce.Application.Model.Produto;
using Ecommerce.Application.ModelResult.Produto;
using Ecommerce.Domain.Entities.Produtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Domain.Services
{
    public interface IFabricanteService
    {
        FabricanteModelResult Cadastrar(FabricanteViewModel entidade);
        FabricanteModelResult ObterPorId(int id);
        IList<FabricanteModelResult> ObterTodos();
        FabricanteModelResult Alterar(FabricanteViewModel entidade);
        void Deletar(int id);

    }
}
