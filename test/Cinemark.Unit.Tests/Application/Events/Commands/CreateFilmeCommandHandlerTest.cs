using Cinemark.Application.Commands;
using Cinemark.Domain.AggregatesModels.FilmeAggregate;
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
            var filme = new Mock<Filme>(new Guid("30eb581a-4373-4a49-93a3-6fba8aae2044"), "E o Vento Levou", "Drama", 12, new DateTime(1971, 10, 3));
            filme.Setup(x => x.Id).Returns(new Guid("30eb581a-4373-4a49-93a3-6fba8aae2044"));
            var cancellationToken = true;

            var filmeRepository = new Mock<IFilmeRepository>();
            filmeRepository.Setup(x => x.InsertAsync(It.IsAny<Filme>()));
            filmeRepository.Setup(x => x.UnitOfWork.SaveEntitiesAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(cancellationToken);            

            var command = new Mock<CreateFilmeCommand>(filme.Object.Nome, filme.Object.Categoria, filme.Object.FaixaEtaria, filme.Object.DataLancamento);
            var handler = new Mock<CreateFilmeCommandHandler>(filmeRepository.Object);
           
            var result = await handler.Object.Handle(command.Object, new CancellationToken());

            result.Should().Be(true);            
        }
    }
}
