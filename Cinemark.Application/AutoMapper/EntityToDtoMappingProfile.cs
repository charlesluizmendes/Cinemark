using AutoMapper;
using Cinemark.Application.Dto;
using Cinemark.Domain.Models;

namespace Cinemark.Application.AutoMapper
{
    public class EntityToDtoMappingProfile : Profile
    {
        public EntityToDtoMappingProfile()
        {
            CreateMap<Filme, CreateFilmeDto>();
            CreateMap<Filme, FilmeDto>();
            CreateMap<Token, TokenDto>();
            CreateMap<Filme, UpdateFilmeDto>();
            CreateMap<Usuario, GetTokenDto>();
        }
    }
}
