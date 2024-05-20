using Ecommerce.Application.Model.Produto;
using Ecommerce.Application.ModelResult.Produto;
using Ecommerce.Domain.Entities.Produtos;
using Ecommerce.Domain.Exceptions;
using Ecommerce.Domain.Interfaces.EFRepository;
using Ecommerce.Domain.Repository;
using Ecommerce.Domain.Services;
using Ecommerce.Infra.ServiceBus.Interface;

namespace Ecommerce.Application.Services
{
    public class CategoriaService : ICategoriaService
    {

        private readonly ICategoriaRepository _categoriaRepository;
        private readonly IServiceBus _serviceBus;
        private readonly ICategoriaEfRepository _categoriaEfRepository;

        public CategoriaService(ICategoriaRepository categoriaRepository, IServiceBus serviceBus, ICategoriaEfRepository categoriaEfRepository)
        {
            _categoriaRepository = categoriaRepository;
            _serviceBus = serviceBus;
            _categoriaEfRepository = categoriaEfRepository;
        }

        public CategoriaModelResult Alterar(CategoriaViewModel model)
        {
           
            var categoria = ObterPorId(model.Id);

            if (categoria is null)
                throw RequisicaoInvalidaException.PorMotivo($"Erro: A Categoria {model.Id} não está cadastrada na Base!");    

            var obj = _categoriaEfRepository.ObterPorId(model.Id);

            obj.Descricao = model.Descricao;
            obj.Nome = model.Nome;
            obj.Ativo = model.Ativo;
            
            _categoriaEfRepository.Alterar(obj);

           _serviceBus.SendMessage(model, "categoriaupdatequeue");

            return BuildModelResult(obj);
        }

        public CategoriaModelResult Cadastrar(CategoriaViewModel model)
        {
            var categoria = buidCategoria(model);

            if (ObterTodos().Where(x => x != null)
                .Any(x => x.Nome.Equals(categoria.Nome)))
                throw RequisicaoInvalidaException.PorMotivo($"Erro: A Categoria {categoria.Nome} Já está cadastrada!");


           _serviceBus.SendMessage(categoria, "categoriainsertqueue");

            return BuildModelResult(categoria);
        }

        public CategoriaModelResult ObterPorId(int id)
        {
            //var result = _categoriaRepository.ObterPorId(id);
            var result = _categoriaEfRepository.ObterPorId(id);


            return BuildModelResult(result);
        }

        public IList<CategoriaModelResult> ObterTodos()
        {
            var resultList = new List<CategoriaModelResult>();


            var result = _categoriaEfRepository.ObterTodos();

            foreach (var item in result)
                resultList.Add(BuildModelResult(item));

            return resultList;


        }

        private CategoriaViewModel BuildViewModel(Categoria categoria)
        {
            if (categoria is null)
                return null;

            return new CategoriaViewModel(categoria.Nome, categoria.Descricao, categoria.Ativo, categoria.Id);
        }

        private CategoriaModelResult BuildModelResult(Categoria categoria)
        {
            if (categoria is null)
                return null;

            return new CategoriaModelResult(categoria.Nome, categoria.Descricao, categoria.Ativo);
        }

        private Categoria buidCategoria(CategoriaViewModel model)
        {
            if (model is null)
                return null;

            return new Categoria(model.Descricao, model.Nome, model.Ativo, model.Id);

        }

        public void AlterarQueue(Categoria entidade)
        {
            var obj = _categoriaEfRepository.ObterPorId(entidade.Id);

            obj.Descricao = entidade.Descricao;
            obj.Nome = entidade.Nome;
            obj.Ativo = entidade.Ativo;

            _categoriaEfRepository.Alterar(obj);

           
        }
    }
}
