using Cinemark.Domain.Core.Commom;
using Cinemark.Domain.Core.Exceptions;

namespace Cinemark.Domain.AggregatesModels.FilmeAggregate
{
    public class Filme : 
        Entity, IAggregateRoot
    {
        public string Nome { get; private set; }
        public string Categoria { get; private set; }
        public int FaixaEtaria { get; private set; }
        public DateTime DataLancamento { get; private set; }

        protected Filme() { }
        
        public Filme(Guid id, string nome, string categoria, int faixaEtaria, DateTime dataLancamento)
        {
            Id = id != Guid.Empty ? id : throw new DomainException(nameof(id));
            Nome = !string.IsNullOrWhiteSpace(nome) ? nome : throw new DomainException(nameof(nome));
            Categoria = !string.IsNullOrWhiteSpace(categoria) ? categoria : throw new DomainException(nameof(categoria));
            FaixaEtaria = faixaEtaria > 0 ? faixaEtaria : throw new DomainException(nameof(faixaEtaria));
            DataLancamento = dataLancamento;
        }        

        public void Update(Guid id, string nome, string categoria, int faixaEtaria, DateTime dataLancamento)
        {
            Id = id != Guid.Empty ? id : throw new DomainException(nameof(id));
            Nome = !string.IsNullOrWhiteSpace(nome) ? nome : throw new DomainException(nameof(nome));
            Categoria = !string.IsNullOrWhiteSpace(categoria) ? categoria : throw new DomainException(nameof(categoria));
            FaixaEtaria = faixaEtaria > 0 ? faixaEtaria : throw new DomainException(nameof(faixaEtaria));
            DataLancamento = dataLancamento;
        }

        public void Delete(Guid id)
        {
            Id = id != Guid.Empty ? id : throw new DomainException(nameof(id));
        }
    }
}
