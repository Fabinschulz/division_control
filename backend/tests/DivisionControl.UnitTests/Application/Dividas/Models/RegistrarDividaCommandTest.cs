using DivisionControl.Api.Applications.Commands.Models;
using DivisionControl.Domain.Models;
using DivisionControl.UnitTests.Domain.Fixtures;
using Xunit;

namespace DivisionControl.UnitTests.Application.Dividas.Models
{
    public class RegistrarDividaCommandTest
    {
        private readonly DividaTestFixture _dividaTestFixture;

        public RegistrarDividaCommandTest()
        {
            _dividaTestFixture = new DividaTestFixture();
        }

        [Fact(DisplayName = "Registro de Divida Valido")]
        [Trait("Categoria", "Application - Command - Registro Divida")]
        public void RegistrarDividaCommand_ComandoDeveEstaValido_Sucesso()
        {
            //Arrange
            var divida = _dividaTestFixture.GerarDividaValido();
            var parcelas = _dividaTestFixture.ObterListaParcela(divida.Id);

            var dividaCommand = new RegistrarDividaCommand(
                divida.NumeroDoTitulo,
                divida.NomeDoDevedor,
                divida.CpfDoDevedor,
                divida.Juros,
                divida.Multa,
                parcelas);

            //Act
            var result = dividaCommand.EhValido();

            //Assert
            Assert.NotNull(dividaCommand.ValidationResult);
            Assert.True(result);
        }

        [Fact(DisplayName = "Registro de Divida Inválido")]
        [Trait("Categoria", "Application - Command - Registro Divida")]
        public void RegistrarDividaCommand_ComandoDeveEstaInvalido_NãoDevePassarNaValidacao()
        {
            //Arrange
            var divida = Divida.Factory.CriarDivida(string.Empty, string.Empty, string.Empty, 0, 0);

            var dividaCommand = new RegistrarDividaCommand(
                divida.NumeroDoTitulo,
                divida.NomeDoDevedor,
                divida.CpfDoDevedor,
                divida.Juros,
                divida.Multa,
                divida.Parcelas);

            //Act
            var result = dividaCommand.EhValido();

            //Assert
            Assert.False(result);
            Assert.Contains("O número do titulo deve ser informado.", dividaCommand.ValidationResult.Errors.Select(c => c.ErrorMessage));
            Assert.Contains("O nome do devedor deve ser informado.", dividaCommand.ValidationResult.Errors.Select(c => c.ErrorMessage));
            Assert.Contains("A dívida deve ter pelo menos uma parcela.", dividaCommand.ValidationResult.Errors.Select(c => c.ErrorMessage));
            Assert.Contains("O CPF do devedor deve ser informado.", dividaCommand.ValidationResult.Errors.Select(c => c.ErrorMessage));
            Assert.Contains("A multa da divida deve ser informado.", dividaCommand.ValidationResult.Errors.Select(c => c.ErrorMessage));
            Assert.Contains("O juros da divida deve ser informado.", dividaCommand.ValidationResult.Errors.Select(c => c.ErrorMessage));
        }
    }
}
