using DivisionControl.Api.Applications.Commands.Models;
using Xunit;

namespace DivisionControl.UnitTests.Application.Dividas.Models
{
    public class RemoverDividaCommandTest
    {
        [Fact(DisplayName = "Remover Divida Inválido")]
        [Trait("Categoria", "Application - Command - Remover Divida")]
        public void RemoverDividaCommand_ComandoDeveEstaInvalido_NãoDevePassarNaValidacao()
        {
            //Arrange
            var dividaCommand = new RemoverDividaCommand(
                Guid.Empty);

            //Act
            var result = dividaCommand.EhValido();

            //Assert
            Assert.False(result);
            Assert.Contains("O Id é obrigatório.", dividaCommand.ValidationResult.Errors.Select(c => c.ErrorMessage));
        }

        [Fact(DisplayName = "Remover Divida Valido")]
        [Trait("Categoria", "Application - Command - Remover Divida")]
        public void RemoverDividaCommand_ComandoDeveEstaValido_Sucesso()
        {
            //Arrange
            var dividaCommand = new RemoverDividaCommand(
                Guid.NewGuid());

            //Act
            var result = dividaCommand.EhValido();

            //Assert
            Assert.NotNull(dividaCommand.ValidationResult);
            Assert.True(result);
        }
    }
}
