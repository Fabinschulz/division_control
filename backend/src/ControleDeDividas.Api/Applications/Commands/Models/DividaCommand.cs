using ControleDeDividas.Core.Communication.Messages;
using ControleDeDividas.Domain.Models;

namespace ControleDeDividas.Api.Applications.Commands.Models
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
