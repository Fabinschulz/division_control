using DivisionControl.Api.Applications.Commands.Models;
using DivisionControl.Core.Utils;
using FluentValidation;

namespace DivisionControl.Api.Applications.Commands.Validations
{
    public abstract class DividaValidation<T> : AbstractValidator<T> where T : DividaCommand
    {
        protected void ValidarId()
        {
            RuleFor(c => c.Id)
                .NotEqual(Guid.Empty)
                .WithMessage("O Id é obrigatório.");
        }

        protected void ValidarNumeroDotitulo()
        {
            RuleFor(c => c.NumeroDoTitulo)
               .NotEmpty()
               .WithMessage("O número do titulo deve ser informado.");
        }

        protected void ValidarNomeDoDevedor()
        {
            RuleFor(c => c.NomeDoDevedor)
               .NotEmpty()
               .WithMessage("O nome do devedor deve ser informado.");
        }

        protected void ValidarJuros()
        {
            RuleFor(c => c.Juros)
               .NotEmpty()
               .WithMessage("O juros da divida deve ser informado.");
        }

        protected void ValidarMulta()
        {
            RuleFor(c => c.Multa)
               .NotEmpty()
               .WithMessage("A multa da divida deve ser informado.");
        }

        protected void ValidarParcelas()
        {
            RuleFor(c => c.Parcelas)
                .Must(p => p?.Count > 0)
                .WithMessage("A dívida deve ter pelo menos uma parcela.");
        }

        protected void ValidarCpfDoDevedor()
        {
            RuleFor(c => c.CpfDoDevedor)
                   .NotEmpty()
                   .WithMessage("O CPF do devedor deve ser informado.");

            RuleFor(c => c.CpfDoDevedor)
               .Must(HasValidarCpf)
               .When(c => !string.IsNullOrEmpty(c.CpfDoDevedor))
               .WithMessage("O CPF informado não é valido.");
        }

        protected static bool HasValidarCpf(string cpf)
        {
            return CpfValidacao.EhCpf(cpf);
        }
    }
}
