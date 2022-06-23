namespace Cinemark.Application.Dto
{
    public class CreateFilmeDto
    {
        public string? Nome { get; set; }
        public string? Categoria { get; set; }
        public int FaixaEtaria { get; set; }
        public DateTime DataLancamento { get; set; }
    }
}
