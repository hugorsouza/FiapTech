﻿using Ecommerce.Domain.Entities;

namespace Ecommerce.Domain.Services
{
    public interface IFazerPedidoDomainService
    {
        public FazerPedidoEntity FazerPedido(FazerPedidoEntity fazerPedidoEntity);
    }
}
