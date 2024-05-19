
using Ecommerce.Application.Model.Produto;
using Ecommerce.Application.ModelResult.Produto;
using Ecommerce.Domain.Entities.Produtos;

namespace Ecommerce.Domain.Services
{
    public interface ICategoriaService
    {
        CategoriaModelResult Cadastrar(CategoriaViewModel entidade);
        CategoriaModelResult ObterPorId(int id);
        IList<CategoriaModelResult> ObterTodos();
        CategoriaModelResult Alterar(CategoriaViewModel entidade);
        void AlterarQueue(Categoria entidade);
    }
}
