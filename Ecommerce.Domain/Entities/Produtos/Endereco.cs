using Ecommerce.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Ecommerce.Domain.Entities.Produtos
{
    public class Endereco
    {
        public Endereco( string logradouro, string numero, string cep, string bairro, string cidade, string estado)
        {
            
            Logradouro = logradouro;
            Numero = numero;
            CEP = cep;
            Bairro = bairro;
            Cidade = cidade;
            Estado = estado;
        
        }
        public Endereco()
        {

        }

        public int Id { get; set; }
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string CEP { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }        
        public int EntidadeId { get; set; }

        public ICollection<Fabricante> Fabricantes { get; set; }
    }
}
