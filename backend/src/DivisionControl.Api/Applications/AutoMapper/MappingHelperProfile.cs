using AutoMapper;
using DivisionControl.Api.Applications.Commands.Models;
using DivisionControl.Api.Applications.Dtos;
using DivisionControl.Domain.Models;

namespace DivisionControl.Api.Applications.AutoMapper
{
    public class MappingHelperProfile : Profile
    {
        public MappingHelperProfile()
        {
            #region ViewModels e Models
            CreateMap<RegistrarDividaDto, Divida>().ReverseMap();
            CreateMap<AtualizarDividaDto, Divida>().ReverseMap();
            CreateMap<DividaDto, Divida>().ReverseMap();
            CreateMap<ParcelasDto, Parcela>().ReverseMap();
            #endregion

            #region ViewModels e Commands
            CreateMap<RegistrarDividaDto, RegistrarDividaCommand>().ReverseMap();
            CreateMap<AtualizarDividaDto, AtualizarDividaCommand>().ReverseMap();
            #endregion
        }
    }
}
