using NerdStore.Core.DomainObjects;
using System;
using Xunit;

namespace NerdStore.Catalogo.Domain.Tests
{
    public class ProdutoTests
    {
        [Fact(DisplayName = "Catalogo - Validar Produto")]
        [Trait("Categoria", "NerdStore.Catalogo.Domain")]
        public void Produto_Validar_ValidacoesDevemRetornarExceptions()
        {
            // Arrange & Act & Assert
            var ex = Assert.Throws<DomainException>(() =>
                new Produto(
                    string.Empty,
                    "Descrição",
                    false,
                    100,
                    Guid.NewGuid(),
                    DateTime.Now,
                    "Imagem",
                    new Dimensoes(1, 1, 1)
                    )
            );

            Assert.Equal("O campo Nome do produto não pode estar vazio.", ex.Message);

            ex = Assert.Throws<DomainException>(() =>
                new Produto(
                    "Nome",
                    string.Empty,
                    false,
                    100,
                    Guid.NewGuid(),
                    DateTime.Now,
                    "Imagem",
                    new Dimensoes(1, 1, 1)
                    )
            );

            Assert.Equal("O campo Descrição do produto não pode estar vazio.", ex.Message);

            ex = Assert.Throws<DomainException>(() =>
                new Produto(
                    "Nome",
                    "Descrição",
                    false,
                    0,
                    Guid.NewGuid(),
                    DateTime.Now,
                    "Imagem",
                    new Dimensoes(1, 1, 1)
                    )
            );

            Assert.Equal("O campo Valor do produto não pode ser menor ou igual a zero.", ex.Message);

            ex = Assert.Throws<DomainException>(() =>
                new Produto(
                    "Nome",
                    "Descrição",
                    false,
                    100,
                    Guid.Empty,
                    DateTime.Now,
                    "Imagem",
                    new Dimensoes(1, 1, 1)
                    )
            );

            Assert.Equal("O campo Categoria do produto não pode estar vazio.", ex.Message);

            ex = Assert.Throws<DomainException>(() =>
                new Produto(
                    "Nome",
                    "Descrição",
                    false,
                    100,
                    Guid.NewGuid(),
                    DateTime.Now,
                    string.Empty,
                    new Dimensoes(1, 1, 1)
                    )
            );

            Assert.Equal("O campo Imagem do produto não pode estar vazio.", ex.Message);

            ex = Assert.Throws<DomainException>(() =>
                new Produto(
                    "Nome",
                    "Descrição",
                    false,
                    100,
                    Guid.NewGuid(),
                    DateTime.Now,
                    "Imagem",
                    new Dimensoes(0, 1, 1)
                    )
            );

            Assert.Equal("O campo Altura não pode ser menor ou igual a zero.", ex.Message);
        }
    }
}
