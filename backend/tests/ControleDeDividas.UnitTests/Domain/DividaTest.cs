using ControleDeDividas.Domain.Models;
using ControleDeDividas.UnitTests.Domain.Fixtures;
using Xunit;

namespace ControleDeDividas.UnitTests.Domain
{
    public class DividaTest
    {
        private readonly DividaTestFixture _dividaTestFixture;

        public DividaTest()
        {
            _dividaTestFixture = new DividaTestFixture();
        }

        [Fact(DisplayName = "Criar novo Divida via Fabrica CriarTitulo")]
        [Trait("Categoria", "Domain - Divida")]
        public void FactoryCriarTitulo_NovaDivida_Sucesso()
        {
            //Arrange
            var divida = _dividaTestFixture.GerarDividaValido();

            divida.AtualizarDivida("001", "Devedor Teste", "000.000.000-00", 2m, 1m);

            //Act & Assert
            Assert.NotNull(divida);
        }

        [Fact(DisplayName = "Atualizar Divida via metodo Atualizar")]
        [Trait("Categoria", "Domain - Divida")]
        public void Atualizar_AtualizarDivida_Sucesso()
        {
            //Arrange
            var divida = Divida.Factory.CriarDivida("000", "Devedor Teste", "000.000.000-00", 10, 2000);

            //Act & Assert
            Assert.NotNull(divida);
        }


        [Fact(DisplayName = "Deve Adicionar parcela com Sucesso")]
        [Trait("Categoria", "Domain - Divida")]
        public void AdicionarParcela_DeveAdicionarParcelaComSucesso()
        {
            //Arrange
            var divida = _dividaTestFixture.GerarDividaValido();
            var parcelas = _dividaTestFixture.ObterListaParcela(divida.Id);

            //Act
            divida.AdicionarParcelas(parcelas);

            //Assert
            Assert.NotNull(divida.Parcelas);
        }


        [Fact(DisplayName = "Deve Calcular o Valor Original")]
        [Trait("Categoria", "Domain - Divida")]
        public void CalcularValorOriginal_DeveCalcularDevidoValorOriginal()
        {
            //Arrange
            var divida = _dividaTestFixture.GerarDividaValido();
            var parcelas = _dividaTestFixture.ObterListaParcelaCompleta(divida.Id);

            //Act
            divida.AdicionarParcelas(parcelas);

            //Assert
            Assert.Equal(1250, divida.ValorOriginal);
        }


        [Fact(DisplayName = "Deve Calcular os Dias em Atraso Geral da Divida")]
        [Trait("Categoria", "Domain - Divida")]
        public void CalcularTotalDiasEmAtraso_DeveRetornarTotalDeDiasAtrasoCorretamente()
        {
            //Arrange
            var divida = _dividaTestFixture.GerarDividaValido();
            var parcelas = _dividaTestFixture.ObterListaParcelaCalcularCalculos(divida.Id);

            // Act
            divida.AdicionarParcelas(parcelas);
            var totalDiasAtraso = divida.DiasEmAtraso;

            // Assert
            Assert.Equal(30, totalDiasAtraso);
        }


        [Fact(DisplayName = "Deve Calcular o valor da Multa Corretamente")]
        [Trait("Categoria", "Domain - Divida")]
        public void AplicarMulta_DeveCalcularMultaCorretamente()
        {
            //Arrange
            var divida = _dividaTestFixture.GerarDividaValido();
            var parcelas = _dividaTestFixture.ObterListaParcelaCalcularCalculos(divida.Id);

            divida.AdicionarParcelas(parcelas);

            decimal valorOriginal = divida.ValorOriginal;
            decimal multaEsperada = valorOriginal * (divida.Multa / 100);
            multaEsperada = Math.Round(multaEsperada, 2);

            // Act
            decimal multaCalculada = divida.AplicarMulta(valorOriginal);

            // Assert
            Assert.Equal(multaEsperada, multaCalculada);
        }


        [Fact(DisplayName = "Deve Calcular o valor Atualizado da Divida Corretamente")]
        [Trait("Categoria", "Domain - Divida")]
        public void ValorAtualizado_DeveCalcularValorAtualizado_Corretamente()
        {
            // Arrange
            var divida = _dividaTestFixture.GerarDividaCalculoValido();
            var parcelas = _dividaTestFixture.ObterListaParcelaCalcularCalculos(divida.Id);

            // Act
            divida.AdicionarParcelas(parcelas);
            var valorAtualizado = divida.ValorAtualizado;

            // Assert
            Assert.Equal(600.40m, valorAtualizado);
        }
    }
}
