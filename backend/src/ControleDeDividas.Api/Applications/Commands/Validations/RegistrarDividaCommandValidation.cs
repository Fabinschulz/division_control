using ControleDeDividas.Api.Applications.Commands.Models;

namespace ControleDeDividas.Api.Applications.Commands.Validations
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
