using ControleDeDividas.Api.Applications.Commands.Validations;
using ControleDeDividas.Domain.Models;

namespace ControleDeDividas.Api.Applications.Commands.Models
{
    public class AtualizarDividaCommand : DividaCommand
    {
        public AtualizarDividaCommand(Guid id, string numeroDoTitulo, string nomeDoDevedor, string cpfDoDevedor, decimal juros, decimal multa, ICollection<Parcela> parcelas)
        {
            Id = id;
            NumeroDoTitulo = numeroDoTitulo;
            NomeDoDevedor = nomeDoDevedor;
            CpfDoDevedor = cpfDoDevedor;
            Juros = juros;
            Multa = multa;
            Parcelas = parcelas;
        }
        public override bool EhValido()
        {
            ValidationResult = new AtualizarDividaCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
