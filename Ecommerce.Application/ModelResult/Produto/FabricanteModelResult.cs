using Ecommerce.Application.Model.Pessoas.Produto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.ModelResult.Produto
{
    public class FabricanteModelResult
    {
        public FabricanteModelResult(string nome, bool ativo, string cnpj)
        {
            Nome = nome;
            Ativo = ativo;
            CNPJ = cnpj;

        }
       

        public string Nome { get; set; }
        public bool Ativo { get; set; }
        public string CNPJ { get; set; }
        public EnderecoViewModel Endereco { get; set; }
        
    }
}
