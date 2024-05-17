using Ecommerce.Application.Model.Produto;
using Ecommerce.Application.ModelResult.Produto;
using Ecommerce.Domain.Entities.Produtos;
using Ecommerce.Domain.Exceptions;
using Ecommerce.Domain.Repository;
using Ecommerce.Domain.Services;
using Ecommerce.Infra.ServiceBus.Interface;

namespace Ecommerce.Application.Services
{
    public class CategoriaService : ICategoriaService
    {

        private readonly ICategoriaRepository _categoriaRepository;
        private readonly IServiceBus _serviceBus;

        public CategoriaService(ICategoriaRepository categoriaRepository, IServiceBus serviceBus)
        {
            _categoriaRepository = categoriaRepository;
            _serviceBus = serviceBus;
        }

        public CategoriaModelResult Alterar(CategoriaViewModel model)
        {
            var entity =  buidCategoria(model);

            var categoria = ObterPorId(entity.Id);

            if (categoria is null)
                throw RequisicaoInvalidaException.PorMotivo($"Erro: A Categoria {entity.Id} não está cadastrada na Base!");            

            _serviceBus.SendMessage(model, "categoriaupdatequeue");

            return BuildModelResult(entity);
        }

        public CategoriaModelResult Cadastrar(CategoriaViewModel model)
        {
            var categoria = buidCategoria(model);

            if (ObterTodos().Where(x => x != null)
                .Any(x => x.Nome.Equals(categoria.Nome)))
                throw RequisicaoInvalidaException.PorMotivo($"Erro: A Categoria {categoria.Nome} Já está cadastrada!");


            _serviceBus.SendMessage(categoria, "categoriainsertqueue");

            //_categoriaEfRepository.Cadastrar(categoria);

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

            return new CategoriaViewModel(categoria.Nome, categoria.Descricao, categoria.Ativo);
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

            return new Categoria(model.Descricao, model.Nome, model.Ativo);

        }
    }
}
