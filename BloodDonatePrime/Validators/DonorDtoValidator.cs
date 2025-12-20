
using BloodBankAPI.DTOs;
using FluentValidation;

namespace BloodBankAPI.Validators
{
    public class DonorDtoValidator : AbstractValidator<DonorDto>
    {
        public DonorDtoValidator()
        {
            RuleFor(x => x.BloodGroup)
                .Matches(@"^(A|B|AB|O)[+-]$")
                .When(x => !string.IsNullOrEmpty(x.BloodGroup))
               .WithMessage("Boold Group is invalid");
            RuleFor(x => x.Age).InclusiveBetween(18, 65).WithMessage("Age must be between 18 and 65");
            RuleFor(x => x.Address).NotEmpty().When(x => !string.IsNullOrEmpty(x.Address));
        }
    }
}
