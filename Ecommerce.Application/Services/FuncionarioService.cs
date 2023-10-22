﻿using Ecommerce.Application.Model.Pessoas.Cadastro;
using Ecommerce.Application.Services.Interfaces.Autenticacao;
using Ecommerce.Application.Services.Interfaces.Pessoas;
using Ecommerce.Domain.Entities.Pessoas.Autenticacao;
using Ecommerce.Domain.Entities.Pessoas.Fisica;
using Ecommerce.Domain.Interfaces;
using Ecommerce.Domain.Interfaces.Repository;
using FluentValidation;

namespace Ecommerce.Application.Services;

public class FuncionarioService : IFuncionarioService
{
    private readonly IFuncionarioRepository _funcionarioRepository;
    private readonly IUsuarioManager _usuarioManager;
    private readonly IValidator<CadastroFuncionarioModel> _validatorCadastro;
    private readonly IValidator<AlterarFuncionarioModel> _validatorAlteracao;
    private readonly ITransactionService _transactionService;
    
    public FuncionarioService(IFuncionarioRepository funcionarioRepository,
        IValidator<CadastroFuncionarioModel> validatorCadastro,
        IUsuarioManager usuarioManager,
        ITransactionService transactionService,
        IValidator<AlterarFuncionarioModel> validatorAlteracao)
    {
        _funcionarioRepository = funcionarioRepository;
        _validatorCadastro = validatorCadastro;
        _usuarioManager = usuarioManager;
        _transactionService = transactionService;
        _validatorAlteracao = validatorAlteracao;
    }

    public async Task<FuncionarioViewModel> Cadastrar(CadastroFuncionarioModel model)
    {
        await _validatorCadastro.ValidateAsync(model);
        model.Cpf = model.ObterCpfSemFormatacao();
        
        _transactionService.BeginTransaction();
        
        var usuario = _usuarioManager.CadastrarUsuario(_usuarioManager.BuildUsuarioParaFuncionario(model));
        var funcionario = BuildFuncionario(model, usuario);
        _funcionarioRepository.Cadastrar(funcionario);
        
        _transactionService.Commit();
        
        var funcionarioViewModel = BuildViewModel(funcionario);
        return funcionarioViewModel;
    }

    public async Task<FuncionarioViewModel> ObterPorId(int id)
    {
        var funcionario = _funcionarioRepository.ObterPorId(id);
        return BuildViewModel(funcionario);
    }

    public async Task<IEnumerable<FuncionarioViewModel>> ObterTodos()
    {
        var funcionarios = _funcionarioRepository.ObterTodos();
        return funcionarios.Select(x => BuildViewModel(x));
    }

    public async Task<FuncionarioViewModel> Alterar(AlterarFuncionarioModel model)
    {
        await _validatorAlteracao.ValidateAsync(model);
        var agora = DateTime.Now;
        var usuario = _usuarioManager.ObterUsuarioAtual();
        var funcionario = usuario.Funcionario;
        funcionario.Nome = model.Nome;
        funcionario.Sobrenome = model.Sobrenome;
        funcionario.DataNascimento = model.DataNascimento;
        funcionario.Cargo = model.Cargo;
        funcionario.DataAlteracao = agora;

        usuario.DataAlteracao = agora;
        usuario.NomeExibicao = funcionario.NomeExibicao();
        
        _transactionService.BeginTransaction();
        _funcionarioRepository.Alterar(funcionario);
        _usuarioManager.Alterar(usuario);
        _transactionService.Commit();
        
        var funcionarioViewModel = BuildViewModel(funcionario);
        return funcionarioViewModel;
    }
    
    private Funcionario BuildFuncionario(CadastroFuncionarioModel model, Usuario usuario)
    {
        var funcionario = new Funcionario(
            usuarioId: usuario.Id,
            nome: model.Nome,
            sobrenome: model.Sobrenome,
            cpf: model.Cpf,
            dataNascimento: model.DataNascimento,
            usuario: usuario,
            cargo: model.Cargo
        );
        return funcionario;
    }
    
    public FuncionarioViewModel BuildViewModel(Funcionario funcionario)
    {
        var funcionarioViewModel = new FuncionarioViewModel(cpf: funcionario.Cpf, nome: funcionario.Nome, sobrenome: funcionario.Sobrenome,
            dataNascimento: funcionario.DataNascimento, administrador: funcionario.Administrador, cargo: funcionario.Cargo,
            usuario: funcionario.Usuario is null
                ? null
                : new UsuarioViewModel(email: funcionario.Usuario.Email, nomeExibicao: funcionario.Usuario.NomeExibicao));
        return funcionarioViewModel;
    }
}