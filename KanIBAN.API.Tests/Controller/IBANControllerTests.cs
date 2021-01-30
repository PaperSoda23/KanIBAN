using System;
using System.Collections.Generic;
using KanIBAN.API.Config;
using KanIBAN.API.Controller;
using KanIBAN.API.Data.Request;
using KanIBAN.API.Model;
using KanIBAN.API.Service;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace KanIBAN.API.Tests.Controller
{
    public class IBANControllerTests
    {
        private readonly IBANController _controller;

        public IBANControllerTests()
        {
            var formats = new List<IBANFormat>
            {
                new IBANFormat { Country = Country.Unknown, AccountNumberLength = -1, BankCodeLength = -1, Length = -1},
                new IBANFormat { Country = Country.LT, AccountNumberLength = 11, BankCodeLength = 5, Length = 20 },
                new IBANFormat { Country = Country.GB, AccountNumberLength = 12, BankCodeLength = 6, Length = 22 }
            };
            var ibanFormatProvider = new IBANFormatProvider(formats);
            _controller = new IBANController(ibanFormatProvider, new IBANResponseBuilder());
        }

        [Fact]
        public void processes_single_request()
        {
            var request = new ValidateIBANRequest { RawIBAN = new RawIBAN { IBAN = "LT012345678901234567"} };
            
            var result = _controller.ValidateSingleIBAN(request);

            var okResult = Assert.IsType<OkObjectResult>(result); 
            var resultStr = Assert.IsType<string>(okResult.Value);
            Assert.Equal("LT012345678901234567;true;Unrecognized", resultStr);
        }

        [Fact]
        public void processes_multiple_requests()
        {
            var request = new ValidateIBANListRequest
            {
                RawIBANs = new List<string>
                {
                    "AA012345678901234567",
                    "LT017044078901234567",
                    "LT012132178901234567",
                    "LT012143278901234567",
                    "LT012345678901234567",
                    "LT01234567890123456712345",
                    "BB012345678901234567"
                }
            };

            var result = _controller.ValidateIBANList(request);
            
            var okResult = Assert.IsType<OkObjectResult>(result); 
            var resultStr = Assert.IsType<string>(okResult.Value);


            var expectedResults = new List<string>
            {
                "AA012345678901234567;false;Unrecognized",
                "LT017044078901234567;true;Seb",
                "LT012132178901234567;true;Swed",
                "LT012143278901234567;true;Barclays",
                "LT012345678901234567;true;Unrecognized",
                "LT01234567890123456712345;false;Unrecognized",
                "BB012345678901234567;false;Unrecognized"
            };
            
            Assert.Equal(expectedResults, resultStr.Split(Environment.NewLine));
        }
    }
}