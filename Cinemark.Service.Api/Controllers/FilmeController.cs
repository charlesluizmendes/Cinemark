using AutoMapper;
using Cinemark.Application.Dto;
using Cinemark.Application.Events.Commands;
using Cinemark.Application.Events.Queries;
using Cinemark.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cinemark.Service.Api.Controllers
{
    [Authorize]
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

        [HttpGet]
        public async Task<ActionResult<FilmeDto>> Get()
        {
            var filmes = await _mediator.Send(new GetFilmeQuery { });

            return Ok(_mapper.Map<IEnumerable<FilmeDto>>(filmes));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<FilmeDto>> Get(int id)
        {
            var filme = await _mediator.Send(new GetFilmeByIdQuery
            {
                Id = id
            });

            return Ok(_mapper.Map<FilmeDto>(filme));
        }

        [HttpPost]
        public async Task<ActionResult<FilmeDto>> Post(CreateFilmeDto request)
        {
            var filme = await _mediator.Send(new CreateFilmeCommand
            {
                Filme = _mapper.Map<Filme>(request)
            });

            if (filme == null)
                return BadRequest("O Filme já foi Cadastrado");

            return Ok(_mapper.Map<FilmeDto>(filme));
        }

        [HttpPut]
        public async Task<ActionResult<FilmeDto>> Put(UpdateFilmeDto request)
        {
            var filme = await _mediator.Send(new UpdateFilmeCommand
            {
                Filme = _mapper.Map<Filme>(request)
            });

            return Ok(_mapper.Map<FilmeDto>(filme));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<FilmeDto>> Delete(int id)
        {
            var filme = await _mediator.Send(new GetFilmeByIdQuery
            {
                Id = id
            });

            if (filme == null)
                return BadRequest("O Filme não foi encontrado");

            await _mediator.Send(new DeleteFilmeCommand
            {
                Filme = filme
            });

            return Ok(_mapper.Map<FilmeDto>(filme));
        }
    }
}
