using NerdStore.Core.DomainObjects;

namespace NerdStore.Catalogo.Domain.ValueObjects;

public class Dimensoes
{
    public Dimensoes(decimal altura, decimal largura, decimal profundidade)
    {
        AssertionConcern.AssertArgumentLessOrEqualThan(Altura, 0, "O campo Altura não pode ser menor ou igual a 0");
        AssertionConcern.AssertArgumentLessOrEqualThan(Largura, 0, "O campo Largura não pode ser menor ou igual a 0");
        AssertionConcern.AssertArgumentLessOrEqualThan(Profundidade, 0, "O campo Profundidade não pode ser menor ou igual a 0");

        Altura = altura;
        Largura = largura;
        Profundidade = profundidade;
    }

    public decimal Altura { get; private set; }
    public decimal Largura { get; private set; }
    public decimal Profundidade { get; private set; }

    public string DescricaoFormatada() => $"LxAxP: {Largura*Altura*Profundidade}";

    public override string ToString()
    {
        return DescricaoFormatada();
    }
}
