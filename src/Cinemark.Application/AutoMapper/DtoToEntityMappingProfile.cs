using AutoMapper;
using Cinemark.Application.Dto;
using Cinemark.Domain.Contracts.Commands;

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
