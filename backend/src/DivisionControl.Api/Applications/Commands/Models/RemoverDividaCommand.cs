using DivisionControl.Api.Applications.Commands.Validations;

namespace DivisionControl.Api.Applications.Commands.Models
{
    public class RemoverDividaCommand : DividaCommand
    {
        public RemoverDividaCommand(Guid id)
        {
            Id = id;
        }
        public override bool EhValido()
        {
            ValidationResult = new RemoverDividaCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
