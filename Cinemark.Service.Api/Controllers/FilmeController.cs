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
        [ProducesResponseType(typeof(FilmeDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<FilmeDto>> Get()
        {
            var filmes = await _mediator.Send(new GetFilmeQuery { });

            if (filmes == null)
                return BadRequest("Nenhum Filme foi encontrado");

            return Ok(_mapper.Map<IEnumerable<FilmeDto>>(filmes));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(FilmeDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<FilmeDto>> Get(int id)
        {
            var filme = await _mediator.Send(new GetFilmeByIdQuery
            {
                Id = id
            });

            if (filme == null)
                return BadRequest("O Filme não foi encontrado");

            return Ok(_mapper.Map<FilmeDto>(filme));
        }

        [HttpPost]
        [ProducesResponseType(typeof(FilmeDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
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
        [ProducesResponseType(typeof(FilmeDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<FilmeDto>> Put(UpdateFilmeDto request)
        {
            var filme = await _mediator.Send(new UpdateFilmeCommand
            {
                Filme = _mapper.Map<Filme>(request)
            });

            if (filme == null)
                return BadRequest("Não foi possível alterar o Filme");

            return Ok(_mapper.Map<FilmeDto>(filme));
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(FilmeDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
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
