using MediatR;

namespace Cinemark.Application.Commands
{
    public class CreateFilmeCommand : IRequest<bool>
    {
        public string Nome { get; private set; }
        public string Categoria { get; private set; }
        public int FaixaEtaria { get; private set; }
        public DateTime DataLancamento { get; private set; }

        public CreateFilmeCommand(string nome, string categoria, int faixaEtaria, DateTime dataLancamento)
        {
            Nome = nome;
            Categoria = categoria;
            FaixaEtaria = faixaEtaria;
            DataLancamento = dataLancamento;
        }
    }
}
