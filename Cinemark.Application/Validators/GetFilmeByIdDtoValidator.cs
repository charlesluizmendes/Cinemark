using Cinemark.Application.Dto;
using FluentValidation;

namespace Cinemark.Application.Validators
{
    public class GetFilmeByIdDtoValidator : AbstractValidator<GetFilmeByIdDto>
    {
        public GetFilmeByIdDtoValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().NotNull()
                .WithMessage("O Id não pode ser nulo");
        }
    }
}
