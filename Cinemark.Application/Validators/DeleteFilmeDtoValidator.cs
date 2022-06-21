using Cinemark.Application.Dto;
using FluentValidation;

namespace Cinemark.Application.Validators
{
    public class DeleteFilmeDtoValidator : AbstractValidator<DeleteFilmeDto>
    {
        public DeleteFilmeDtoValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().NotNull()
                .WithMessage("O Id não pode ser nulo");
        }
    }
}
