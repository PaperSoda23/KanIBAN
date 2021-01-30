using FluentValidation;

namespace KanIBAN.API.Data.Request.Validators
{
    public class ValidateIBANRequestValidator : AbstractValidator<ValidateIBANRequest>
    {
        public ValidateIBANRequestValidator()
        {
            RuleFor(x => x.RawIBAN.IBAN)
                .SetValidator(new IBANValidator());
        }
    }
}