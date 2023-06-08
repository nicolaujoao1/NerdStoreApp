using NerdStore.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NerdStore.Catalogo.Domain
{
    public interface IProdutoRepository:IRepository<Produto>
    {
        Task<IEnumerable<Produto>> ObterTodos();
        Task<IEnumerable<Categoria>> ObterCategorias();
        Task<Produto>ObterPorId(Guid id);    
        Task<Produto>ObterPorCategoria(Guid codigo);
        Task Adicionar(Produto produto);
        Task Adicionar(Categoria categoria);
        void Atualizar(Produto produto);
        void Atualizar(Categoria categoria);
    }
}
