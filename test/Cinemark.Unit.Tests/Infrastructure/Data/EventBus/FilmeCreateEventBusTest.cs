using Cinemark.Domain.Interfaces.EventBus;
using Cinemark.Domain.Models;
using FluentAssertions;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using Moq;
using System.Threading;
using Xunit;

namespace Cinemark.Unit.Tests.Infrastructure.Data.EventBus
{
    public class FilmeCreateEventBusTest
    {
        [Fact]
        public void PublisherAsync()
        {
            var filme = new Filme()
            {
                Id = 1,
                Nome = "E o Vento Levou",
                Categoria = "Drama",
                FaixaEtaria = 12,
                DataLancamento = new DateTime(1971, 10, 3)
            };

            var filmeCreateEventBus = new Mock<IFilmeCreateEventBus>();
            filmeCreateEventBus.Setup(x => x.PublisherAsync(It.IsAny<Filme>()))
                .Returns(Task.CompletedTask);            

            var result = filmeCreateEventBus.Object.PublisherAsync(filme);

            result.IsCompletedSuccessfully.Should().BeTrue();
        }

        [Fact]
        public void SubscriberAsync()
        {
            var filmeCreateEventBus = new Mock<IFilmeCreateEventBus>();            

            var result = filmeCreateEventBus.Object.SubscriberAsync((message, cancellationToken) => 
            {
                return Task.FromResult(true);
            });

            result.IsCompletedSuccessfully.Should().BeTrue();
        }       
    }
}
