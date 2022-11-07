using Cinemark.Domain.Interfaces.EventBus;
using Cinemark.Domain.Models;
using Cinemark.Domain.Models.Commom;
using FluentAssertions;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using Moq;
using System.Threading;
using Xunit;

namespace Cinemark.Unit.Tests.Infrastructure.Data.EventBus
{
    public class FilmeEventBusTest
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

            var filmeCreateEventBus = new Mock<IFilmeEventBus>();
            filmeCreateEventBus.Setup(x => x.PublisherAsync("Filme_Insert", It.IsAny<Filme>()))
                .Returns(Task.CompletedTask);            

            var result = filmeCreateEventBus.Object.PublisherAsync("Filme_Insert", filme);

            result.IsCompletedSuccessfully.Should().BeTrue();
        }

        [Fact]
        public void SubscriberAsync()
        {
            var filmeCreateEventBus = new Mock<IFilmeEventBus>();            

            var result = filmeCreateEventBus.Object.SubscriberAsync("Filme_Insert", (message, cancellationToken) => 
            {
                return Task.FromResult(new ResultData<bool>(true));
            });

            result.IsCompletedSuccessfully.Should().BeTrue();
        }       
    }
}
