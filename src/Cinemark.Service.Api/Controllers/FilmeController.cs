using AutoMapper;
using Cinemark.Application.Dto;
using Cinemark.Application.Queries;
using Cinemark.Application.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Cinemark.Domain.Core.Commom;

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
        [ProducesResponseType(typeof(ResultData<FilmeDto>), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ResultData<IEnumerable<FilmeDto>>>> Get()
        {
            var filmes = await _mediator.Send(new GetFilmeQuery());

            return CustomResponse(_mapper.Map<IEnumerable<FilmeDto>>(filmes));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ResultData<FilmeDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResultData<FilmeDto>), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ResultData<FilmeDto>>> Get(Guid id)
        {
            var filme = await _mediator.Send(new GetFilmeByIdQuery(id));

            return CustomResponse(_mapper.Map<FilmeDto>(filme));
        }

        [HttpPost]
        [ProducesResponseType(typeof(ResultData<FilmeDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResultData<FilmeDto>), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ResultData<FilmeDto>>> Post(CreateFilmeDto request)
        {
            await _mediator.Send(_mapper.Map<CreateFilmeCommand>(request));

            return CustomResponse();
        }

        [HttpPut]
        [ProducesResponseType(typeof(ResultData<FilmeDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResultData<FilmeDto>), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ResultData<FilmeDto>>> Put(UpdateFilmeDto request)
        {
            await _mediator.Send(_mapper.Map<UpdateFilmeCommand>(request));

            return CustomResponse();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ResultData<FilmeDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResultData<FilmeDto>), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<FilmeDto>> Delete(int id)
        {
            await _mediator.Send(_mapper.Map<DeleteFilmeCommand>(id));            

            return CustomResponse();
        }
    }
}
