using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ecommerce.Domain.Entity;

namespace Ecommerce.Domain.Entities.Produtos
{
    public class Fabricante : Entidade
    {
        public Fabricante() : base() { }
        public Fabricante(string nome, string cnpj ,bool ativo)
        {
            CNPJ = cnpj;
            Nome = nome;
            Ativo = ativo;
           
        }



        public string CNPJ { get; set; }
        public Endereco Endereco { get; set; }
        public int EnderecoId { get; set; }

        public ICollection<Produto> Produtos { get; set; }

        public string ObterCnpjSemFormatacao() => string.Join("", CNPJ.Where(char.IsDigit).ToArray());

    }
}
