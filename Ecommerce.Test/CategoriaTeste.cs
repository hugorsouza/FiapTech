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
    public class CategoriaTeste
    {
        private readonly Mock<ICategoriaService> _mockCategoriaService;
        private readonly Mock<ILogger<CategoriaController>> _mockLogger;
        private readonly CategoriaController _controller;

        public CategoriaTeste()
        {
            _mockCategoriaService = new Mock<ICategoriaService>();
            _mockLogger = new Mock<ILogger<CategoriaController>>();
            _controller = new CategoriaController(_mockCategoriaService.Object, _mockLogger.Object);
        }

        [Fact]
        public void Cadastrar_ValidCategoria_ReturnsOkResult()
        {
            // Arrange
            var categoria = new CategoriaViewModel("NomeTeste", "DescricaoTeste", true, 1);
            var expectedResult = new CategoriaModelResult("NomeTeste", "DescricaoTeste", true);
            _mockCategoriaService.Setup(service => service.Cadastrar(categoria)).Returns(expectedResult);

            // Act
            var result = _controller.Cadastrar(categoria);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(expectedResult, okResult.Value);
            _mockCategoriaService.Verify(service => service.Cadastrar(categoria), Times.Once);
        }

        [Fact]
        public void Cadastrar_CategoriaNull_ReturnsBadRequest()
        {
            // Act
            var result = _controller.Cadastrar(null);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public void Cadastrar_CategoriaInvalida_ReturnsBadRequest()
        {
            // Arrange
            var categoria = new CategoriaViewModel(null, "DescricaoTeste", true, 1); // Nome é obrigatório

            // Act
            var result = _controller.Cadastrar(categoria);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public void Cadastrar_ReturnsStatusCode500()
        {
            // Arrange
            var categoria = new CategoriaViewModel("NomeTeste", "DescricaoTeste", true, 1);
            _mockCategoriaService.Setup(service => service.Cadastrar(categoria)).Throws(new Exception("Service error"));

            // Act
            var result = _controller.Cadastrar(categoria);

            // Assert
            var statusCodeResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, statusCodeResult.StatusCode);
        }

        [Fact]
        public void ObterPorId_RetornaOkResult_ComCategoria()
        {
            // Arrange
            var categoriaId = 1;
            var categoria = new CategoriaModelResult("Nome Categoria", "Descrição Categoria", true);
            _mockCategoriaService.Setup(service => service.ObterPorId(categoriaId)).Returns(categoria);

            // Act
            var result = _controller.ObterPorId(categoriaId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<CategoriaModelResult>(okResult.Value);
            Assert.Equal(categoria, returnValue);
        }

        [Fact]
        public void ObterPorId_RetornaNoContent_QuandoCategoriaNaoEncontrada()
        {
            // Arrange
            var categoriaId = 1;
            _mockCategoriaService.Setup(service => service.ObterPorId(categoriaId)).Returns((CategoriaModelResult)null);

            // Act
            var result = _controller.ObterPorId(categoriaId);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }
        [Fact]
        public void Alterar_AtualizarCategoria()
        {
            // Arrange
            var categoriaViewModel = new CategoriaViewModel("Nome Atualizado", "Descrição Atualizada", true, 1);
            var categoriaModelResult = new CategoriaModelResult("Nome Atualizado", "Descrição Atualizada", true);

            _mockCategoriaService.Setup(service => service.Alterar(It.IsAny<CategoriaViewModel>()))
                .Returns(categoriaModelResult);

            // Act
            var result = _controller.Alterar(categoriaViewModel) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            Assert.Equal(categoriaModelResult, result.Value);
        }

        [Fact]
        public void Alterar_ComParametros()
        {
            // Arrange
            var categoriaViewModel = new CategoriaViewModel("Nome", "Descrição", true, 1);
            var categoriaModelResult = new CategoriaModelResult("Nome", "Descrição", true);

            _mockCategoriaService.Setup(service => service.Alterar(It.IsAny<CategoriaViewModel>()))
                .Returns(categoriaModelResult);

            // Act
            _controller.Alterar(categoriaViewModel);

            // Assert
            _mockCategoriaService.Verify(service => service.Alterar(It.Is<CategoriaViewModel>(c =>
                c.Nome == categoriaViewModel.Nome &&
                c.Descricao == categoriaViewModel.Descricao &&
                c.Ativo == categoriaViewModel.Ativo &&
                c.Id == categoriaViewModel.Id)), Times.Once);
        }

        [Fact]
        public void Alterar_IsInvalid()
        {
            // Arrange
            _controller.ModelState.AddModelError("Nome", "Required");

            var categoriaViewModel = new CategoriaViewModel(null, "Descrição", true, 1);

            // Act
            var result = _controller.Alterar(categoriaViewModel);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }
    }
}

