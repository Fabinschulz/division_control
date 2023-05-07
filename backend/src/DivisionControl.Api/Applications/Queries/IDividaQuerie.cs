using DivisionControl.Api.Applications.Dtos;

namespace DivisionControl.Api.Applications.Queries
{
    public interface IDividaQuerie
    {
        Task<DividaDto> ObterPorId(Guid Id);
        Task<ICollection<DividaDto>> ObterListagem();
    }
}
