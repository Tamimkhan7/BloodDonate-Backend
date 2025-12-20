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
                .WithMessage("Blood Group is invalid");

            RuleFor(x => x.Age)
                .InclusiveBetween(18, 65)
                .When(x => x.Age.HasValue)
                .WithMessage("Age must be between 18 and 65");

            // Address validation
            RuleFor(x => x.PresentAddress).NotEmpty().When(x => !string.IsNullOrEmpty(x.PresentAddress));
            RuleFor(x => x.PermanentAddress).NotEmpty().When(x => !string.IsNullOrEmpty(x.PermanentAddress));
        }
    }
}
