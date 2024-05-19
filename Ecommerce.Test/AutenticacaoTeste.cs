using Ecommerce.API.Controller;
using Ecommerce.Application.Services.Interfaces.Autenticacao;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public async Task Login_ShouldReturnOk_WhenLoginIsSuccessful()
        {
            // Arrange
            var loginModel = new LoginModel { /* populate properties */ };
            _mockAutenticacaoService.Setup(service => service.Login(loginModel)).ReturnsAsync(new LoginResponse { /* populate properties */ });

            // Act
            var result = await _controller.Login(loginModel);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<LoginResponse>(okResult.Value);
        }

        [Fact]
        public async Task Refresh_ShouldReturnOk_WhenRefreshIsSuccessful()
        {
            // Arrange
            var refreshLoginModel = new RefreshLoginModel { /* populate properties */ };
            _mockAutenticacaoService.Setup(service => service.RefreshLogin(refreshLoginModel)).ReturnsAsync(new LoginResponse { /* populate properties */ });

            // Act
            var result = await _controller.Refresh(refreshLoginModel);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<LoginResponse>(okResult.Value);
        }

        [Fact]
        public async Task AlterarSenha_ShouldReturnOk_WhenAlterarSenhaIsSuccessful()
        {
            // Arrange
            var alterarSenhaModel = new AlterarSenhaModel { /* populate properties */ };

            // Act
            var result = await _controller.Alterar(alterarSenhaModel);

            // Assert
            var okResult = Assert.IsType<OkResult>(result);
        }

        [Fact]
        public void TesteRoleCliente_ShouldReturnOk_WhenUserIsCliente()
        {
            // Act
            var result = _controller.TesteRoleCliente();

            // Assert
            var okResult = Assert.IsType<OkResult>(result);
        }

        [Fact]
        public void TesteRoleOperador_ShouldReturnOk_WhenUserIsFuncionario()
        {
            // Act
            var result = _controller.TesteRoleOperador();

            // Assert
            var okResult = Assert.IsType<OkResult>(result);
        }

        [Fact]
        public void TestePoliticaAdmin_ShouldReturnOk_WhenUserIsAdmin()
        {
            // Act
            var result = _controller.TestePoliticaAdmin();

            // Assert
            var okResult = Assert.IsType<OkResult>(result);
        }
    }
}
