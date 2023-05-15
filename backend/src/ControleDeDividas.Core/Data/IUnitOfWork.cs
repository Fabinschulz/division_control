namespace ControleDeDividas.Core.Data
{
    public interface IUnitOfWork
    {
        Task<bool> Commit();
    }
}
