using KanIBAN.API.Config;

namespace KanIBAN.API.Model
{
    public class IBANFormat
    {
        public Country Country { get; set; }
        public int BankCodeLength { get; set; }   
        public int AccountNumberLength { get; set; }
        public int Length { get; set; }
    }
}