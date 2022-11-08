using Cinemark.Application.Dto;
using Cinemark.Application.Events.Commands;
using Cinemark.Application.Events.Queries;
using Cinemark.Application.Validators;
using Cinemark.Domain.Interfaces.EventBus;
using Cinemark.Domain.Interfaces.Repositories;
using Cinemark.Domain.Interfaces.Services;
using Cinemark.Domain.Models;
using Cinemark.Domain.Models.Commom;
using Cinemark.Infrastructure.Data.Context;
using Cinemark.Infrastructure.Data.EventBus;
using Cinemark.Infrastructure.Data.Repositories;
using Cinemark.Infrastructure.Data.Services;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Cinemark.Infrastructure.IoC
{
    public class InjectorDependency
    {
        public static void Register(IServiceCollection container)
        {

            // Application

            container.AddTransient<IRequestHandler<GetFilmeQuery, ResultData<IEnumerable<Filme>>>, GetFilmeQueryHandler>();
            container.AddTransient<IRequestHandler<GetFilmeByIdQuery, ResultData<Filme>>, GetFilmeByIdQueryHandler>();
            container.AddTransient<IRequestHandler<GetTokenByUsuarioQuery, ResultData<Token>>, GetTokenByUsuarioQueryHandler>();
            container.AddTransient<IRequestHandler<CreateFilmeCommand, ResultData<Filme>>, CreateFilmeCommandHandler>();
            container.AddTransient<IRequestHandler<DeleteFilmeCommand, ResultData<Filme>>, DeleteFilmeCommandHandler>();
            container.AddTransient<IRequestHandler<UpdateFilmeCommand, ResultData<Filme>>, UpdateFilmeCommandHandler>();

            container.AddTransient<IValidator<CreateFilmeDto>, CreateFilmeDtoValidator>();
            container.AddTransient<IValidator<UpdateFilmeDto>, UpdateFilmeDtoValidator>();

            // Infrastructure

            container.AddTransient<MongoContext>();

            container.AddTransient<IFilmeRepository, FilmeRepository>();
            container.AddTransient<IUsuarioRepository, UsuarioRepository>();

            container.AddTransient<IFilmeEventBus, FilmeEventBus>();

            container.AddTransient<ITokenService, TokenService>();
        }
    }
}
