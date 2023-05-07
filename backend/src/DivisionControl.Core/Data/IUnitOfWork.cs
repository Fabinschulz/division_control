namespace DivisionControl.Core.Data
{
    public interface IUnitOfWork
    {
        Task<bool> Commit();
    }
}
