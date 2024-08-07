﻿using Ecommerce.Application.Model.Produto;
using Ecommerce.Application.ModelResult.Produto;
using Ecommerce.Application.Services;
using Ecommerce.Domain.Entities.Pessoas.Autenticacao;
using Ecommerce.Domain.Entities.Produtos;
using Ecommerce.Domain.Services;
using Ecommerce.Infra.Auth.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controller
{
   // [Authorize]
    [Route("api/[Controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private readonly ICategoriaService _categoriaservice;
        private readonly ILogger<CategoriaController> _logger;
        public CategoriaController(ICategoriaService categoriaservice, ILogger<CategoriaController> logger)
        {
            _categoriaservice= categoriaservice;
            _logger = logger;
        }

        /// <summary>
        /// Cadastrar categoria
        /// </summary>
        /// <param name="categoria"></param>
        /// <returns></returns>
        //[Authorize(Roles = PerfilUsuarioExtensions.Funcionario)]
        [ProducesResponseType(typeof(CategoriaModelResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [HttpPost]
        [Route("Cadastrar")]
        public IActionResult Cadastrar([FromBody] CategoriaViewModel categoria)
        {
            if (categoria == null || string.IsNullOrEmpty(categoria.Nome) || string.IsNullOrEmpty(categoria.Descricao))
            {
                return BadRequest();
            }

            try
            {
                var result = _categoriaservice.Cadastrar(categoria);
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }


        /// <summary>
        /// Obter categoria por ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(typeof(CategoriaModelResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [Route("ObterPorId/{id}")]
        public IActionResult ObterPorId(int id)
        {
            var result = _categoriaservice.ObterPorId(id);

            if (result != null)
                return Ok(result);

            return NoContent();
        }

        /// <summary>
        /// Obter todas as categorias
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CategoriaModelResult>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [Route("ObterPorTodos")]
        public IActionResult Otertodos()
        {
            var result = _categoriaservice.ObterTodos();

            if (result != null)
                return Ok(result);

            return NoContent();
        }

        /// <summary>
        /// Alterar categoria
        /// </summary>
        /// <param name="categoria"></param>
        /// <returns></returns>
       // [Authorize(Roles = PerfilUsuarioExtensions.Funcionario)]
        [ProducesResponseType(typeof(CategoriaModelResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [HttpPut]
        [Route("Alterar")]
        public IActionResult Alterar([FromBody] CategoriaViewModel categoria)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = _categoriaservice.Alterar(categoria);

            return Ok(result);

        }        
    }
}
