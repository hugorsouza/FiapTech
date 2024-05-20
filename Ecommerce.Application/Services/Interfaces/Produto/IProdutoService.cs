using Ecommerce.Application.Model.Produto;
using Ecommerce.Application.ModelResult.Produto;
using Ecommerce.Domain.Entities.Produtos;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Domain.Services
{
    public interface IProdutoService 
    {
        ProdutoModelResult Cadastrar(ProdutoViewModel entidade);
        ProdutoModelResult ObterPorId(int id);
        IList<ProdutoModelResult> ObterTodos();
        ProdutoModelResult Alterar(ProdutoViewModel entidade);
        void Deletar(int id);
        Task<string> Upload(IFormFile arquivo, int id);
        Task DeletarimagemProduto(int id);

    }
}
