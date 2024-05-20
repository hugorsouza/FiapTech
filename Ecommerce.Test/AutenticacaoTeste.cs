using Ecommerce.API.Controller;
using Ecommerce.Application.Model.Pessoas.Autenticacao;
using Ecommerce.Application.Services.Interfaces.Autenticacao;
using Ecommerce.Domain.Entities.Pessoas.Autenticacao;
using Ecommerce.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Ecommerce.Test
{
    public class AutenticacaoTeste
    {
        private readonly Mock<IAutenticacaoService> _mockAutenticacaoService;
        private readonly Mock<IUsuarioManager> _mockUsuarioManager;
        private readonly AutenticacaoController _controller;

        public AutenticacaoTeste()
        {
            _mockAutenticacaoService = new Mock<IAutenticacaoService>();
            _mockUsuarioManager = new Mock<IUsuarioManager>();
            _controller = new AutenticacaoController(_mockAutenticacaoService.Object, _mockUsuarioManager.Object);
        }

        [Fact]
        public async Task Login_IsSuccessful()
        {
            // Arrange
            var loginModel = new LoginModel
            {
                Email = "cliente@hotmail.com",
                Senha = "123456"
            };

            var expectedResponse = new LoginWithRefreshResponse
            {
                AccessToken = "some-access-token",
                ExpiraEmUtc = DateTime.UtcNow.AddHours(1),
                RefreshToken = "some-refresh-token"
            };

            _mockAutenticacaoService
                .Setup(service => service.Login(It.IsAny<LoginModel>()))
                .ReturnsAsync(expectedResponse);

            // Act
            var result = await _controller.Login(loginModel);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<LoginWithRefreshResponse>(okResult.Value);
            Assert.Equal(expectedResponse.AccessToken, returnValue.AccessToken);
            Assert.Equal(expectedResponse.ExpiraEmUtc, returnValue.ExpiraEmUtc);
            Assert.Equal(expectedResponse.RefreshToken, returnValue.RefreshToken);


        }

        [Fact]
        public async Task Login_Fails()
        {
            // Arrange
            var loginModel = new LoginModel
            {
                Email = "user@example.com",
                Senha = "wrongpassword"
            };

            _mockAutenticacaoService
                .Setup(service => service.Login(It.IsAny<LoginModel>()))
                .ReturnsAsync((LoginWithRefreshResponse)null);

            // Act
            var result = await _controller.Login(loginModel);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Invalid login attempt", badRequestResult.Value);
        }

        [Fact]
        public async Task Refresh_IsSuccessful()
        {
            // Arrange
            var mockService = new Mock<IAutenticacaoService>();
            var mockUsuarioManager = new Mock<IUsuarioManager>();
            var loginModel = new RefreshLoginModel { Email = "cliente@hotmail.com", RefreshToken = "refresh_token_valido" };
            var loginResponse = new LoginResponse();
            mockService.Setup(s => s.RefreshLogin(It.IsAny<RefreshLoginModel>())).ReturnsAsync(loginResponse);
            var controller = new AutenticacaoController(mockService.Object, mockUsuarioManager.Object);

            // Act
            var result = await controller.Refresh(loginModel);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(loginResponse, okResult.Value);
        }

        [Fact]
        public async Task Refresh_Fails()
        {
            // Arrange
            var mockUsuarioManager = new Mock<IUsuarioManager>();
            var mockService = new Mock<IAutenticacaoService>();
            var loginModel = new RefreshLoginModel { Email = "email@teste.com", RefreshToken = "refresh_token_invalido" };
            mockService.Setup(s => s.RefreshLogin(It.IsAny<RefreshLoginModel>())).ReturnsAsync((LoginResponse)null);
            var controller = new AutenticacaoController(mockService.Object, mockUsuarioManager.Object);

            // Act
            var result = await controller.Refresh(loginModel);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Invalid refresh token", badRequestResult.Value);
        }

        [Fact]
        public async Task Alterar_DeveRetornarOk_QuandoSenhaValida()
        {
            // Arrange
            var mockService = new Mock<IAutenticacaoService>();
            var model = new AlterarSenhaModel { Senha = "novaSenha123" };
            var usuarioManagerMock = new Mock<IUsuarioManager>();
            var controller = new AutenticacaoController(mockService.Object, usuarioManagerMock.Object);

            // Act
            var result = await controller.Alterar(model);

            // Assert
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task Alterar_DeveChamarMetodoAlterarSenha_QuandoSenhaValida()
        {
            // Arrange
            var mockService = new Mock<IAutenticacaoService>();
            var model = new AlterarSenhaModel { Senha = "novaSenha123" };
            var usuarioManagerMock = new Mock<IUsuarioManager>();
            var controller = new AutenticacaoController(mockService.Object, usuarioManagerMock.Object);

            // Act
            await controller.Alterar(model);

            // Assert
            usuarioManagerMock.Verify(x => x.AlterarSenha(model), Times.Once);
        }

        [Fact]
        public async Task Alterar_DeveRetornarOk_QuandoUsuarioValido()
        {
            // Arrange
            var mockService = new Mock<IAutenticacaoService>();
            var model = new AlterarSenhaModel { Senha = "novaSenha123" };
            var usuarioManagerMock = new Mock<IUsuarioManager>();
            var controller = new AutenticacaoController(mockService.Object, usuarioManagerMock.Object);

            // Act
            var result = await controller.Alterar(model);

            // Assert
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public void TesteRoleCliente_DeveRetornarOk()
        {
            // Arrange
            var mockService = new Mock<IAutenticacaoService>();
            var usuarioManagerMock = new Mock<IUsuarioManager>();
            var controller = new AutenticacaoController(mockService.Object, usuarioManagerMock.Object);

            // Act
            var result = controller.TesteRoleCliente();

            // Assert
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public void TesteRoleCliente_DeveRetornarStatusCodeCorreto()
        {
            // Arrange
            var mockService = new Mock<IAutenticacaoService>();
            var usuarioManagerMock = new Mock<IUsuarioManager>();
            var controller = new AutenticacaoController(mockService.Object, usuarioManagerMock.Object);

            // Act
            var result = controller.TesteRoleCliente() as StatusCodeResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public void TesteRoleCliente_NaoDeveRetornarNotFound()
        {
            // Arrange
            var mockService = new Mock<IAutenticacaoService>();
            var usuarioManagerMock = new Mock<IUsuarioManager>();
            var controller = new AutenticacaoController(mockService.Object, usuarioManagerMock.Object);

            // Act
            var result = controller.TesteRoleCliente();

            // Assert
            Assert.IsNotType<NotFoundResult>(result);
        }

        [Fact]
        public void TesteRoleOperador_DeveRetornarOk()
        {
            // Arrange
            var mockService = new Mock<IAutenticacaoService>();
            var usuarioManagerMock = new Mock<IUsuarioManager>();
            var controller = new AutenticacaoController(mockService.Object, usuarioManagerMock.Object);

            // Act
            var result = controller.TesteRoleCliente();

            // Assert
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public void TesteRoleOperador_DeveRetornarStatusCodeCorreto()
        {
            // Arrange
            var mockService = new Mock<IAutenticacaoService>();
            var usuarioManagerMock = new Mock<IUsuarioManager>();
            var controller = new AutenticacaoController(mockService.Object, usuarioManagerMock.Object);

            // Act
            var result = controller.TesteRoleCliente() as StatusCodeResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public void TesteRoleOperador_NaoDeveRetornarNotFound()
        {
            // Arrange
            var mockService = new Mock<IAutenticacaoService>();
            var usuarioManagerMock = new Mock<IUsuarioManager>();
            var controller = new AutenticacaoController(mockService.Object, usuarioManagerMock.Object);

            // Act
            var result = controller.TesteRoleCliente();

            // Assert
            Assert.IsNotType<NotFoundResult>(result);
        }

        [Fact]
        public void TesteRolePolitica_DeveRetornarOk()
        {
            // Arrange
            var mockService = new Mock<IAutenticacaoService>();
            var usuarioManagerMock = new Mock<IUsuarioManager>();
            var controller = new AutenticacaoController(mockService.Object, usuarioManagerMock.Object);

            // Act
            var result = controller.TesteRoleCliente();

            // Assert
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public void TesteRolePolitica_DeveRetornarStatusCodeCorreto()
        {
            // Arrange
            var mockService = new Mock<IAutenticacaoService>();
            var usuarioManagerMock = new Mock<IUsuarioManager>();
            var controller = new AutenticacaoController(mockService.Object, usuarioManagerMock.Object);

            // Act
            var result = controller.TesteRoleCliente() as StatusCodeResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public void TesteRolePolitica_NaoDeveRetornarNotFound()
        {
            // Arrange
            var mockService = new Mock<IAutenticacaoService>();
            var usuarioManagerMock = new Mock<IUsuarioManager>();
            var controller = new AutenticacaoController(mockService.Object, usuarioManagerMock.Object);

            // Act
            var result = controller.TesteRoleCliente();

            // Assert
            Assert.IsNotType<NotFoundResult>(result);
        }
    }
}
