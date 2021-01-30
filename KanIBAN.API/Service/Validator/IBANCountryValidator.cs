using KanIBAN.API.Config;
using KanIBAN.API.Model;

namespace KanIBAN.API.Service.Validator
{
    public static class IBANCountryValidator
    {
        public static bool IsCountryRecognized(IBAN iban, IBANFormat format) => iban.Country != Country.Unknown;
    }
}