using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Ecommerce.Domain.EntitiesEF.Produtos
{
    public class Endereco
    {
        
        public int Id { get; set; }
        public required string Logradouro { get; set; }
        public required string Numero { get; set; }
        public required string CEP { get; set; }
        public required string Bairro { get; set; }
        public required string Cidade { get; set; }
        public required string Estado { get; set; }        
        

    }
}
