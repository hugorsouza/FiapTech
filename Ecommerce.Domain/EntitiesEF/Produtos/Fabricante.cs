using Ecommerce.Domain.Entities.Produtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Domain.EntitiesEF.Produtos
{
    public class Fabricante : Entidade
    {
        public Fabricante() : base() { }
        public Fabricante(string nome, string cnpj, bool ativo, Endereco endereco)
        {
            CNPJ = cnpj;
            Nome = nome;
            Ativo = ativo;
            Endereco = endereco;
        }


        public string CNPJ { get; set; }
        public Endereco Endereco { get; set; }

        public string ObterCnpjSemFormatacao() => string.Join("", CNPJ.Where(char.IsDigit).ToArray());

    }
}
