using DivisionControl.Core.Communication.Messages;
using DivisionControl.Domain.Models;

namespace DivisionControl.Api.Applications.Commands.Models
{
    public class DividaCommand : Command
    {
        public Guid Id { get; protected init; }
        public string NumeroDoTitulo { get; protected init; }
        public string NomeDoDevedor { get; protected init; }
        public string CpfDoDevedor { get; protected init; }
        public decimal Juros { get; protected init; }
        public decimal Multa { get; protected init; }
        public ICollection<Parcela> Parcelas { get; protected init; }
    }
}
