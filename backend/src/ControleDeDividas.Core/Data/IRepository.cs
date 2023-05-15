using ControleDeDividas.Core.DomainObjects;

namespace ControleDeDividas.Core.Data
{
    public interface IRepository<T> where T : IAggregateRoot
    {
        IUnitOfWork UnitOfWork { get; }
    }
}
