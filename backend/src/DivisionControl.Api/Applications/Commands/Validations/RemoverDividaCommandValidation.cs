using DivisionControl.Api.Applications.Commands.Models;

namespace DivisionControl.Api.Applications.Commands.Validations
{
    public class RemoverDividaCommandValidation : DividaValidation<RemoverDividaCommand>
    {
        public RemoverDividaCommandValidation()
        {
            ValidarId();
        }
    }
}
