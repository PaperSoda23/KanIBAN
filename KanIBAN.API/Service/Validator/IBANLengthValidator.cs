using KanIBAN.API.Model;

namespace KanIBAN.API.Service.Validator
{
    public static class IBANLengthValidator
    {
        public static bool HasCorrectLengthForCountry(IBAN iban, IBANFormat ibanFormat) =>
            iban.RawIBAN.Length == ibanFormat.Length;
    }
}