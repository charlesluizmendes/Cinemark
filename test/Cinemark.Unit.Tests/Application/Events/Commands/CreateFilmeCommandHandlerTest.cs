using Cinemark.Application.Commands;
using Cinemark.Domain.AggregatesModels.FilmeAggregate;
using Cinemark.Domain.Constants;
using Cinemark.Domain.Interfaces.EventBus;
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

            var filmeRepositoryMock = new Mock<IFilmeRepository>();
            filmeRepositoryMock.Setup(x => x.InsertAsync(It.IsAny<Filme>()))
                .ReturnsAsync(filme);

            var filmeEventBusMock = new Mock<IFilmeEventBus>();
            filmeEventBusMock.Setup(x => x.PublisherAsync(typeof(Filme).Name + QueueConstants.Insert, It.IsAny<Filme>()))
                .Returns(Task.CompletedTask);

            var command = new CreateFilmeCommand() { Filme = filme };
            var handler = new Mock<CreateFilmeCommandHandler>(filmeRepositoryMock.Object, filmeEventBusMock.Object);
           
            var result = await handler.Object.Handle(command, new CancellationToken());

            result.Data.Id.Should().Be(1);
            result.Data.Nome.Should().Be("E o Vento Levou");
            result.Data.Categoria.Should().Be("Drama");
            result.Data.FaixaEtaria.Should().Be(12);
            result.Data.DataLancamento.Should().Be(new DateTime(1971, 10, 3));
        }
    }
}
