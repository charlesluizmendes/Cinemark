using AutoMapper;
using Cinemark.Application.Dto;
using Cinemark.Domain.Models;
using Cinemark.Domain.Models.Commom;

namespace Cinemark.Application.AutoMapper
{
    public class EntityToDtoMappingProfile : Profile
    {
        public EntityToDtoMappingProfile()
        {
            CreateMap<SuccessData<IEnumerable<Filme>>, ResultData<IEnumerable<FilmeDto>>>();
            CreateMap<ErrorData<IEnumerable<Filme>>, ResultData<IEnumerable<FilmeDto>>>();
            CreateMap<SuccessData<Filme>, ResultData<FilmeDto>>();
            CreateMap<ErrorData<Filme>, ResultData<FilmeDto>>();
            CreateMap<Filme, FilmeDto>();

            CreateMap<SuccessData<Token>, ResultData<TokenDto>>();
            CreateMap<ErrorData<Token>, ResultData<TokenDto>>();
            CreateMap<Token, TokenDto>();
        }
    }
}
