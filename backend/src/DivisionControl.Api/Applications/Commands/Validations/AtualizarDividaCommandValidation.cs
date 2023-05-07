using DivisionControl.Api.Applications.Commands.Models;

namespace DivisionControl.Api.Applications.Commands.Validations
{
    public class AtualizarDividaCommandValidation : DividaValidation<AtualizarDividaCommand>
    {
        public AtualizarDividaCommandValidation()
        {
            ValidarId();
            ValidarNumeroDotitulo();
            ValidarCpfDoDevedor();
            ValidarNomeDoDevedor();
            ValidarJuros();
            ValidarMulta();
            ValidarParcelas();
        }
    }
}
