using Azure.Storage.Blobs;
using Ecommerce.Application.Model.Produto;
using Ecommerce.Application.ModelResult.Produto;
using Ecommerce.Application.Services.Interfaces.Autenticacao;
using Ecommerce.Domain.Entities.Estoque;
using Ecommerce.Domain.Entities.Produtos;
using Ecommerce.Domain.Exceptions;
using Ecommerce.Domain.Interfaces.EFRepository;
using Ecommerce.Domain.Interfaces.Repository;
using Ecommerce.Domain.Repository;
using Ecommerce.Domain.Services;
using Ecommerce.Infra.ServiceBus.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace Ecommerce.Application.Services
{
    public class ProdutoService : IProdutoService
    {

        private readonly IProdutoRepository _produtoRepository;
        private readonly IProdutoEfRepository _produtoEfRepository;
        private readonly IEstoqueRepository _estoqueRepository;
        private readonly IUsuarioManager _usuarioManager;
        private readonly IFuncionarioRepository _funcionarioRepository;
        private readonly IConfiguration _configuration;
        private readonly IServiceBus _serviceBus;

        public ProdutoService(
            IProdutoRepository produtoRepository,
            IProdutoEfRepository produtoEfRepository,
            IEstoqueRepository estoqueRepository,
            IUsuarioManager usuarioManager,
            IFuncionarioRepository funcionarioRepository,
            IConfiguration configuration,
            IServiceBus serviceBus)
        {
            _produtoRepository = produtoRepository;
            _estoqueRepository = estoqueRepository;
            _usuarioManager = usuarioManager;
            _funcionarioRepository = funcionarioRepository;
            _configuration = configuration;
            _serviceBus = serviceBus;
            _produtoEfRepository = produtoEfRepository;
        }

        public ProdutoModelResult Cadastrar(ProdutoViewModel entidade)
        {
            var produto = buidProduto(entidade);

            if (ObterTodos().Where(x => x.Nome != null)
                    .Any(x => x.Nome.Equals(produto.Nome)))
                throw RequisicaoInvalidaException.PorMotivo($"O Produto {produto.Nome} já está cadastrado!");


            var produtoViewModel = BuildViewModel(produto);


            _serviceBus.SendMessage(produto, "produtoinsertqueue");       


            return BuildModelResult(produto);

        }

        public ProdutoModelResult Alterar(ProdutoViewModel entidade)
        {
            var entity = buidProduto(entidade);

            var result = ObterPorId(entidade.Id);

            if (result is null)
                throw RequisicaoInvalidaException.PorMotivo($"O Produto {entity.Id} não está cadastrado na Base");

            var produto = buidProduto(entidade);
            
            _serviceBus.SendMessage(produto, "produtoupdatequeue");

            return BuildModelResult(produto);
        }

        public void Deletar(int id)
        {
            throw new NotImplementedException();
        }

        public ProdutoModelResult ObterPorId(int id)
        {
            var result = _produtoEfRepository.ObterPorId(id);

            return BuildModelResult(result);
        }

        public IList<ProdutoModelResult> ObterTodos()
        {
            var listResult = new List<ProdutoModelResult>();
            var result = _produtoEfRepository.ObterTodos();

            foreach (var item in result)
                listResult.Add(BuildModelResult(item));

            return listResult;
        }

        private ProdutoViewModel BuildViewModel(Produto produto)
        {
            if (produto is null)
                return null;

            return new ProdutoViewModel(produto.Ativo, produto.Nome, produto.Preco,
                produto.Descricao, produto.FabricanteId, produto.UrlImagem, produto.CategoriaId, produto.Id);
        }

        private ProdutoModelResult BuildModelResult(Produto produto)
        {
            if (produto is null)
                return null;

            return new ProdutoModelResult(produto.Ativo, produto.Nome, produto.Preco,
                produto.Descricao, produto.FabricanteId, produto.UrlImagem, produto.CategoriaId);
        }

        private Produto buidProduto(ProdutoViewModel model)
        {
            if (model is null)
                return null;

            return new Produto(model.Ativo, model.Nome, model.Preco,
                model.Descricao, model.FabricanteId, model.UrlImagem, model.CategoriaId);

        }

        public async Task<string> Upload(IFormFile arquivoimportado, int idProduto)
        {
            throw new NotImplementedException();

        }

        public async Task DeletarimagemProduto(int idProduto)
        {

            throw new NotImplementedException();

        }
    }
}
