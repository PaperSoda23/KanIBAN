using KanIBAN.API.Config;

namespace KanIBAN.API.Model
{
    public class IBAN
    {
        public Country Country { get; set; }
        public string CheckDigits { get; set; }
        public string BankCode { get; set; }
        public string AccountNumber { get; set; }
        public string RawIBAN { get; set; }
    }
}