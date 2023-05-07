using DivisionControl.Core.Communication.Messages;
using FluentValidation.Results;

namespace DivisionControl.Core.Communication.Mediator
{
    public interface IMediatorHandler
    {
        Task PublicarEvento<T>(T evento) where T : Event;
        Task<ValidationResult> EnviarComando<T>(T comando) where T : Command;
    }
}
