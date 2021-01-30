using KanIBAN.API.Config;
using KanIBAN.API.Model;

namespace KanIBAN.API.Service
{
    /// <summary>
    /// Builds IBAN Response to send to client
    /// </summary>
    /// <remarks>format: a;b;c</remarks>
    /// <returns>string</returns>
    public class IBANResponseBuilder
    {
        private string _response = string.Empty;

        public IBANResponseBuilder AddIBAN(in IBAN iban) 
        {
            _response = string.Concat(_response, ";", iban.RawIBAN);
            return this;
        }

        public IBANResponseBuilder AddIBANValidityStatus(bool isValid)
        {
            _response = string.Concat(_response, ";", isValid.ToString().ToLower());
            return this;
        }

        public IBANResponseBuilder AddIBANBank(Bank bank)
        {
            _response = string.Concat(_response, ";", bank.ToString());
            return this;
        }
        
        public string Build()
        {
            RemoveFirstColumn();
            string @return = _response;
            ResetResponseToEmpty();
            return @return;
        }

        private void RemoveFirstColumn() => _response = _response.Remove(0, 1);
        private void ResetResponseToEmpty() => _response = string.Empty;
    }
}