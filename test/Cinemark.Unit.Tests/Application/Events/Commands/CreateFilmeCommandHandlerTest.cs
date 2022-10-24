using Cinemark.Application.Events.Commands;
using Cinemark.Domain.Interfaces.EventBus;
using Cinemark.Domain.Interfaces.Repositories;
using Cinemark.Domain.Models;
using FluentAssertions;
using Moq;
using Xunit;

namespace Cinemark.Unit.Tests.Application.Events.Commands
{
    public class CreateFilmeCommandHandlerTest
    {
        [Fact]
        public async void CreateFilmeCommandHandler()
        {
            var filme = new Filme()
            {
                Id = 1,
                Nome = "E o Vento Levou",
                Categoria = "Drama",
                FaixaEtaria = 12,
                DataLancamento = new DateTime(1971, 10, 3)
            };

            var createFilmeCommand = new CreateFilmeCommand()
            {
                Filme = filme
            };

            var filmeRepositoryMock = new Mock<IFilmeRepository>();
            filmeRepositoryMock.Setup(x => x.InsertAsync(It.IsAny<Filme>()))
                .ReturnsAsync(filme);

            var filmeCreateEventBus = new Mock<IFilmeCreateEventBus>();
            filmeCreateEventBus.Setup(x => x.PublisherAsync(It.IsAny<Filme>()))
                .Returns(Task.FromResult(filme));

            var createFilmeCommandHandler = new Mock<CreateFilmeCommandHandler>(filmeRepositoryMock.Object, filmeCreateEventBus.Object);
           
            var result = await createFilmeCommandHandler.Object.Handle(createFilmeCommand, new CancellationToken());

            result.Id.Should().Be(1);
            result.Nome.Should().Be("E o Vento Levou");
            result.Categoria.Should().Be("Drama");
            result.FaixaEtaria.Should().Be(12);
            result.DataLancamento.Should().Be(new DateTime(1971, 10, 3));
        }
    }
}
