using NerdStore.Core.DomainObjects;

namespace NerdStore.Catalogo.Domain
{
    public class Dimensoes
    {
        public Dimensoes(decimal altura, decimal largura, decimal profundidade)
        {
            Validacoes.ValidarSeMenorIgualMinimo(altura, 0, "O campo Altura não pode ser menor ou igual a zero.");
            Validacoes.ValidarSeMenorIgualMinimo(largura, 0, "O campo Largura não pode ser menor ou igual a zero.");
            Validacoes.ValidarSeMenorIgualMinimo(profundidade, 0, "O campo Profundidade não pode ser menor ou igual a zero.");

            Altura = altura;
            Largura = largura;
            Profundidade = profundidade;
        }

        public decimal Altura { get; private set; }
        public decimal Largura { get; private set; }
        public decimal Profundidade { get; private set; }

        public string DescricaoFormatada()
        {
            return $"LxAxP: {Largura} x {Altura} x {Profundidade}";
        }

        public override string ToString()
        {
            return DescricaoFormatada();
        }
    }
}
