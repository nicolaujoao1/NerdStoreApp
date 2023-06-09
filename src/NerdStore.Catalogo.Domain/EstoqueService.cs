using NerdStore.Catalogo.Domain.Events;
using NerdStore.Core.MediatR;
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
        private readonly IMediatRHandler _mediatR;

        public EstoqueService(IProdutoRepository produtoRepository, IMediatRHandler mediatR)
        {
            _produtoRepository = produtoRepository;
            _mediatR = mediatR; 
        }

        public async Task<bool> DebitarEstoque(Guid produtoId, int quantidade)
        {
            var produto = await _produtoRepository.ObterPorId(produtoId);

            if (produto is null) return false;

            if (!produto.PossuiEstoque(quantidade)) return false;
            
            produto.DebitarEstoque(quantidade);

            //Aplicar Event Domain
            if (produto.QuantidadeEstoque<10)
            {
                await _mediatR.PublicarEvento<ProdutoAbaixoEstoqueEvent>(new ProdutoAbaixoEstoqueEvent(produto.Id, produto.QuantidadeEstoque));
            }
            
            _produtoRepository.Atualizar(produto);
            
            return await _produtoRepository.UnitOfWork.Commit();
        }

        public async Task<bool> ReporEstoque(Guid produtoId, int quantidade)
        {
            var produto = await _produtoRepository.ObterPorId(produtoId);

            if (produto is null) return false;
            
            produto.ReporEstoque(quantidade);

            await _produtoRepository.Adicionar(produto);

            return await _produtoRepository.UnitOfWork.Commit();
        }
        public void Dispose()
        {
            _produtoRepository.Dispose();
        }
    }
}
