using NerdStore.Core.DomainObjects;

namespace NerdStore.Catalogo.Domain
{
    public class Categoria : Entity
    {
        public Categoria(int codigo, string? nome)
        {
            Codigo = codigo;
            Nome = nome;
            Validar();
        }

        public int Codigo { get;private set; }
        public string? Nome { get; private set; }

       
        public override string ToString()
        {
            return $"{Nome} - {Codigo}";
        }
        public void Validar()
        {
            AssertionConcern.AssertArgumentNotEmpty(Nome, "O campo Nome não pode estar vazio.");
            AssertionConcern.AssertArgumentEquals(Codigo, 0,"O campo codigo não pode ser igual a zero(0).");
        }
    }
}
