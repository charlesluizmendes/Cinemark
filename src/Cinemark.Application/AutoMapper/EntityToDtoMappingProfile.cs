using AutoMapper;
using Cinemark.Application.Dto;
using Cinemark.Domain.AggregatesModels.FilmeAggregate;
using Cinemark.Domain.AggregatesModels.UsuarioAggregate;

namespace Cinemark.Application.AutoMapper
{
    public class EntityToDtoMappingProfile : Profile
    {
        public EntityToDtoMappingProfile()
        {
            CreateMap<Filme, FilmeDto>();

            CreateMap<Token, TokenDto>();
        }
    }
}
