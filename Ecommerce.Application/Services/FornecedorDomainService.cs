﻿using Ecommerce.Domain.Entity;
using Ecommerce.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Services
{
    public class FornecedorDomainService : DomainService<Fornecedor>, IFornecedorDomainService
    {
        public override void Alterar(Fornecedor entidade)
        {
            throw new NotImplementedException();
        }

        public override void Cadastrar(Fornecedor entidade)
        {
            throw new NotImplementedException();
        }

        public override void Deletar(int id)
        {
            throw new NotImplementedException();
        }

        public override Fornecedor ObterPorId(int id)
        {
            throw new NotImplementedException();
        }

        public override IList<Fornecedor> ObterTodos()
        {
            throw new NotImplementedException();
        }
    }
}
