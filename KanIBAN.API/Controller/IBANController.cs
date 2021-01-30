using System.Text;
using KanIBAN.API.Config;
using KanIBAN.API.Data.Request;
using KanIBAN.API.Extensions;
using KanIBAN.API.Model;
using KanIBAN.API.Service;
using Microsoft.AspNetCore.Mvc;

namespace KanIBAN.API.Controller
{
    [ApiController]
    [Route("[controller]")]
    public class IBANController : ControllerBase
    {
        private readonly IBANFormatProvider _ibanFormatProvider;
        private readonly IBANResponseBuilder _ibanResponseBuilder;

        public IBANController(IBANFormatProvider ibanFormatProvider, IBANResponseBuilder ibanResponseBuilder)
        {
            _ibanFormatProvider = ibanFormatProvider;
            _ibanResponseBuilder = ibanResponseBuilder;
        }
        // POST: /IBAN
        /// <param name="validateIBANRequest">
        ///  example request:
        ///  {
        ///     "iban": "LT012345678901234567"
        ///  }
        /// </param>
        /// <returns>
        ///    LT012345678901234567;true;Unrecognized
        /// </returns>
        [HttpPost]
        public IActionResult ValidateSingleIBAN(ValidateIBANRequest validateIBANRequest)
        {
            IBAN iban = IBANParser.ParseIBAN(
                        validateIBANRequest.RawIBAN.IBAN,
                        _ibanFormatProvider.GetIBANFormatForCountryOrNull,
                        out var ibanFormat
                    );
            bool isIbanValid = IBANRootValidator.IsValid(iban, ibanFormat, IBANActiveValidators.ActiveValidators);
            Bank ibanBank = IBANBankResolver.ResolveIBANBank(iban, ibanFormat);
            
            string response =_ibanResponseBuilder
                .AddIBAN(iban)
                .AddIBANValidityStatus(isIbanValid)
                .AddIBANBank(ibanBank)
                .Build();
            
            return Ok(response);
        }
        // POST: IBAN/list
        /// <param name="validateIBANListRequest">
        ///  {
        ///  "ibans": [
        ///     "AA012345678901234567",
        ///     "LT017044078901234567",
        ///     ]
        ///  }
        /// </param>
        /// <returns>
        ///     AA012345678901234567;false;Unrecognized
        ///     LT017044078901234567;true;Seb
        /// </returns>
        [HttpPost("list")]
        public IActionResult ValidateIBANList(ValidateIBANListRequest validateIBANListRequest)
        {
            var sb = new StringBuilder();
            
            foreach (var rawIBAN in validateIBANListRequest.RawIBANs)
            {
                var response = ValidateSingleIBAN(new ValidateIBANRequest { RawIBAN = new RawIBAN { IBAN = rawIBAN } });
                var okResult = response as OkObjectResult;
                string responseString = okResult?.Value.ToString();
                sb.AppendLine(responseString);
            }

            return Ok(sb.RemoveLastNewLine(from: sb.Length - 2, count: 2).ToString());
        }
    }
}