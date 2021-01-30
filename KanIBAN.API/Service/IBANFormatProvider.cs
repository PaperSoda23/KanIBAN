using System;
using System.Collections.Generic;
using System.Linq;
using KanIBAN.API.Config;
using KanIBAN.API.Model;

namespace KanIBAN.API.Service
{
    public class IBANFormatProvider
    {
        private readonly Dictionary<string, IBANFormat> _formats;

        public IBANFormatProvider(IEnumerable<IBANFormat> formats)
        {
            _formats = formats.ToDictionary(e => e.Country.ToString(), e => e);
        }

        public IBANFormat GetIBANFormatForCountryOrNull(Country country)
        {
            var enumName = Enum.GetName(typeof(Country), country)!;
            return _formats.ContainsKey(enumName ?? string.Empty) ? _formats[enumName!] : null;
        }
    }
}