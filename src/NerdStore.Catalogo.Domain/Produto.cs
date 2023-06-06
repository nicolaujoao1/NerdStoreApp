using NerdStore.Catalogo.Domain.ValueObjects;
using NerdStore.Core.DomainObjects;

namespace NerdStore.Catalogo.Domain
{
    public class Produto : Entity, IAggregateRoot
    {
        public Produto(string? nome, string? descricao, bool ativo, decimal valor, Guid categoriaId, DateTime dataCadastro, string? imagem, Dimensoes dimensoes)
        {
            CategoriaId = categoriaId;
            Nome = nome;
            Descricao = descricao;
            Ativo = ativo;
            Valor = valor;
            DataCadastro = dataCadastro;
            Imagem = imagem;
            Dimensoes = dimensoes;  

            Validar();
        }

        public Guid CategoriaId { get; private set; }
        public string? Nome { get;private set; }
        public string? Descricao { get;private set; }
        public bool Ativo { get; private set; }
        public decimal Valor { get; private set; }
        public DateTime DataCadastro { get;private set; }
        public string? Imagem { get;private set; }
        public int QuantidadeEstoque { get; private set; }
        public Categoria? Categoria { get;private set; }
        public Dimensoes Dimensoes { get; private set; }


        public void Ativar() => Ativo = true;
        public void Desativar() => Ativo = false;
        public void AlterarCategoria(Categoria categoria)
        {
            Categoria = categoria;
            CategoriaId = Categoria.Id;
        }
        public void AlterarDescricao(string descricao)
        {
            AssertionConcern.AssertArgumentNotEmpty(descricao, "O campo Descrição não pode estar vazio");
            Descricao =descricao;
        }
        public void DebitarEstoque(int quantidade)
        {
            if (quantidade < 0) quantidade *= -1;
            if (!PossuiEstoque(quantidade)) throw new Exception("Estoque insuficiente");
            QuantidadeEstoque -= quantidade;
        }
        public void ReporEstoque(int quantidade)
        {
            QuantidadeEstoque += quantidade;
        }
        public bool PossuiEstoque(int quantidade)
        => QuantidadeEstoque >= quantidade;
        public void Validar()
        {
            AssertionConcern.AssertArgumentNotEmpty(Nome, "O campo Nome do produto não pode estar vazio");
            AssertionConcern.AssertArgumentNotEmpty(Descricao, "O campo Descrição não pode estar vazio");
            AssertionConcern.AssertArgumentNotEquals(CategoriaId, Guid.Empty, "O campo CategoriaId do produto não pode estar vazio");
            AssertionConcern.AssertArgumentLessOrEqualThan(Valor, 0, "O valor do produto não pode ser menor ou igual a 0");
            AssertionConcern.AssertArgumentNotEmpty(Imagem, "O campo Imagem do produto não pode estar vazio");
        }
    }
}
