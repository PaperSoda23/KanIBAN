using System;
using System.Linq;
using KanIBAN.API.Config;
using KanIBAN.API.Model;

namespace KanIBAN.API.Service
{
    public static class IBANBankResolver
    {
        /// <summary>
        /// finds Bank (name, code), associated with the IBAN code
        /// </summary>
        /// <param name="iban">IBAN model</param>
        /// <param name="ibanFormat"></param>
        /// <returns>Bank model</returns>
        public static Bank ResolveIBANBank(IBAN iban, IBANFormat ibanFormat)
        {
            if (iban == null || ibanFormat.Country == Country.Unknown)
                return Bank.Unrecognized;
            
            return (
                from int bankCode in Enum.GetValues(typeof(Bank))
                let bankCodeString = bankCode.ToString().PadLeft(ibanFormat.BankCodeLength, '0')
                where iban.BankCode.Equals(bankCodeString) 
                select (Bank) bankCode
            ).FirstOrDefault();
        }
    }
}