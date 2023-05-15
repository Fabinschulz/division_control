using ControleDeDividas.Api.Applications.Dtos;

namespace ControleDeDividas.Api.Applications.Queries
{
    public interface IDividaQuerie
    {
        Task<DividaDto> ObterPorId(Guid Id);
        Task<ICollection<DividaDto>> ObterListagem();
    }
}
