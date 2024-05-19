using MassTransit.Internals.GraphValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.ModelResult.Produto
{
    public class CategoriaModelResult
    {
        public CategoriaModelResult(string nome, string descricao, bool ativo)
        {
            Nome = nome;
            Descricao = descricao;
            Ativo = ativo;
        }

        public string Nome { get; set; }
        public string Descricao { get; set; }
        public bool Ativo { get; set; }
        

        
    }
}


