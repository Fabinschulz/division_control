using DivisionControl.Domain.Models;
using Xunit;

namespace DivisionControl.UnitTests.Domain
{
    public class ParcelaTest 
    {
        [Fact(DisplayName = "Criar nova Parcela via Fabrica CriarParcela")]
        [Trait("Categoria", "Domain - Parcela")]
        public void CriarParcela_CriarNovaInstancia_DeveRetornarInstanciaValida()
        {
            // Arrange
            Guid dividaId = Guid.NewGuid();
            string numeroDaParcela = "1";
            DateTime dataDeVencimento = new DateTime(2023, 5, 31);
            decimal valorDaParcela = 1000.00m;

            // Act
            Parcela parcela = Parcela.Factory.CriarParcela(dividaId, numeroDaParcela, dataDeVencimento, valorDaParcela);

            // Assert
            Assert.NotNull(parcela);
            Assert.IsType<Parcela>(parcela);
            Assert.Equal(dividaId, parcela.DividaId);
            Assert.Equal(numeroDaParcela, parcela.NumeroDaParcela);
            Assert.Equal(dataDeVencimento, parcela.DataDeVencimento);
            Assert.Equal(valorDaParcela, parcela.ValorDaParcela);
        }

        [Fact(DisplayName = "Deve Calcular os Dias em Atraso da Parcela")]
        [Trait("Categoria", "Domain - Parcela")]
        public void CalcularDiasEmAtraso_DeveCalcularDiasEmAtraso()
        {
            //Arrange
            var parcela = Parcela.Factory.CriarParcela(Guid.NewGuid(), "000", DateTime.Parse("21/09/2020"), 10);

            // Act
            var diasAtraso = parcela.DiasEmAtraso;

            // Assert
            var hoje = DateTime.Today;
            var expectedDiasAtraso = (hoje - parcela.DataDeVencimento).Days;
            Assert.Equal(expectedDiasAtraso, diasAtraso);
        }
    }
}
