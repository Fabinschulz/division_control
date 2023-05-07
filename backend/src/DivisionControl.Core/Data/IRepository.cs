using DivisionControl.Core.DomainObjects;

namespace DivisionControl.Core.Data
{
    public interface IRepository<T> where T : IAggregateRoot
    {
        IUnitOfWork UnitOfWork { get; }
    }
}
