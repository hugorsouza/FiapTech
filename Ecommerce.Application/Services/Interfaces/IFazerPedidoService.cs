﻿using Ecommerce.Application.DTO;

namespace Ecommerce.Application.Services.Interfaces
{
    public interface IFazerPedidoService
    {
        public List<FazerPedidoDTO> Pedidos();
    }
}
