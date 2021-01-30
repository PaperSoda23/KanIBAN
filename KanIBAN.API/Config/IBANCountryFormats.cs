using System.Collections.Generic;
using KanIBAN.API.Model;

namespace KanIBAN.API.Config
{
    public static class IBANCountryFormats
    {
        public static IEnumerable<IBANFormat> IBANFormats { get; } = new List<IBANFormat>
        {
            new IBANFormat { Country = Country.Unknown, AccountNumberLength = -1, BankCodeLength = -1, Length = -1},
            new IBANFormat { Country = Country.LT, AccountNumberLength = 11, BankCodeLength = 5, Length = 20 },
            new IBANFormat { Country = Country.GB, AccountNumberLength = 8, BankCodeLength = 10, Length = 22 }
        };
    }
}