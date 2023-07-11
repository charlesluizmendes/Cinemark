using Cinemark.Domain.Core.Exceptions;
using FluentAssertions;
using Moq;
using Xunit;

namespace Cinemark.Unit.Tests.Domain.Core.Exeptions
{
    public class DomainExceptionTest
    {
        [Fact]
        public void DomainException()
        {
            var domainException = new Mock<DomainException>();

            domainException.Object.Should().NotBeNull();
        }

        [Fact]
        public void DomainExceptionCompanyName()
        {
            var domainException = new Mock<DomainException>("O Nome do Filme não pode ser nulo ou vazio");

            domainException.Object.Should().NotBeNull();
        }

        [Fact]
        public void DomainExceptionCompanyNameError()
        {
            var domainException = new Mock<DomainException>("O Nome do Filme não pode ser nulo ou vazio", new Exception());

            domainException.Object.Should().NotBeNull();
        }
    }
}
