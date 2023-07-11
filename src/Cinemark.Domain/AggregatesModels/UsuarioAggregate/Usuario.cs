using Cinemark.Domain.Core.Commom;
using Cinemark.Domain.Core.Exceptions;

namespace Cinemark.Domain.AggregatesModels.UsuarioAggregate
{
    public class Usuario : 
        Entity, IAggregateRoot
    {
        public string Nome { get; private set; }
        public string Email { get; private set; }
        public string Senha { get; private set; }
        public DateTime DataCriacao { get; private set; }

        public Usuario(string nome, string email, string senha, DateTime dataCriacao)
        {
            Nome = !string.IsNullOrWhiteSpace(nome) ? nome : throw new DomainException(nameof(nome));
            Email = !string.IsNullOrWhiteSpace(email) ? email : throw new DomainException(nameof(email));
            Senha = !string.IsNullOrWhiteSpace(senha) ? senha : throw new DomainException(nameof(senha));
            DataCriacao = dataCriacao;
        }
    }
}
