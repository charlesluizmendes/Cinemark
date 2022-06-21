using Cinemark.Application.Dto;
using FluentValidation;

namespace Cinemark.Application.Validators
{
    public class UpdateFilmeDtoValidator : AbstractValidator<UpdateFilmeDto>
    {
        public UpdateFilmeDtoValidator()
        {
            RuleFor(x => x.Nome)
                .NotEmpty().NotNull()
                .WithMessage("O Nome não pode ser nulo");

            RuleFor(x => x.Categoria)
                .NotEmpty().NotNull()
                .WithMessage("A Categoria não pode ser nula");

            RuleFor(x => x.FaixaEtaria)
               .NotEmpty().NotNull()
               .WithMessage("A Faixa Etária não pode ser nula");

            RuleFor(x => x.DataLancamento)
               .NotNull().NotNull()
               .WithMessage("A Data de Lançamento não pode ser nula");
        }
    }
}
