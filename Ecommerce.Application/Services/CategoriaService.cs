using Ecommerce.Application.Model.Produto;
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

        public Categoria Alterar(Categoria model)
        {
           
            var categoria = ObterPorId(model.Id);

            if (categoria is null)
                throw RequisicaoInvalidaException.PorMotivo($"Erro: A Categoria {model.Id} não está cadastrada na Base!");    

            var obj = _categoriaEfRepository.ObterPorId(model.Id);

            obj.Descricao = model.Descricao;
            obj.Nome = model.Nome;
            obj.Ativo = model.Ativo;
            
            _categoriaEfRepository.Alterar(obj);

           // _serviceBus.SendMessage(model, "categoriaupdatequeue");

            return BuildModelResult(obj);
        }

        public CategoriaViewModel Cadastrar(CategoriaViewModel model)
        {
            var categoria = buidCategoria(model);

            if (ObterTodos().Where(x=> x!=null)
                .Any(x => x.Nome.Equals(categoria.Nome)))
                throw RequisicaoInvalidaException.PorMotivo($"Erro: A Categoria {categoria.Nome} Já está cadastrada!");

            //_categoriaRepository.Cadastrar(categoria);
            _serviceBus.SendMessage(categoria,"categoriainsertqueue");


            var categoriaViewModel = BuildViewModel(categoria);

            return categoriaViewModel;
        }

        public Categoria ObterPorId(int id)
        {
            var result = _categoriaRepository.ObterPorId(id);
            return result;
        }

        public  IList<Categoria> ObterTodos()
        {
            return _categoriaRepository.ObterTodos();
        }

        private CategoriaViewModel BuildViewModel(Categoria categoria)
        {
            if (categoria is null) 
                return null;

            return new CategoriaViewModel(categoria.Nome, categoria.Descricao, categoria.Ativo);
        }

        private Categoria buidCategoria(CategoriaViewModel model)
        {
            if (model is null)
                return null;

            return new Categoria(model.Descricao, model.Nome, model.Ativo);

        }
    }
}
