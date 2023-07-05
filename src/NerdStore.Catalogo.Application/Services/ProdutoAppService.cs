using AutoMapper;
using NerdStore.Catalogo.Application.ViewModels;
using NerdStore.Catalogo.Domain;
using NerdStore.Core.DomainObjects;

namespace NerdStore.Catalogo.Application.Services
{
    public class ProdutoAppService : IProdutoAppService
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly IEstoqueService _estoqueService;
        private readonly IMapper _mapper;
        public ProdutoAppService(IProdutoRepository produtoRepository, IMapper mapper, IEstoqueService estoqueService)
        {
            _produtoRepository = produtoRepository;
            _mapper = mapper;
            _estoqueService = estoqueService;
        }

        public async Task AdicionarProduto(ProdutoViewModel produtoViewModel)
        {
            var produto = _mapper.Map<Produto>(produtoViewModel);

            await _produtoRepository.Adicionar(produto);

            await _produtoRepository.UnitOfWork.Commit();
        }

        public async Task AtualizarProduto(ProdutoViewModel produtoViewModel)
        {
            var produto = _mapper.Map<Produto>(produtoViewModel);

            _produtoRepository.Atualizar(produto);

            await _produtoRepository.UnitOfWork.Commit();

        }

        public async Task<ProdutoViewModel> DebitarEstoque(Guid id, int quantidade)
        {
            //Result e await teriam a mesma função
            var existeEstoque =  _estoqueService.DebitarEstoque(id, quantidade).Result;
            if (!existeEstoque)
            {
                throw new DomainException("Falha ao Debitar o Estoque");
            }
            else
            {
                return _mapper.Map<ProdutoViewModel>(await _produtoRepository.ObterPorId(id));
            }
        }

        public void Dispose()
        {
            _produtoRepository.Dispose();   
        }

        public async Task<IEnumerable<ProdutoViewModel>> ObterPorCategoria(int codigo)
        {
            return _mapper.Map<IEnumerable<ProdutoViewModel>>(await _produtoRepository.ObterPorCategoria(codigo));
        }

        public async Task<ProdutoViewModel> ObterPorId(Guid codigo)
        {
            return _mapper.Map<ProdutoViewModel>(await _produtoRepository.ObterPorId(codigo));
        }

        public async Task<IEnumerable<ProdutoViewModel>> ObterTodos()
        {
            return _mapper.Map<IEnumerable<ProdutoViewModel>>(await _produtoRepository.ObterTodos());
        }

        public async Task<ProdutoViewModel> ReporEstoque(Guid id, int quantidade)
        {
            var existeEstoque = await _estoqueService.ReporEstoque(id, quantidade);
            if (!existeEstoque)
            {
                throw new DomainException("Falha ao repor o estoque.");
            }
            else
            {
                return _mapper.Map<ProdutoViewModel>(await _produtoRepository.ObterPorId(id));
            }
        }
    }
}
