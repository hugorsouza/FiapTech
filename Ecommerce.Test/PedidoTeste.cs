using Ecommerce.API.Controller;
using Ecommerce.Application.Model.Pessoas.Pedido;
using Ecommerce.Application.Services.Interfaces.Pedido;
using Ecommerce.Domain.Entities.Pedidos;
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
    public class PedidoTeste
    {
        private readonly Mock<IPedidoService> _pedidoServiceMock = new Mock<IPedidoService>();
        private readonly Mock<ILogger<PedidoController>> _loggerMock = new Mock<ILogger<PedidoController>>();

        [Fact]
        public void CadastrarPedido_DeveRetornarOk_QuandoPedidoCadastradoComSucesso()
        {
            // Arrange
            var pedidoController = new PedidoController(_loggerMock.Object, _pedidoServiceMock.Object);
            var produtoId = 1;
            var quantidade = 5;
            var pedidoModel = new PedidoModel
            {
                UsuarioDocumento = "12345678900",
                Usuario = "Usuario Teste",
                Descricao = "Pedido de teste",
                Quantidade = quantidade,
                ValorUnitario = 10.00m,
                ValorTotal = quantidade * 10.00m,
                DataPedido = DateTime.Now,
                TipoPedido = 1,
                TipoPedidoDescricao = "Tipo Teste",
                Status = 1,
                StatusDescricao = "Status Teste"
            };

            _pedidoServiceMock.Setup(m => m.CadastrarPedido(produtoId, quantidade)).Returns(pedidoModel);

            // Act
            var resultado = pedidoController.CadastrarPedido(produtoId, quantidade);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(resultado);
            var retorno = Assert.IsType<PedidoModel>(okResult.Value);
            Assert.Equal(pedidoModel, retorno);
        }

        [Fact]
        public void CadastrarPedido_DeveRetornarBadRequest_QuandoCadastroFalha()
        {
            // Arrange
            var pedidoController = new PedidoController(_loggerMock.Object, _pedidoServiceMock.Object);
            var produtoId = 1;
            var quantidade = 5;

            _pedidoServiceMock.Setup(m => m.CadastrarPedido(produtoId, quantidade)).Throws(new Exception("Falha ao cadastrar o pedido"));

            // Act
            var resultado = pedidoController.CadastrarPedido(produtoId, quantidade);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(resultado);
            Assert.Equal("Falha ao cadastrar o pedido", badRequestResult.Value);
        }
       
        [Fact]
        public async Task ObterPedidoPorId_DeveRetornarBadRequest_QuandoErroOcorre()
        {
            // Arrange
            var pedidoController = new PedidoController(_loggerMock.Object, _pedidoServiceMock.Object);
            var pedidoId = 1;

            _pedidoServiceMock.Setup(m => m.ObterPedidoPorId(pedidoId)).Throws(new Exception("Falha ao obter o pedido"));

            // Act
            var resultado =  pedidoController.ObterPedidoPorId(pedidoId);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(resultado);
            Assert.Equal("Falha ao obter o pedido", badRequestResult.Value);
        }
    }
}
