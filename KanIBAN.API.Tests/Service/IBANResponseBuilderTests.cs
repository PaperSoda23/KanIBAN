using KanIBAN.API.Config;
using KanIBAN.API.Model;
using KanIBAN.API.Service;
using Xunit;

namespace KanIBAN.API.Tests.Service
{
    public class IBANResponseBuilderTests
    {
        private readonly IBANResponseBuilder _ibanResponseBuilder;

        public IBANResponseBuilderTests()
        {
            _ibanResponseBuilder = new IBANResponseBuilder();
        }
        
        [Fact]
        public void builds_expected_response()
        {
            string response = _ibanResponseBuilder
                .AddIBAN(new IBAN { RawIBAN = "LT1234"})
                .AddIBANValidityStatus(true)
                .AddIBANBank(Bank.Seb)
                .Build();
            
            Assert.Equal(response, $"LT1234;true;Seb");
        }
    }
}