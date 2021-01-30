using System.Collections.Generic;
using KanIBAN.API.Config;
using KanIBAN.API.Model;
using KanIBAN.API.Service;
using Xunit;

namespace KanIBAN.API.Tests.Service
{
    public class IBANParserTests
    {
        [Fact]
        public void parses_lithuanian_code()
        {
            const string rawIBAN = "LT012345678901234567";
            var lithuanianFormat = new IBANFormat { Country = Country.LT, AccountNumberLength = 11, BankCodeLength = 5 }; 
            var ibanFormatProvider = new IBANFormatProvider(new List<IBANFormat> { lithuanianFormat });
            
            IBAN iban = IBANParser.ParseIBAN(rawIBAN, ibanFormatProvider.GetIBANFormatForCountryOrNull, out _);

            Assert.Equal(Country.LT, iban.Country);
            Assert.Equal("01", iban.CheckDigits);
            Assert.Equal("23456", iban.BankCode);
            Assert.Equal("78901234567", iban.AccountNumber);
        }

        [Fact]
        public void tricky_bank_code_test()
        {
            Assert.NotEqual("011234", 011234.ToString());
            
            const int codeLength = 6;
            Assert.Equal("011234", 011234.ToString().PadLeft(codeLength, '0'));
        }
    }
}