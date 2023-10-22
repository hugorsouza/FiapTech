﻿using Ecommerce.Application.Model.Pessoas;
using Ecommerce.Application.Model.Pessoas.Cadastro;
using Ecommerce.Application.Services.Interfaces.Autenticacao;
using Ecommerce.Application.Services.Interfaces.Pessoas;
using Ecommerce.Domain.Entities.Pessoas.Autenticacao;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controller;

[Authorize(Roles = PerfilUsuarioExtensions.Cliente)]
[ApiController]
[Route("[controller]")]
public class ClienteController : ControllerBase
{
    private readonly IClienteService _clienteService;
    private readonly IUsuarioManager _usuarioManager;
    public ClienteController(IClienteService clienteService, IUsuarioManager usuarioManager)
    {
        _clienteService = clienteService;
        _usuarioManager = usuarioManager;
    }
    
    [AllowAnonymous]
    [HttpPost("Cadastrar")]
    [ProducesResponseType(typeof(ClienteViewModel), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Cadastrar([FromBody] CadastroClienteModel cadastro)
    {
        var resultado = await _clienteService.Cadastrar(cadastro);
        return Ok(resultado);
    } 
    
    [HttpPost("Alterar")]
    [ProducesResponseType(typeof(ClienteViewModel), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Alterar([FromBody] AlterarClienteModel cadastro)
    {
        var resultado = await _clienteService.Alterar(cadastro);
        return Ok(resultado);
    } 
    
    [HttpGet("ObterDadosPessoais")]
    [ProducesResponseType(typeof(ClienteViewModel), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ObterDadosPessoais()
    {
        var usuario = _usuarioManager.ObterUsuarioAtual();
        var resultado = _clienteService.BuildViewModel(usuario.Cliente);
        return Ok(resultado);
    }
    
    [Authorize(Roles = PerfilUsuarioExtensions.Funcionario)]
    [HttpGet("ObterPorId")]
    [ProducesResponseType(typeof(ClienteViewModel), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ObterPorId([FromRoute] int id)
    {
        var resultado = await _clienteService.ObterPorId(id);
        return Ok(resultado);
    }
    
    [Authorize(Roles = PerfilUsuarioExtensions.Funcionario)]
    [HttpGet("ObterTodos")]
    [ProducesResponseType(typeof(IEnumerable<ClienteViewModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ObterTodos()
    {
        var resultado = await _clienteService.ObterTodos();
        return Ok(resultado);
    }
}