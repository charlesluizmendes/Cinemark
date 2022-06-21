using AutoMapper;
using Cinemark.Application.Dto;
using Cinemark.Domain.Entities;

namespace Cinemark.Application.AutoMapper
{
    public class DtoToEntityMappingProfile : Profile
    {
        public DtoToEntityMappingProfile()
        {
            CreateMap<CreateFilmeDto, Filme>();
            CreateMap<UpdateFilmeDto, Filme>();
            CreateMap<DeleteFilmeDto, Filme>();
        }
    }
}
