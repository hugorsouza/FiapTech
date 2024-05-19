using Ecommerce.Application.Model.Pessoas.Cadastro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.ModelResult.Cliente
{
    public class ClienteModelResult
    {
        public ClienteModelResult(int id, string cpf, string nome, string sobrenome, DateTime dataNascimento, bool recebeNewsletterEmail, UsuarioViewModel usuario)
        {
            Id = id;
            Cpf = cpf;
            Nome = nome;
            Sobrenome = sobrenome;
            DataNascimento = dataNascimento;
            RecebeNewsletterEmail = recebeNewsletterEmail;
            Usuario = usuario;
        }

        public int Id { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Cpf { get; set; }
        public DateTime DataNascimento { get; set; }
        public UsuarioViewModel Usuario { get; set; }
        public bool RecebeNewsletterEmail { get; set; }
        
    }
}
