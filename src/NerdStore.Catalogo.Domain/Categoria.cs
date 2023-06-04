using NerdStore.Core.DomainObjects;

namespace NerdStore.Catalogo.Domain
{
    public class Categoria : Entity
    {
        public int Codigo { get;private set; }
        public string? Nome { get; private set; }
        public override string ToString()
        {
            return $"{Nome} - {Codigo}";
        }
    }
}
