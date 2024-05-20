using Ecommerce.API.Controller;
using Ecommerce.Application.Model.Produto;
using Ecommerce.Application.ModelResult.Produto;
using Ecommerce.Domain.Services;
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
    public class FabricanteTeste
    {
        private readonly Mock<IFabricanteService> _fabricanteServiceMock;
        private readonly Mock<ILogger<CategoriaController>> _loggerMock;
        private readonly FabricanteController _fabricanteController;
        private readonly CategoriaController _categoriaController;

        public FabricanteTeste()
        {
            _fabricanteServiceMock = new Mock<IFabricanteService>();
            _loggerMock = new Mock<ILogger<CategoriaController>>();
            _fabricanteController = new FabricanteController(_fabricanteServiceMock.Object, _loggerMock.Object);
        }

        [Fact]
        public void Cadastrar_DeveRetornarOk_QuandoFabricanteValido()
        {
            // Arrange
            var fabricante = new FabricanteViewModel("Nome", true, "12345678901234", 1);
            var fabricanteModelResult = new FabricanteModelResult("Nome", true, "12345678901234");
            _fabricanteServiceMock.Setup(repo => repo.Cadastrar(fabricante)).Returns(fabricanteModelResult);

            // Act
            var result = _fabricanteController.Cadastrar(fabricante) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            Assert.Equal(fabricanteModelResult, result.Value);
        }

        [Fact]
        public void ObterPorId_DeveRetornarOk_QuandoFabricanteEncontrado()
        {
            // Arrange
            var fabricanteId = 1;
            var fabricanteModelResult = new FabricanteModelResult("Nome", true, "12345678901234");
            _fabricanteServiceMock.Setup(repo => repo.ObterPorId(fabricanteId)).Returns(fabricanteModelResult);

            // Act
            var result = _fabricanteController.ObterPorId(fabricanteId) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            Assert.Equal(fabricanteModelResult, result.Value);
        }

        [Fact]
        public void ObterPorId_DeveRetornarNoContent_QuandoFabricanteNaoEncontrado()
        {
            // Arrange
            var fabricanteId = 999;
            _fabricanteServiceMock.Setup(repo => repo.ObterPorId(fabricanteId)).Returns(() => null);

            // Act
            var result = _fabricanteController.ObterPorId(fabricanteId) as StatusCodeResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(204, result.StatusCode);
        }

        [Fact]
        public void ObterTodos_DeveRetornarOk_QuandoListaNaoVazia()
        {
            // Arrange
            var fabricantes = new List<FabricanteModelResult>
            {
                new FabricanteModelResult("Nome1", true, "12345678901234"),
                new FabricanteModelResult("Nome2", true, "12345678901235")
            };
            _fabricanteServiceMock.Setup(repo => repo.ObterTodos()).Returns(fabricantes);

            // Act
            var result = _fabricanteController.Otertodos() as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            Assert.Equal(fabricantes, result.Value);
        }

        [Fact]
        public void Alterar_DeveRetornarOk_QuandoFabricanteAlterado()
        {
            // Arrange
            var fabricante = new FabricanteViewModel("Nome", true, "12345678901234", 1);
            var fabricanteModelResult = new FabricanteModelResult("Nome", true, "12345678901234");
            _fabricanteServiceMock.Setup(repo => repo.Alterar(fabricante)).Returns(fabricanteModelResult);

            // Act
            var result = _fabricanteController.Alterar(fabricante) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            Assert.Equal(fabricanteModelResult, result.Value);
        }
    }
}
