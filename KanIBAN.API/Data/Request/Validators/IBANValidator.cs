using FluentValidation;

namespace KanIBAN.API.Data.Request.Validators
{
    public class IBANValidator : AbstractValidator<string>
    {
        public IBANValidator()
        {
            RuleFor(iban => iban)
                .NotEmpty().WithMessage("IBAN can not be empty")
                .MinimumLength(15).WithMessage("min length for IBAN is 15")
                .MaximumLength(34).WithMessage("max length for IBAN is 34");
        }
    }
}