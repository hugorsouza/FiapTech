using Ecommerce.API.Controller;
using Ecommerce.Application.Model.Pessoas.Cadastro;
using Ecommerce.Application.Services.Interfaces.Pessoas;
using Ecommerce.Domain.Entities.Pessoas.Autenticacao;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Microsoft.AspNetCore.Http;
using Ecommerce.Domain.Entities.Shared;
using Ecommerce.Application.Services.Interfaces.Autenticacao;
using Ecommerce.Domain.Entities.Pessoas.Fisica;

namespace Ecommerce.Test
{
    public class ClienteTeste
    {
        [Fact]
        public async Task ObterTodos_DeveRetornarClientes_QuandoExistemClientesCadastrados()
        {
            // Arrange
            var clientesCadastrados = new List<ClienteViewModel>
        {
            new ClienteViewModel(1, "12345678900", "João", "Silva", new DateTime(1990, 5, 15), true, new UsuarioViewModel())
            // Adicione mais clientes simulados aqui, conforme necessário
        };

            var clienteServiceMock = new Mock<IClienteService>();
            clienteServiceMock.Setup(service => service.ObterTodos()).ReturnsAsync(clientesCadastrados);

            var controller = new ClienteController(clienteServiceMock.Object, null);

            var httpContext = new DefaultHttpContext();
            httpContext.User = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
            new Claim(ClaimTypes.Role, PerfilUsuarioExtensions.Funcionario)
            }, "mock"));

            controller.ControllerContext = new ControllerContext
            {
                HttpContext = httpContext
            };

            // Act
            var result = await controller.ObterTodos() as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
            Assert.Equal(clientesCadastrados, result.Value);
        }

        [Fact]
        public async Task ObterTodos_DeveRetornar204_QuandoNaoExistemClientesCadastrados()
        {
            // Arrange
            var clienteServiceMock = new Mock<IClienteService>();
            clienteServiceMock.Setup(service => service.ObterTodos()).ReturnsAsync(Enumerable.Empty<ClienteViewModel>());

            var controller = new ClienteController(clienteServiceMock.Object, null);

            var httpContext = new DefaultHttpContext();
            httpContext.User = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Role, PerfilUsuarioExtensions.Funcionario)
            }, "mock"));

            controller.ControllerContext = new ControllerContext
            {
                HttpContext = httpContext
            };

            // Act
            var result = await controller.ObterTodos();

            // Assert
            Assert.IsType<NoContentResult>(result);  // Verifica se o resultado é NoContentResult
        }

        [Fact]
        public async Task Cadastrar_DeveRetornarOkComClienteViewModel()
        {
            // Arrange
            var clienteServiceMock = new Mock<IClienteService>();
            var controller = new ClienteController(clienteServiceMock.Object, null);
            var cadastroClienteModel = new CadastroClienteModel
            {
                // Preencha as variáveis de teste aqui conforme necessário
                RecebeNewsletterEmail = true,
                // outras propriedades conforme necessário
            };
            var clienteViewModelMock = new ClienteViewModel
            (
                id: 1,
                cpf: "123456789",
                nome: "Teste",
                sobrenome: "Testando",
                dataNascimento: DateTime.Now,
                recebeNewsletterEmail: true,
                usuario: new UsuarioViewModel
                (
                    email: "teste@teste.com",
                    nomeExibicao: "Teste",
                    ativo: true
                )
            );
            clienteServiceMock.Setup(service => service.Cadastrar(It.IsAny<CadastroClienteModel>()))
                .ReturnsAsync(clienteViewModelMock);

            // Act
            var result = await controller.Cadastrar(cadastroClienteModel);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var clienteResult = Assert.IsType<ClienteViewModel>(okResult.Value);
            Assert.Equal(clienteViewModelMock, clienteResult);
        }

        [Fact]
        public async Task Alterar_DeveRetornarOk()
        {
            // Arrange
            var clienteServiceMock = new Mock<IClienteService>();
            var controller = new ClienteController(clienteServiceMock.Object, null);

            var cadastro = new AlterarClienteModel
            {
                // Preencha as variáveis da model para o teste
                RecebeNewsletterEmail = true,
                // Adicione outras propriedades conforme necessário
            };

            var clienteViewModel = new ClienteViewModel(
                id: 1,
                cpf: "12345678900",
                nome: "Fulano",
                sobrenome: "de Tal",
                dataNascimento: new DateTime(1990, 1, 1),
                recebeNewsletterEmail: true,
                usuario: new UsuarioViewModel()
            );

            clienteServiceMock.Setup(service => service.Alterar(It.IsAny<AlterarClienteModel>()))
                              .ReturnsAsync(clienteViewModel);

            // Act
            var resultado = await controller.Alterar(cadastro);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(resultado);
            var viewModelResult = Assert.IsType<ClienteViewModel>(okResult.Value);

            Assert.Equal(clienteViewModel.Id, viewModelResult.Id);
            Assert.Equal(clienteViewModel.Cpf, viewModelResult.Cpf);
            Assert.Equal(clienteViewModel.Nome, viewModelResult.Nome);
            Assert.Equal(clienteViewModel.Sobrenome, viewModelResult.Sobrenome);
            Assert.Equal(clienteViewModel.DataNascimento, viewModelResult.DataNascimento);
            Assert.Equal(clienteViewModel.RecebeNewsletterEmail, viewModelResult.RecebeNewsletterEmail);
        }
        [Fact]
        public async Task Alterar_DeveRetornarOkComClienteViewModel_QuandoAlteracaoBemSucedida()
        {
            // Arrange
            var clienteServiceMock = new Mock<IClienteService>();
            var controller = new ClienteController(clienteServiceMock.Object, null);
            var id = 1;
            var cadastro = new AlterarClienteAdminModel
            {
                Ativo = true,
            };
            var clienteViewModel = new ClienteViewModel(
                id: 1,
                cpf: "12345678900",
                nome: "Fulano",
                sobrenome: "de Tal",
                dataNascimento: new DateTime(1990, 1, 1),
                recebeNewsletterEmail: true,
                usuario: new UsuarioViewModel() // Preencha conforme necessário
            );

            clienteServiceMock.Setup(service => service.Alterar(id, cadastro)).ReturnsAsync(clienteViewModel);

            // Act
            var result = await controller.Alterar(id, cadastro);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var modelResult = Assert.IsType<ClienteViewModel>(okResult.Value);
            Assert.Equal(clienteViewModel, modelResult);
        }
        [Fact]
        public async Task ObterDadosPessoais_DeveRetornarOkComClienteViewModel_QuandoUsuarioTemClienteAssociado()
        {
            // Arrange
            var usuarioManagerMock = new Mock<IUsuarioManager>();
            var clienteServiceMock = new Mock<IClienteService>();
            var controller = new ClienteController( clienteServiceMock.Object, usuarioManagerMock.Object);

            var usuario = new Usuario
            {
                // Preencha com os dados do objeto usuario conforme necessário para o teste
                Cliente = new Cliente
                {
                    Id = 1,
                    Cpf = "12345678900",
                    Nome = "Fulano",
                    Sobrenome = "de Tal",
                    DataNascimento = new DateTime(1990, 1, 1),
                    // Preencha o restante conforme necessário
                }
            };

            var clienteViewModel = new ClienteViewModel(
                id: usuario.Cliente.Id,
                cpf: usuario.Cliente.Cpf,
                nome: usuario.Cliente.Nome,
                sobrenome: usuario.Cliente.Sobrenome,
                dataNascimento: usuario.Cliente.DataNascimento,
                recebeNewsletterEmail: true,
                usuario: new UsuarioViewModel()
            );

            usuarioManagerMock.Setup(manager => manager.ObterUsuarioAtual()).Returns(usuario);
            clienteServiceMock.Setup(service => service.BuildViewModel(usuario.Cliente)).Returns(clienteViewModel);

            // Act
            var result = await controller.ObterDadosPessoais();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var modelResult = Assert.IsType<ClienteViewModel>(okResult.Value);
            Assert.Equal(clienteViewModel, modelResult);
        }

        [Fact]
        public async Task ObterPorId_DeveRetornarOkComClienteViewModel_QuandoClienteExiste()
        {
            // Arrange
            var usuarioManagerMock = new Mock<IUsuarioManager>();
            var clienteServiceMock = new Mock<IClienteService>();
            var controller = new ClienteController(clienteServiceMock.Object, usuarioManagerMock.Object);

            var clienteId = 1;
            var clienteViewModel = new ClienteViewModel(
                id: clienteId,
                cpf: "12345678900",
                nome: "Fulano",
                sobrenome: "de Tal",
                dataNascimento: new DateTime(1990, 1, 1),
                recebeNewsletterEmail: true,
                usuario: new UsuarioViewModel()
            );

            clienteServiceMock.Setup(service => service.ObterPorId(clienteId)).ReturnsAsync(clienteViewModel);

            // Act
            var result = await controller.ObterPorId(clienteId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var modelResult = Assert.IsType<ClienteViewModel>(okResult.Value);
            Assert.Equal(clienteViewModel, modelResult);
        }

        [Fact]
        public async Task ObterPorId_DeveRetornarNotFound_QuandoClienteNaoExiste()
        {
            // Arrange
            var clienteServiceMock = new Mock<IClienteService>();
            var controller = new ClienteController(clienteServiceMock.Object, null);

            var clienteId = 1;

            clienteServiceMock.Setup(service => service.ObterPorId(clienteId)).ReturnsAsync(() => null);

            // Act
            var result = await controller.ObterPorId(clienteId);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundResult>(result);
        }
    }
}

