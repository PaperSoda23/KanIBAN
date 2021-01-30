using System;
using System.Collections.Generic;
using KanIBAN.API.Model;
using KanIBAN.API.Service.Validator;

namespace KanIBAN.API.Config
{
    /// <summary>
    /// Contains list of registered validator functions to validate IBAN against
    /// </summary>
    public static class IBANActiveValidators
    {
        /// <summary>
        /// List of validation functions to validate IBAN
        /// </summary>
        /// <remarks>Validation method order matters</remarks>
        /// <remarks>Short circuit is active</remarks>
        public static readonly IEnumerable<Func<IBAN, IBANFormat, bool>> ActiveValidators = new List<Func<IBAN, IBANFormat, bool>>
        {
            IBANCountryValidator.IsCountryRecognized,
            // IBANChecksumValidator.ValidateChecksum,
            IBANLengthValidator.HasCorrectLengthForCountry
        };
    }
}