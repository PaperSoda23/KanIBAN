using System;
using KanIBAN.API.Config;
using KanIBAN.API.Model;

namespace KanIBAN.API.Service
{
    public static class IBANParser
    {
        /// <summary>
        /// Parses raw IBAN string to IBAN domain model
        /// </summary>
        /// <param name="rawIBAN">raw iban, passed by client</param>
        /// <param name="resolveIBANFormat">method to resolve iban format</param>
        /// <param name="ibanFormat"></param>
        /// <returns>IBAN model</returns>
        public static IBAN ParseIBAN(string rawIBAN, Func<Country, IBANFormat> resolveIBANFormat, out IBANFormat ibanFormat)
        {
            if (rawIBAN == null)
            {
                throw new ArgumentNullException($"argument {nameof(rawIBAN)} is null");
            }
            
            var isIBANCountryRecognized = Enum.TryParse<Country>(rawIBAN.Substring(0, 2), ignoreCase: false, out var country);
            if (!isIBANCountryRecognized)
            {
                country = Country.Unknown;
                ibanFormat = resolveIBANFormat(country);
                return new IBAN { RawIBAN = rawIBAN, Country = country, AccountNumber = string.Empty, BankCode = string.Empty, CheckDigits = string.Empty };
            }
            
            ibanFormat = resolveIBANFormat(country);

            string checkDigits = rawIBAN.Substring(2, 2);
            string bankCode;
            string accountNumber;
            try 
            {
                bankCode = rawIBAN.Substring(4, ibanFormat.BankCodeLength);
                accountNumber = rawIBAN.Substring(4 + ibanFormat.BankCodeLength, ibanFormat.AccountNumberLength);
            } 
            catch (ArgumentOutOfRangeException) 
            {
                bankCode = string.Empty;
                accountNumber = string.Empty;
            }

            return new IBAN
            {
                Country = country,
                CheckDigits = checkDigits,
                BankCode = bankCode,
                AccountNumber = accountNumber,
                RawIBAN = rawIBAN
            };
        }
    }
}