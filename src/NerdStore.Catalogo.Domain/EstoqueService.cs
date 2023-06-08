using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NerdStore.Catalogo.Domain
{
    public class EstoqueService : IEstoqueService
    {
        private readonly IProdutoRepository _produtoRepository;

        public EstoqueService(IProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }

        public async Task<bool> DebitarEstoque(Guid produtoId, int quantidade)
        {
            var produto = await _produtoRepository.ObterPorId(produtoId);

            if (produto is null) return false;

            if (!produto.PossuiEstoque(quantidade)) return false;
            
            produto.DebitarEstoque(quantidade);
            
            _produtoRepository.Atualizar(produto);
            
            return await _produtoRepository.UnitOfWork.Commit();
        }

        public Task<bool> ReporEstoque(Guid produtoId, int quantidade)
        {
            throw new NotImplementedException();
        }
        public void Dispose()
        {
            _produtoRepository.Dispose();
        }
    }
}
