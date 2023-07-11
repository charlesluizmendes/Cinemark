using AutoMapper;
using Cinemark.Application.Commands;
using Cinemark.Application.Dto;

namespace Cinemark.Application.AutoMapper
{
    public class DtoToEntityMappingProfile : Profile
    {
        public DtoToEntityMappingProfile()
        {            
            CreateMap<CreateFilmeDto, CreateFilmeCommand>();
            CreateMap<UpdateFilmeDto, UpdateFilmeCommand>(); 
            CreateMap<CreateTokenDto, CreateTokenByEmailAndSenhaCommand>();
        }
    }
}
