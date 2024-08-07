﻿using Ecommerce.Application.Model.Produto;
using Ecommerce.Domain.Entities.Produtos;
using Ecommerce.Domain.Repository;
using Ecommerce.Domain.Services;
using Ecommerce.Domain.Exceptions;
using FluentValidation.Validators;
using Ecommerce.Infra.ServiceBus.Interface;
using Ecommerce.Domain.Interfaces.Repository;
using Ecommerce.Domain.Interfaces.EFRepository;
using Ecommerce.Application.Model.Pessoas.Produto;
using Ecommerce.Application.ModelResult.Produto;

namespace Ecommerce.Application.Services
{
    public class FabricanteService : IFabricanteService
    {

        private readonly IFabricanteRepository _fabricanteRepository;
        private readonly IServiceBus _serviceBus;
        private readonly IFabricanteEfRepository _fabricanteEfRepository;
   

        public FabricanteService( IServiceBus serviceBus, IFabricanteEfRepository fabricanteEfRepository,
            IFabricanteRepository fabricanteRepository
            )
        {
            
            _serviceBus = serviceBus;
           
            _fabricanteEfRepository = fabricanteEfRepository;

            _fabricanteRepository = fabricanteRepository;

        }

        public FabricanteModelResult Alterar(FabricanteViewModel entidade)
        {
            var fabricante = ObterPorId(entidade.Id);

            if (fabricante is null)
                throw RequisicaoInvalidaException.PorMotivo($"O Fabricante {entidade.Id} não está cadastrado na Base");

            var entity = BuidFabricante(entidade);

            //_serviceBus.SendMessage(entity, "fabricanteupdatequeue");

            _fabricanteRepository.Alterar(entity);

            return null;
        }

        public FabricanteModelResult Cadastrar(FabricanteViewModel model)
        {
            var fabricante = BuidFabricante(model);

            var endereco = BuildEndereco(model.Endereco);

            fabricante.CNPJ = fabricante.ObterCnpjSemFormatacao();

            if (!validaCNPJ(fabricante.CNPJ))
                throw RequisicaoInvalidaException.PorMotivo($"CNPJ {fabricante.CNPJ} inválido");

            if (ObterTodos().Where(x => x.CNPJ != null)
                .Any(x => x.CNPJ.Equals(fabricante.CNPJ)))
                throw RequisicaoInvalidaException.PorMotivo($"O fabrinte {fabricante.CNPJ} Já está cadastrado na base!");

            fabricante.Endereco = endereco;

            //_serviceBus.SendMessage(fabricante, "fabricanteinsertqueue");
            _fabricanteRepository.Cadastrar(fabricante);

            return BuidModelResult(fabricante);
        }

        public void Deletar(int id)
        {
            throw new NotImplementedException();
        }

        public FabricanteModelResult ObterPorId(int id)
        {
            return BuidModelResult(_fabricanteRepository.ObterPorId(id));
        }

        public IList<FabricanteModelResult> ObterTodos()
        {
            var listResult = new List<FabricanteModelResult>();

            var result = _fabricanteRepository.ObterTodos();

            foreach (var item in result)
                listResult.Add(BuidModelResult(item));

            return listResult;
            
        }

        private FabricanteViewModel BuildViewModel(Fabricante fabricante)
        {
            if (fabricante is null)
                return null;

            return new FabricanteViewModel(fabricante.Nome, fabricante.Ativo, fabricante.CNPJ, fabricante.Id);
        }

        private Fabricante BuidFabricante(FabricanteViewModel model)
        {
            if (model is null)
                return null;

            return new Fabricante(model.Nome, model.CNPJ, model.Ativo);

        }


        private FabricanteModelResult BuidModelResult(Fabricante model)
        {
            if (model is null)
                return null;

            return new FabricanteModelResult(model.Nome,  model.Ativo, model.CNPJ);

        }

        public bool validaCNPJ(string cnpj)
        {
            int[] multiplicador1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int soma;
            int resto;
            string digito;
            string tempCnpj;
            cnpj = cnpj.Trim();
            cnpj = cnpj.Replace(".", "").Replace("-", "").Replace("/", "");
            if (cnpj.Length != 14)
                return false;
            tempCnpj = cnpj.Substring(0, 12);
            soma = 0;
            for (int i = 0; i < 12; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];
            resto = (soma % 11);
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = resto.ToString();
            tempCnpj = tempCnpj + digito;
            soma = 0;
            for (int i = 0; i < 13; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];
            resto = (soma % 11);
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = digito + resto.ToString();
            return cnpj.EndsWith(digito);
        }

        public Endereco BuildEndereco(EnderecoViewModel model) 
        {
            return new Endereco(model.Logradouro,model.Numero,model.CEP,model.Bairro,model.Cidade,model.Estado);        
        }
    }
}
