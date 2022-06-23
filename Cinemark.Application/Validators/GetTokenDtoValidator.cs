using Cinemark.Application.Dto;
using FluentValidation;

namespace Cinemark.Application.Validators
{
    public class GetTokenDtoValidator : AbstractValidator<GetTokenDto>
    {
        public GetTokenDtoValidator()
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
