using AutoMapper;
using Cinemark.Application.Dto;
using Cinemark.Application.Events.Commands;
using Cinemark.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Cinemark.Service.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilmeController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public FilmeController(IMapper mapper,
            IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }
                
        [HttpPost]
        public async Task<ActionResult<CreateFilmeDto>> Post(CreateFilmeDto request)
        {
            var filme = await _mediator.Send(new CreateFilmeCommand
            {
                Filme = _mapper.Map<Filme>(request)
            });

            if (filme == null)            
                return BadRequest("O Filme já foi Cadastrado");            

            return Ok(_mapper.Map<CreateFilmeDto>(filme));
        }
    }
}
