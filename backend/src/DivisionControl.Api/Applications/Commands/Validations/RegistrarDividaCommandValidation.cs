using DivisionControl.Api.Applications.Commands.Models;

namespace DivisionControl.Api.Applications.Commands.Validations
{
    public class RegistrarDividaCommandValidation : DividaValidation<RegistrarDividaCommand>
    {
        public RegistrarDividaCommandValidation()
        {
            ValidarNumeroDotitulo();
            ValidarCpfDoDevedor();
            ValidarNomeDoDevedor();
            ValidarJuros();
            ValidarMulta();
            ValidarParcelas();
        }
    }
}
