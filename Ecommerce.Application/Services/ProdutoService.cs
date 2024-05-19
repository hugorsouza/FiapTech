using Azure.Storage.Blobs;
using Ecommerce.Application.Model.Produto;
using Ecommerce.Application.ModelResult.Produto;
using Ecommerce.Application.Services.Interfaces.Autenticacao;
using Ecommerce.Domain.Entities.Estoque;
using Ecommerce.Domain.Entities.Produtos;
using Ecommerce.Domain.Exceptions;
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
        private readonly IEstoqueRepository _estoqueRepository;
        private readonly IUsuarioManager _usuarioManager;
        private readonly IFuncionarioRepository _funcionarioRepository;
        private readonly IConfiguration _configuration;
        private readonly IServiceBus _serviceBus;

        public ProdutoService(
            IProdutoRepository produtoRepository,
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
        }

        public ProdutoModelResult Cadastrar(ProdutoViewModel entidade)
        {
            var produto = buidProduto(entidade);

            if (ObterTodos().Where(x => x.Nome != null)
                    .Any(x => x.Nome.Equals(produto.Nome)))
                throw RequisicaoInvalidaException.PorMotivo($"O Produto {produto.Nome} já está cadastrado!");


            var produtoViewModel = BuildViewModel(produto);

            //Add item no estoque
            //var consultaUser = _usuarioManager.ObterUsuarioAtual();
            // if (consultaUser == null)
            //    throw RequisicaoInvalidaException.PorMotivo($"Funcionario {consultaUser.Id} não localizado");

            // var consultaFuncionario = _funcionarioRepository.ObterPorId(consultaUser.Id);

            //var estoque = new Estoque
            //{
            //    UsuarioDocumento = consultaFuncionario.Cpf,
            //    Usuario = consultaUser.NomeExibicao,
            //    QuantidadeAtual = 0,
            //    DataUltimaMovimentacao = DateTime.UtcNow
            //};


            var estoque = new Estoque
            {
                UsuarioDocumento = "1342342354",
                Usuario = "Pedro Alvares Cabral",
                QuantidadeAtual = 0,
                DataUltimaMovimentacao = DateTime.UtcNow
            };

            var produtoEstoque = new { ProdutoModelResult = produto, Estoque = estoque };


            //var produtoId = await _produtoRepository.CadastrarAsync(produto, estoque);
            _serviceBus.SendMessage(produto, "produtoinsertqueue");



            return BuildModelResult(produto);

        }

        public ProdutoModelResult Alterar(ProdutoViewModel entidade)
        {
            var entity = buidProduto(entidade);

            var produto = ObterPorId(entity.Id);

            if (produto is null)
                throw RequisicaoInvalidaException.PorMotivo($"O Produto {entity.Id} não está cadastrado na Base");

            //_produtoRepository.Alterar(entidade);
            _serviceBus.SendMessage(entidade, "produtoupdatequeue");

            return BuildModelResult(produto);
        }

        public void Deletar(int id)
        {
            throw new NotImplementedException();
        }

        public Produto ObterPorId(int id)
        {
            return _produtoRepository.ObterPorId(id);
        }

        public IList<ProdutoModelResult> ObterTodos()
        {
            var listResult = new List<ProdutoModelResult>();
            var result = _produtoRepository.ObterTodos();

            foreach (var item in result)
                listResult.Add(BuildModelResult(item));

            return listResult;
        }

        private ProdutoViewModel BuildViewModel(Produto produto)
        {
            if (produto is null)
                return null;

            return new ProdutoViewModel(produto.Ativo, produto.Nome, produto.Preco,
                produto.Descricao, produto.FabricanteId, produto.UrlImagem, produto.CategoriaId);
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
