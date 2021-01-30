using FluentValidation;

namespace KanIBAN.API.Data.Request.Validators
{
    public class ValidateIBANListRequestValidator : AbstractValidator<ValidateIBANListRequest>
    {
        public ValidateIBANListRequestValidator()
        {
            RuleFor(x => x.RawIBANs).NotEmpty();
            
            RuleForEach(x => x.RawIBANs)
                .SetValidator(new IBANValidator());
        }
    }
}