using System;
using System.Collections.Generic;
using System.Linq;
using KanIBAN.API.Model;

namespace KanIBAN.API.Service
{
    /// <summary>
    /// Validates IBAN to match IBAN format criteria
    /// </summary>
    public static class IBANRootValidator
    {
        /// <summary>
        ///   Checks if all assigned IBAN validations pass
        /// </summary>
        /// <remarks>LINQ.All() short-circuits on first non-match</remarks>
        /// <remarks>validator order matters</remarks>
        /// <param name="iban">native IBAN model</param>
        /// <param name="format">country-dependent IBAN format information</param>
        /// <param name="validators">list of validator methods, to check IBAN model correctness</param>
        /// <returns>true if all assigned validators pass, else false</returns>
        public static bool IsValid(IBAN iban, IBANFormat format, IEnumerable<Func<IBAN, IBANFormat, bool>> validators)
        {
            return validators.All(validator => validator.Invoke(iban, format));
        }
    }
}