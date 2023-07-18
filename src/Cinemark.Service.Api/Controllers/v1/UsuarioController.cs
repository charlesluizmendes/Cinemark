using AutoMapper;
using Cinemark.Application.Dto;
using Cinemark.Domain.Contracts.Commands;
using Cinemark.Domain.Core.Commom;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Cinemark.Service.Api.Controllers.v1
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1")]
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

        [HttpPost("CreateTokenByEmailAndSenhaAsync")]
        [ProducesResponseType(typeof(ResultData<TokenDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResultData<TokenDto>), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ResultData<TokenDto>>> CreateTokenByEmailAndSenhaAsync(CreateTokenDto request)
        {
            var token = await _mediator.Send(_mapper.Map<CreateTokenByEmailAndSenhaCommand>(request));

            return CustomResponse(_mapper.Map<TokenDto>(token));
        }
    }
}
