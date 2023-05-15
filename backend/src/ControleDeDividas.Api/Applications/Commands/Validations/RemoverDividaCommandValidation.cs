using ControleDeDividas.Api.Applications.Commands.Models;

namespace ControleDeDividas.Api.Applications.Commands.Validations
{
    public class RemoverDividaCommandValidation : DividaValidation<RemoverDividaCommand>
    {
        public RemoverDividaCommandValidation()
        {
            ValidarId();
        }
    }
}
