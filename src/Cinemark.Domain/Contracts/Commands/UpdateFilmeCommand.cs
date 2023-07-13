using MediatR;

namespace Cinemark.Domain.Contracts.Commands
{
    public class UpdateFilmeCommand : IRequest<bool>
    {
        public Guid Id { get; private set; }
        public string Nome { get; private set; }
        public string Categoria { get; private set; }
        public int FaixaEtaria { get; private set; }
        public DateTime DataLancamento { get; private set; }

        public UpdateFilmeCommand(Guid id, string nome, string categoria, int faixaEtaria, DateTime dataLancamento)
        {
            Id = id;
            Nome = nome;
            Categoria = categoria;
            FaixaEtaria = faixaEtaria;
            DataLancamento = dataLancamento;
        }
    }
}
