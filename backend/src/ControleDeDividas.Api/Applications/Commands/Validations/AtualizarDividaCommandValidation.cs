using ControleDeDividas.Api.Applications.Commands.Models;

namespace ControleDeDividas.Api.Applications.Commands.Validations
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
