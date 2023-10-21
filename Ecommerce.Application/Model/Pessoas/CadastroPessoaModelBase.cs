﻿namespace Ecommerce.Application.Model.Pessoas;

public abstract record CadastroPessoaModelBase : CadastroUsuarioModelBase
{
    public string Nome { get; init; }
    public string Sobrenome { get; init; }
    public string Cpf { get; set; }
    public DateTime DataNascimento { get; init; }

    public override string ObterNomeExibicao()
    {
        return $"{Nome} {Sobrenome}".Trim();
    }
}