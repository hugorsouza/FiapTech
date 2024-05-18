using Ecommerce.Domain.Entities.Produtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Domain.EntitiesEF.Produtos
{
    public class Fornecedor : Entidade
    {
        public string CNPJ { get; set; }
        public Endereco Endereco { get; set; }


    }
}
