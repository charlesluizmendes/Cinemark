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
            CreateMap<Filme, UpdateFilmeDto>();
            CreateMap<Filme, DeleteFilmeDto>();
        }
    }
}
