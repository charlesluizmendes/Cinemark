using Cinemark.Domain.Commom;
using Cinemark.Domain.Exceptions;

namespace Cinemark.Domain.AggregatesModels.UsuarioAggregate
{
    public class Token : 
        Entity
    {
        public string AccessKey { get; private set; }
        public string ValidTo { get; private set; }

        public Token(string accessKey, string validTo)
        {
            AccessKey = !string.IsNullOrWhiteSpace(accessKey) ? accessKey : throw new DomainException(nameof(accessKey));
            ValidTo = !string.IsNullOrWhiteSpace(validTo) ? validTo : throw new DomainException(nameof(validTo));
        }
    }
}
