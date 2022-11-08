namespace Cinemark.Domain.Models
{
    public class Filme
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Categoria { get; set; }
        public int FaixaEtaria { get; set; }
        public DateTime DataLancamento { get; set; }
    }
}
