using AutoMapper;
using Cinemark.Application.Dto;
using Cinemark.Application.Events.Commands;
using Cinemark.Application.Events.Queries;
using Cinemark.Domain.Models;
using Cinemark.Domain.Models.Commom;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cinemark.Service.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class FilmeController : BaseController
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
        [ProducesResponseType(typeof(ResultData<FilmeDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResultData<FilmeDto>), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ResultData<FilmeDto>>> Get()
        {
            var filmes = await _mediator.Send(new GetFilmeQuery { });

            if (filmes == null)
                return NotFound("Nenhum Filme foi encontrado");

            return Ok(_mapper.Map<ResultData<IEnumerable<FilmeDto>>>(filmes));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ResultData<FilmeDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResultData<FilmeDto>), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ResultData<FilmeDto>>> Get(int id)
        {
            var filme = await _mediator.Send(new GetFilmeByIdQuery
            {
                Id = id
            });

            if (filme == null)
                return NotFound("O Filme não foi encontrado");

            return Ok(_mapper.Map<ResultData<FilmeDto>>(filme));
        }

        [HttpPost]
        [ProducesResponseType(typeof(ResultData<FilmeDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResultData<FilmeDto>), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ResultData<FilmeDto>>> Post(CreateFilmeDto request)
        {
            var filme = await _mediator.Send(new CreateFilmeCommand
            {
                Filme = _mapper.Map<Filme>(request)
            });

            if (filme == null)
                return BadRequest("O Filme já foi Cadastrado");

            return Ok(_mapper.Map<ResultData<FilmeDto>>(filme));
        }

        [HttpPut]
        [ProducesResponseType(typeof(ResultData<FilmeDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResultData<FilmeDto>), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ResultData<FilmeDto>>> Put(UpdateFilmeDto request)
        {
            var filme = await _mediator.Send(new UpdateFilmeCommand
            {
                Filme = _mapper.Map<Filme>(request)
            });

            if (filme == null)
                return BadRequest("Não foi possível alterar o Filme");

            return Ok(_mapper.Map<ResultData<FilmeDto>>(filme));
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ResultData<FilmeDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResultData<FilmeDto>), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<FilmeDto>> Delete(int id)
        {
            var filme = await _mediator.Send(new GetFilmeByIdQuery
            {
                Id = id
            });

            if (filme == null)
                return NotFound("O Filme não foi encontrado");

            await _mediator.Send(new DeleteFilmeCommand
            {
                Filme = filme
            });

            return Ok(_mapper.Map<ResultData<FilmeDto>>(filme));
        }
    }
}
