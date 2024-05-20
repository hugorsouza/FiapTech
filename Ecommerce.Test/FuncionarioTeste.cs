using Ecommerce.API.Controller;
using Ecommerce.Application.Model.Pessoas.Cadastro;
using Ecommerce.Application.Services.Interfaces.Autenticacao;
using Ecommerce.Application.Services.Interfaces.Pessoas;
using Ecommerce.Domain.Entities.Pessoas.Autenticacao;
using Ecommerce.Domain.Entities.Pessoas.Fisica;
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
    public class FuncionarioTeste
    {
        private readonly Mock<IFuncionarioService> _funcionarioServiceMock;
        private readonly Mock<IUsuarioManager> _usuarioManagerMock;
        private readonly FuncionarioController _funcionarioController;

        public FuncionarioTeste()
        {
            _funcionarioServiceMock = new Mock<IFuncionarioService>();
            _usuarioManagerMock = new Mock<IUsuarioManager>();
            _funcionarioController = new FuncionarioController(_funcionarioServiceMock.Object, _usuarioManagerMock.Object);
        }

        [Fact]
        public async Task ObterTodos_DeveRetornarOk_QuandoFuncionariosEncontrados()
        {
            // Arrange
            var funcionarios = new List<FuncionarioViewModel>
            {
                new FuncionarioViewModel(1, "João", "Silva", "12345678900", DateTime.Now, "Gerente", true, new UsuarioViewModel()),
                new FuncionarioViewModel(2, "Maria", "Santos", "98765432100", DateTime.Now, "Assistente", false, new UsuarioViewModel())
            };
            _funcionarioServiceMock.Setup(service => service.ObterTodos()).ReturnsAsync(funcionarios);

            // Act
            var result = await _funcionarioController.ObterTodos();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<FuncionarioViewModel>>(okResult.Value);
            Assert.Equal(funcionarios, model);
        }

        [Fact]
        public async Task ObterTodos_DeveRetornarNoContent_QuandoNaoExistemFuncionarios()
        {
            // Arrange
            _funcionarioServiceMock.Setup(service => service.ObterTodos()).ReturnsAsync(new List<FuncionarioViewModel>());

            // Act
            var result = await _funcionarioController.ObterTodos();

            // Assert
            Assert.IsType<NoContentResult>(result);
        }
        [Fact]
        public async Task Cadastrar_DeveRetornarOk_QuandoCadastroBemSucedido()
        {
            // Arrange
            var cadastro = new CadastroFuncionarioModel(); // Preencher o objeto de cadastro conforme necessário
            var funcionario = new FuncionarioViewModel(1, "Nome", "Sobrenome", "12345678901", DateTime.Now, "Cargo", true, new UsuarioViewModel()); // Ajustar os parâmetros conforme necessário
            _funcionarioServiceMock.Setup(service => service.Cadastrar(cadastro)).ReturnsAsync(funcionario);

            // Act
            var result = await _funcionarioController.Cadastrar(cadastro);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var model = Assert.IsAssignableFrom<FuncionarioViewModel>(okResult.Value);
            Assert.Equal(funcionario, model);
        }

        [Fact]
        public async Task Cadastrar_DeveRetornarBadRequest_QuandoCadastroFalha()
        {
            // Arrange
            var cadastro = new CadastroFuncionarioModel(); // Preencher o objeto de cadastro conforme necessário
            _funcionarioServiceMock.Setup(service => service.Cadastrar(cadastro)).ThrowsAsync(new Exception("Falha ao cadastrar o funcionário"));

            // Act
            var result = await _funcionarioController.Cadastrar(cadastro);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task Alterar_DeveRetornarOk_QuandoAlteracaoBemSucedida()
        {
            // Arrange
            var funcionarioServiceMock = new Mock<IFuncionarioService>();
            var id = 1;
            var cadastro = new AlterarFuncionarioModel
            {
                Cargo = "Novo Cargo",
                Administrador = true,
                Ativo = true
            };
            var funcionarioViewModel = new FuncionarioViewModel(id, "Nome", "Sobrenome", "12345678901", DateTime.Now, "Cargo Antigo", false, new UsuarioViewModel());

            funcionarioServiceMock.Setup(service => service.Alterar(cadastro, id)).ReturnsAsync(funcionarioViewModel);
            var controller = new FuncionarioController(funcionarioServiceMock.Object, null);

            // Act
            var result = await controller.Alterar(id, cadastro);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task Alterar_DeveRetornarBadRequest_QuandoAlteracaoFalha()
        {
            // Arrange
            var funcionarioServiceMock = new Mock<IFuncionarioService>();
            var id = 1;
            var cadastro = new AlterarFuncionarioModel
            {
                Cargo = "Novo Cargo",
                Administrador = true,
                Ativo = true
            };

            funcionarioServiceMock.Setup(service => service.Alterar(cadastro, id)).ThrowsAsync(new Exception("Falha ao alterar o funcionário"));
            var controller = new FuncionarioController(funcionarioServiceMock.Object, null);

            // Act
            var result = await controller.Alterar(id, cadastro);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }
        [Fact]
        public async Task ObterPorId_DeveRetornarOk_QuandoFuncionarioExiste()
        {
            // Arrange
            int funcionarioId = 1;
            var funcionarioViewModel = new FuncionarioViewModel(
                id: funcionarioId,
                nome: "João",
                sobrenome: "Silva",
                cpf: "12345678901",
                dataNascimento: new System.DateTime(1990, 1, 1),
                cargo: "Gerente",
                administrador: true,
                usuario: new UsuarioViewModel()
            );

            var funcionarioServiceMock = new Mock<IFuncionarioService>();
            funcionarioServiceMock.Setup(m => m.ObterPorId(funcionarioId)).ReturnsAsync(funcionarioViewModel);

            var controller = new FuncionarioController(funcionarioServiceMock.Object, null);

            // Act
            var result = await controller.ObterPorId(funcionarioId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var model = Assert.IsType<FuncionarioViewModel>(okResult.Value);
            Assert.Equal(funcionarioId, model.Id);
        }

        [Fact]
        public async Task ObterPorId_DeveRetornarNoContent_QuandoFuncionarioNaoExiste()
        {
            // Arrange
            int funcionarioId = 999; // Um ID que não existe
            FuncionarioViewModel funcionarioViewModel = null; // Simula um funcionário que não foi encontrado

            var funcionarioServiceMock = new Mock<IFuncionarioService>();
            funcionarioServiceMock.Setup(m => m.ObterPorId(funcionarioId)).ReturnsAsync(funcionarioViewModel);

            var controller = new FuncionarioController(funcionarioServiceMock.Object, null);

            // Act
            var result = await controller.ObterPorId(funcionarioId);

            // Assert
            var noContentResult = Assert.IsType<NoContentResult>(result);
        }
    }
}
