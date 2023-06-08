﻿using Microsoft.EntityFrameworkCore;
using NerdStore.Catalogo.Data.Context;
using NerdStore.Catalogo.Domain;
using NerdStore.Core.Data;

namespace NerdStore.Catalogo.Data.Repositories
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly CatalogoContext _context;
        public ProdutoRepository(CatalogoContext context)
        {
            _context = context;
        }
        public IUnitOfWork UnitOfWork => _context;

        public async Task Adicionar(Produto produto)
        {
            await _context.Produtos.AddAsync(produto);
        }

        public async Task Adicionar(Categoria categoria)
        {
            await _context.Categorias.AddAsync(categoria);
        }

        public void Atualizar(Produto produto)
        {
            _context.Produtos.Update(produto);
        }

        public void Atualizar(Categoria categoria)
        {
            _context.Categorias.Update(categoria);
        }

        public async Task<IEnumerable<Categoria>> ObterCategorias()
        {
            return await _context.Categorias.AsNoTracking().ToListAsync();
        }

        public async Task<Produto> ObterPorCategoria(Guid codigo)
        {
            return await _context.Produtos.AsNoTracking().Include(c => c.Categoria).FirstOrDefaultAsync(p => p.CategoriaId == codigo);
        }

        public async Task<Produto> ObterPorId(Guid id)
        {
            return await _context.Produtos.AsNoTracking().FirstOrDefaultAsync(p => p.Id.Equals(id));
        }

        public async Task<IEnumerable<Produto>> ObterTodos()
        {
            return await _context.Produtos.AsNoTracking().ToListAsync();
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}