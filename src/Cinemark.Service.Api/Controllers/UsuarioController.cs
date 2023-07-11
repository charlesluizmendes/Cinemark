using AutoMapper;
using Cinemark.Application.Commands;
using Cinemark.Application.Dto;
using Cinemark.Domain.Commom;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Cinemark.Service.Api.Controllers
{
    [Route("api/[controller]")]
    public class UsuarioController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public UsuarioController(IMapper mapper,
            IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        [HttpPost]
        [ProducesResponseType(typeof(ResultData<TokenDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResultData<TokenDto>), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ResultData<TokenDto>>> Post(CreateTokenDto request)
        {
            var token = await _mediator.Send(_mapper.Map<CreateTokenByEmailAndSenhaCommand>(request));

            return Ok(_mapper.Map<TokenDto>(token));
        }
    }
}
