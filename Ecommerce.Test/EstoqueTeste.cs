using Ecommerce.API.Controller;
using Ecommerce.Application.Model.Pessoas.Estoque;
using Ecommerce.Application.Services.Interfaces.Estoque;
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
    public class EstoqueTeste
    {
        private readonly Mock<ILogger<EstoqueController>> _loggerMock;
        private readonly Mock<IEstoqueService> _estoqueServiceMock;
        private readonly EstoqueController _controller;

        public EstoqueTeste()
        {
            _loggerMock = new Mock<ILogger<EstoqueController>>();
            _estoqueServiceMock = new Mock<IEstoqueService>();
            _controller = new EstoqueController(_loggerMock.Object, _estoqueServiceMock.Object);
        }

        [Fact]
        public void AlterarItemEstoque_DeveRetornarOk_QuandoAlteracaoBemSucedida()
        {
            // Arrange
            int itemId = 1;
            int quantidade = 10;
            var estoque = new EstoqueModel(
                usuarioDocumento: "12345678900",
                usuario: "Usuario Teste",
                produtoId: itemId,
                quantidadeAtual: 20,
                dataUltimaMovimentacao: DateTime.UtcNow);

            _estoqueServiceMock.Setup(s => s.AlterarItemEstoque(itemId, quantidade)).Returns(estoque);

            // Act
            var result = _controller.AlterarItemEstoque(itemId, quantidade);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal($"Alteração realizada com sucesso, quantidade atual é {estoque.QuantidadeAtual}", okResult.Value);
        }

        [Fact]
        public void AlterarItemEstoque_DeveRetornarOk_QuandoQuantidadeAtualizadaCorretamente()
        {
            // Arrange
            int itemId = 2;
            int quantidade = 5;
            var estoque = new EstoqueModel(
                usuarioDocumento: "98765432100",
                usuario: "Outro Usuario",
                produtoId: itemId,
                quantidadeAtual: 15,
                dataUltimaMovimentacao: DateTime.UtcNow);

            _estoqueServiceMock.Setup(s => s.AlterarItemEstoque(itemId, quantidade)).Returns(estoque);

            // Act
            var result = _controller.AlterarItemEstoque(itemId, quantidade);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal($"Alteração realizada com sucesso, quantidade atual é {estoque.QuantidadeAtual}", okResult.Value);
        }

        [Fact]
        public void AlterarItemEstoque_DeveRetornarOk_QuandoQuantidadeAtualizadaEDataMovimentacaoAtualizada()
        {
            // Arrange
            int itemId = 3;
            int quantidade = -3;
            var estoque = new EstoqueModel(
                usuarioDocumento: "11122233344",
                usuario: "Teste Usuario",
                produtoId: itemId,
                quantidadeAtual: 7,
                dataUltimaMovimentacao: DateTime.UtcNow);

            _estoqueServiceMock.Setup(s => s.AlterarItemEstoque(itemId, quantidade)).Returns(estoque);

            // Act
            var result = _controller.AlterarItemEstoque(itemId, quantidade);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal($"Alteração realizada com sucesso, quantidade atual é {estoque.QuantidadeAtual}", okResult.Value);
        }

        [Fact]
        public async Task ObterItemEstoquePorId_DeveRetornarOk_QuandoItemEncontrado()
        {
            // Arrange
            int itemId = 1;
            var estoque = new EstoqueModel(
                usuarioDocumento: "12345678900",
                usuario: "Usuario Teste",
                produtoId: itemId,
                quantidadeAtual: 20,
                dataUltimaMovimentacao: DateTime.UtcNow);

            _estoqueServiceMock.Setup(s => s.ObterItemEstoquePorId(itemId)).ReturnsAsync(estoque);

            // Act
            var result = await _controller.ObterItemEstoquePorId(itemId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(estoque, okResult.Value);
        }

        [Fact]
        public async Task ObterItemEstoquePorId_DeveRetornarNoContent_QuandoItemNaoEncontrado()
        {
            // Arrange
            int itemId = 2;
            _estoqueServiceMock.Setup(s => s.ObterItemEstoquePorId(itemId)).ReturnsAsync((EstoqueModel)null);

            // Act
            var result = await _controller.ObterItemEstoquePorId(itemId);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task ObterItemEstoquePorId_DeveRetornarOk_QuandoOutroItemEncontrado()
        {
            // Arrange
            int itemId = 3;
            var estoque = new EstoqueModel(
                usuarioDocumento: "98765432100",
                usuario: "Outro Usuario",
                produtoId: itemId,
                quantidadeAtual: 15,
                dataUltimaMovimentacao: DateTime.UtcNow);

            _estoqueServiceMock.Setup(s => s.ObterItemEstoquePorId(itemId)).ReturnsAsync(estoque);

            // Act
            var result = await _controller.ObterItemEstoquePorId(itemId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(estoque, okResult.Value);
        }
        [Fact]
        public async Task ObterListaCompletaEstoque_DeveRetornarOk_QuandoListaNaoVazia()
        {
            // Arrange
            var estoqueList = new List<EstoqueModel>
        {
            new EstoqueModel("12345678900", "Usuario Teste", 1, 20, DateTime.UtcNow),
            new EstoqueModel("98765432100", "Outro Usuario", 2, 30, DateTime.UtcNow)
        };

            _estoqueServiceMock.Setup(s => s.ObterListaCompletaEstoque()).ReturnsAsync(estoqueList);

            // Act
            var result = await _controller.ObterListaCompletaEstoque();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(estoqueList, okResult.Value);
        }

        [Fact]
        public async Task ObterListaCompletaEstoque_DeveRetornarNoContent_QuandoListaVazia()
        {
            // Arrange
            var emptyList = new List<EstoqueModel>();
            _estoqueServiceMock.Setup(s => s.ObterListaCompletaEstoque()).ReturnsAsync(emptyList);

            // Act
            var result = await _controller.ObterListaCompletaEstoque();

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task ObterListaCompletaEstoque_DeveRetornarNoContent_QuandoResultadoNulo()
        {
            // Arrange
            _estoqueServiceMock.Setup(s => s.ObterListaCompletaEstoque()).ReturnsAsync((IEnumerable<EstoqueModel>)null);

            // Act
            var result = await _controller.ObterListaCompletaEstoque();

            // Assert
            Assert.IsType<NoContentResult>(result);
        }
    }
}
