using Cinemark.Domain.Interfaces.Repositories;
using Cinemark.Domain.Models;
using Cinemark.Infrastructure.Data.Context;
using Cinemark.Infrastructure.Data.Context.Option;
using Cinemark.Infrastructure.Data.Repositories;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Moq;
using Xunit;

namespace Cinemark.Unit.Tests.Infrastructure.Data.Repositories
{
    public class FilmeRepositoryTest
    {
        [Fact]
        public async void InsertAsync()
        {
            var mongoConfiguration = new MongoConfiguration()
            {
                Connection = "mongodb://tes123 ",
                DatabaseName = "TestDB"
            };

            var mongoOptionsMock = new Mock<IOptions<MongoConfiguration>>();
            mongoOptionsMock.Setup(s => s.Value).Returns(mongoConfiguration);         
            
            var mongoContext = new MongoContext(mongoOptionsMock.Object);

            var sqlServerOptions = new DbContextOptionsBuilder<SqlServerContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var sqlServerContext = new SqlServerContext(sqlServerOptions);
            sqlServerContext.Database.EnsureCreated();

            var filmeRepository = new FilmeRepository(mongoContext, sqlServerContext);

            var filme = new Filme()
            {
                Id = 1,
                Nome = "E o Vento Levou",
                Categoria = "Drama",
                FaixaEtaria = 12,
                DataLancamento = new DateTime(1971, 10, 3)
            };
            
            var result = await filmeRepository.InsertAsync(filme);

            result.Id.Should().Be(1);
            result.Nome.Should().Be("E o Vento Levou");
            result.Categoria.Should().Be("Drama");
            result.FaixaEtaria.Should().Be(12);
            result.DataLancamento.Should().Be(new DateTime(1971, 10, 3));
        }
    }
}
