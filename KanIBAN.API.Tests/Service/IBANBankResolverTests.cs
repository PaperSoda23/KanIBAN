using System;
using KanIBAN.API.Config;
using KanIBAN.API.Model;
using KanIBAN.API.Service;
using Xunit;

namespace KanIBAN.API.Tests.Service
{
    public class IBANBankResolverTests
    {
        [Fact]
        public void with_existing_bank_code_resolves_to_associated_bank()
        {
            string sebBankCode = Convert.ToString((int)Bank.Seb);
            var iban = new IBAN { BankCode = sebBankCode, Country = Country.LT};
            
            Bank bank = IBANBankResolver.ResolveIBANBank(iban, new IBANFormat { BankCodeLength = sebBankCode.Length, Country = Country.LT});
            
            Assert.Equal(Bank.Seb, bank);
        }

        [Fact]
        public void with_non_existing_code_resolves_to_unrecognized_bank()
        {
            const string nonExistingCode = "99999";
            var iban = new IBAN { BankCode = nonExistingCode, Country = Country.LT};
            
            Bank bank = IBANBankResolver.ResolveIBANBank(iban, new IBANFormat { BankCodeLength = nonExistingCode.Length, Country = Country.LT});
            
            Assert.Equal(Bank.Unrecognized, bank);
        }

        [Fact]
        public void resolves_to_unrecognized_bank_when_iban_is_null()
        {
            IBAN iban = null;

            Bank bank = IBANBankResolver.ResolveIBANBank(iban, new IBANFormat { Country = Country.LT });
            
            Assert.Equal(Bank.Unrecognized, bank);
        }

        [Fact]
        public void resolves_to_unrecognized_bank_when_country_is_unknown()
        {
            var iban = new IBAN { Country = Country.Unknown };
            var ibanFormat = new IBANFormat { Country = Country.Unknown };

            Bank bank = IBANBankResolver.ResolveIBANBank(iban, ibanFormat);
            
            Assert.Equal(Bank.Unrecognized, bank);
        }
    }
}