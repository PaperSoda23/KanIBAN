using System.Collections.Generic;

namespace KanIBAN.API.Config
{
    /// <summary>
    /// Convert letters to appropriate numbers for IBAN checksum validation
    /// </summary>
    public static class L2DConversionTable
    {
        public static readonly Dictionary<char, string> ConversionTable = new Dictionary<char, string>
        {
            {'B', "11"},
            {'L', "21"},
            {'T', "29"},
            {'A', "10"},
            {'R', "27"},
            {'C', "12"},
            {'U', "30"},
            {'K', "20"},
            {'G', "16"},
        };
    }
}