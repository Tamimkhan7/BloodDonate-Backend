using BloodBankAPI.DTOs;
using FluentValidation;

//using FluentValidation;FluentValidation হলো popular validation library .NET এ। এটি সাহায্য করে clean এবং readable rules define করতে, যেমন “not empty”, “minimum length”, “regex match” ইত্যাদি।
namespace BloodBankAPI.Validators
{
    public class RegisterDtoValidator : AbstractValidator<RegisterDto>
    {
        public RegisterDtoValidator()
        {
            RuleFor(x => x.FullName).NotEmpty().MinimumLength(3);
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.Password).NotEmpty().MinimumLength(6);
            RuleFor(x => x.Phone)
                .Matches(@"^\+?\d{7,15}$")
                .When(x => !string.IsNullOrEmpty(x.Phone))
                .WithMessage("Phone number invalid");
        }
    }
}
