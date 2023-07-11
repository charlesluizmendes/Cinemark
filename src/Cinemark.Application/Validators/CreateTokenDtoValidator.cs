using Cinemark.Application.Dto;
using FluentValidation;

namespace Cinemark.Application.Validators
{
    public class CreateTokenDtoValidator : AbstractValidator<CreateTokenDto>
    {
        public CreateTokenDtoValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().NotNull()
                .WithMessage("O Email não pode ser nulo");

            RuleFor(x => x.Senha)
                .NotEmpty().NotNull()
                .WithMessage("A Senha não pode ser nula");
        }
    }
}
