using Cinemark.Application.Commands;
using Cinemark.Application.Dto;
using Cinemark.Application.Events;
using Cinemark.Application.Queries;
using Cinemark.Application.Validators;
using Cinemark.Domain.AggregatesModels.FilmeAggregate;
using Cinemark.Domain.AggregatesModels.UsuarioAggregate;
using Cinemark.Domain.Events;
using Cinemark.Infrastructure.Data.Context;
using Cinemark.Infrastructure.Data.Repositories;
using Cinemark.Infrastructure.EventBus;
using Cinemark.Infrastructure.Identity;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Cinemark.Infrastructure.IoC
{
    public class InjectorDependency
    {
        public static void Register(IServiceCollection container)
        {

            // Application

            container.AddTransient<IRequestHandler<CreateTokenByEmailAndSenhaCommand, Token>, CreateTokenByEmailAndSenhaCommandHandler>();
            container.AddTransient<IRequestHandler<GetFilmeQuery, IEnumerable<Filme>>, GetFilmeQueryHandler>();
            container.AddTransient<IRequestHandler<GetFilmeByIdQuery, Filme>, GetFilmeByIdQueryHandler>();
            container.AddTransient<IRequestHandler<CreateFilmeCommand, bool>, CreateFilmeCommandHandler>();
            container.AddTransient<IRequestHandler<DeleteFilmeCommand, bool>, DeleteFilmeCommandHandler>();
            container.AddTransient<IRequestHandler<UpdateFilmeCommand, bool>, UpdateFilmeCommandHandler>();

            container.AddScoped<INotificationHandler<FilmeCreatedEvent>, FilmeCreatedEventHandler>();
            container.AddScoped<INotificationHandler<FilmeRemovedEvent>, FilmeRemovedEventHandler>();
            container.AddScoped<INotificationHandler<FilmeUpdatedEvent>, FilmeUpdatedEventHandler>();

            container.AddTransient<IValidator<CreateTokenDto>, CreateTokenDtoValidator>();
            container.AddTransient<IValidator<CreateFilmeDto>, CreateFilmeDtoValidator>();
            container.AddTransient<IValidator<UpdateFilmeDto>, UpdateFilmeDtoValidator>();

            // Infrastructure

            container.AddTransient<MongoContext>();

            container.AddTransient<IFilmeRepository, FilmeRepository>();
            container.AddTransient<IUsuarioRepository, UsuarioRepository>();

            container.AddTransient<IFilmeEventBus, FilmeEventBus>();

            container.AddTransient<IUsuarioService, UsuarioService>();
        }
    }
}
