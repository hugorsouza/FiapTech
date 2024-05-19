using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.ModelResult.Produto
{
    public class ProdutoModelResult
    {
        public ProdutoModelResult(bool ativo, string nome, decimal preco, string descricao, int fabricanteId, string urlImagem, int categoriaId)
        {
            Ativo = ativo;
            Nome = nome;
            Preco = preco;
            Descricao = descricao;
            FabricanteId = fabricanteId;
            UrlImagem = urlImagem;
            CategoriaId = categoriaId;


        }
        public bool Ativo { get; set; }
        public string Nome { get; set; }
        public decimal Preco { get; set; }
        public string Descricao { get; set; }
        public int FabricanteId { get; set; }
        public string UrlImagem { get; set; }
        public int CategoriaId { get; set; }
    
        
    }
}
