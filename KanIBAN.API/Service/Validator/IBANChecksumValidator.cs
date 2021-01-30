using System;
using System.Numerics;
using System.Text;
using System.Text.RegularExpressions;
using KanIBAN.API.Config;
using KanIBAN.API.Model;

namespace KanIBAN.API.Service.Validator
{
    public static class IBANChecksumValidator
    {
        /// <summary>
        /// MOD 97-10 Checksum check
        /// </summary>
        /// <param name="iban"></param>
        /// <param name="format"></param>
        /// <returns>true if checksum validation passes, else false</returns>
        /// <exception cref="OverflowException">when remainder can't be converted to integer or when remainder too large | too small</exception>
        public static bool ValidateChecksum(IBAN iban, IBANFormat format)
        {
            var countryChars = iban.Country
                .ToString()
                .ToUpper()
                .ToCharArray();

            if (iban.Country == Country.Unknown)
            {
                throw new ArgumentException("IBANChecksumValidator: unknown iban passed");
            }
            
            if (!L2DConversionTable.ConversionTable.ContainsKey(countryChars[0]) || !L2DConversionTable.ConversionTable.ContainsKey(countryChars[1]))
            {
                throw new ArgumentException("IBANChecksumValidator: no key in conversion table");
            }

            string reorderedIBAN = GetComputedIBANString(
                subIban: iban.RawIBAN.Substring(startIndex: 4),
                countryCode: L2DConversionTable.ConversionTable[countryChars[0]] + L2DConversionTable.ConversionTable[countryChars[1]],
                checkSum: iban.CheckDigits
            );

            string digitOnlyIBAN = ReplaceAlphaToDigit(reorderedIBAN);
            
            var newComputedChecksum = ComputeNewChecksum(digitOnlyIBAN);

            return newComputedChecksum.Equals(iban.CheckDigits);
        }

        private static string GetComputedIBANString(string subIban, string countryCode, string checkSum) =>
            $"{subIban}{countryCode}{checkSum}";

        private static string ReplaceAlphaToDigit(string text)
        {
            var result = new StringBuilder(text);
            var chars = text.ToCharArray();
            
            foreach (var @char in chars)
            {
                if (char.IsLetter(@char))
                {
                    result.Replace(@char.ToString(), L2DConversionTable.ConversionTable[@char]);
                }
            }

            return result.ToString();
        }
        
        private static string ComputeNewChecksum(string reorderedDigitalIBAN)
        {
            string noLeadingZeroesIBAN = Regex.Replace(reorderedDigitalIBAN, "^0+", string.Empty);
            
            BigInteger computedIBANNumber = BigInteger.Parse(noLeadingZeroesIBAN);
            
            BigInteger.DivRem(computedIBANNumber, 97, out var remainder);

            BigInteger computedChecksum = BigInteger.Subtract(98, remainder);

            return computedChecksum.CompareTo(10) == -1 ? 
                $"0{computedChecksum}" :
                computedChecksum.ToString();
        }
    }
}