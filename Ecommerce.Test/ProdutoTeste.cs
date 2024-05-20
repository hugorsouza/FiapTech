using Ecommerce.API.Controller;
using Ecommerce.Application.Model.Produto;
using Ecommerce.Application.ModelResult.Produto;
using Ecommerce.Domain.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Ecommerce.Test
{
    public class ProdutoTeste
    {
        private readonly Mock<IProdutoService> _produtoServiceMock = new Mock<IProdutoService>();
        private readonly Mock<ILogger<ProdutoController>> _loggerMock = new Mock<ILogger<ProdutoController>>();

        [Fact]
        public async Task Cadastrar_DeveRetornarOk_QuandoCadastroSucesso()
        {
            // Arrange
            var produtoViewModel = new ProdutoViewModel(true, "Produto Teste", 10.0m, "Descrição Teste", 1, "url_imagem", 1, 1);
            var produtoController = new ProdutoController(_produtoServiceMock.Object, _loggerMock.Object);

            _produtoServiceMock.Setup(m => m.Cadastrar(It.IsAny<ProdutoViewModel>())).Returns(new ProdutoModelResult(true, "Produto Teste", 10.0m, "Descrição Teste", 1, "url_imagem", 1));

            // Act
            var resultado = produtoController.Post(produtoViewModel);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(resultado);
            Assert.IsType<ProdutoModelResult>(okResult.Value);
        }

        [Fact]
        public void ObterPorId_DeveRetornarOk_QuandoProdutoExiste()
        {
            // Arrange
            var produtoId = 1;
            var produtoController = new ProdutoController(_produtoServiceMock.Object, _loggerMock.Object);

            _produtoServiceMock.Setup(m => m.ObterPorId(produtoId)).Returns(new ProdutoModelResult(true, "Produto Teste", 10.0m, "Descrição Teste", 1, "url_imagem", 1));

            // Act
            var resultado = produtoController.ObterPorId(produtoId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(resultado);
            Assert.IsType<ProdutoModelResult>(okResult.Value);
        }

        [Fact]
        public void ObterPorId_DeveRetornarNoContent_QuandoProdutoNaoExiste()
        {
            // Arrange
            var produtoId = 1;
            var produtoController = new ProdutoController(_produtoServiceMock.Object, _loggerMock.Object);

            _produtoServiceMock.Setup(m => m.ObterPorId(produtoId)).Returns((ProdutoModelResult)null);

            // Act
            var resultado = produtoController.ObterPorId(produtoId);

            // Assert
            Assert.IsType<NoContentResult>(resultado);
        }

        [Fact]
        public void Otertodos_DeveRetornarOk_QuandoExistemProdutos()
        {
            // Arrange
            var produtoController = new ProdutoController(_produtoServiceMock.Object, _loggerMock.Object);

            _produtoServiceMock.Setup(m => m.ObterTodos()).Returns(new List<ProdutoModelResult> { new ProdutoModelResult(true, "Produto Teste", 10.0m, "Descrição Teste", 1, "url_imagem", 1) });

            // Act
            var resultado = produtoController.Otertodos();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(resultado);
            var produtos = Assert.IsAssignableFrom<IEnumerable<ProdutoModelResult>>(okResult.Value);
            Assert.Single(produtos);
        }

        [Fact]
        public async Task Alterar_DeveRetornarOk_QuandoAlteracaoSucesso()
        {
            // Arrange
            var produtoViewModel = new ProdutoViewModel(true, "Produto Teste", 10.0m, "Descrição Teste", 1, "url_imagem", 1, 1);
            var produtoController = new ProdutoController(_produtoServiceMock.Object, _loggerMock.Object);

            _produtoServiceMock.Setup(m => m.Alterar(It.IsAny<ProdutoViewModel>())).Returns(new ProdutoModelResult(true, "Produto Teste", 10.0m, "Descrição Teste", 1, "url_imagem", 1));

            // Act
            var resultado = produtoController.Alterar(produtoViewModel);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(resultado);
            Assert.IsType<ProdutoModelResult>(okResult.Value);
        }
    }
}
