using AutoMapper;
using ControleDeDividas.Api.Applications.Dtos;
using ControleDeDividas.Domain.Interfaces;

namespace ControleDeDividas.Api.Applications.Queries
{
    public class DividaQuerie : IDividaQuerie
    {
        private readonly IDividaRepository _dividaRepository;
        private readonly IMapper _mapper;

        public DividaQuerie(IDividaRepository dividaRepository, IMapper mapper)
        {
            _dividaRepository = dividaRepository;
            _mapper = mapper;
        }

        public async Task<ICollection<DividaDto>> ObterListagem()
        {
            return _mapper.Map<ICollection<DividaDto>>(await _dividaRepository.ObterListagem());
        }

        public async Task<DividaDto> ObterPorId(Guid Id)
        {
            return _mapper.Map<DividaDto>(await _dividaRepository.ObterPorId(Id));
        }
    }
}
