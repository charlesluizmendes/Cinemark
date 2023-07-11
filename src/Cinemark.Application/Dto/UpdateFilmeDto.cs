namespace Cinemark.Application.Dto
{
    public class UpdateFilmeDto
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Categoria { get; set; }
        public int FaixaEtaria { get; set; }
        public DateTime DataLancamento { get; set; }
    }
}
