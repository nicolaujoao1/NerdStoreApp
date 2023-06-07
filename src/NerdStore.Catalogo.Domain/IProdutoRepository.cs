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
        Task<Produto>ObterPorCategoria(int codigo);
        Task Adicionar(Produto produto);
        Task Adicionar(Categoria categoria);
        Task Atualizar(Produto produto);
        Task Atualizar(Categoria categoria);
    }
}
