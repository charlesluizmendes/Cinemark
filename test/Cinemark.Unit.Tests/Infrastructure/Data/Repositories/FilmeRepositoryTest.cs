using Cinemark.Domain.AggregatesModels.FilmeAggregate;
using FluentAssertions;
using Moq;
using Xunit;

namespace Cinemark.Unit.Tests.Infrastructure.Data.Repositories
{
    public class FilmeRepositoryTest
    {
        [Fact]
        public async void InsertAsync()
        {
            var filme = new Mock<Filme>(new Guid("30eb581a-4373-4a49-93a3-6fba8aae2044"), "E o Vento Levou", "Drama", 12, new DateTime(1971, 10, 3));
            filme.Setup(x => x.Id).Returns(new Guid("30eb581a-4373-4a49-93a3-6fba8aae2044"));

            var filmeRepository = new Mock<IFilmeRepository>();
            filmeRepository.Setup(x => x.InsertAsync(It.IsAny<Filme>()));
            
            await filmeRepository.Object.InsertAsync(filme.Object);
        }
    }
}
