 namespace BANKFE.Models
{
    public class PinModel
    {
        public PinModel(string token, string userName, string pin, string new_pin)
        {
            Token = token;
            UserName = userName;
            Pin = pin;
            New_Pin = new_pin;
        }

        public string Token { get; set; }
        public string UserName { get; set; }
        public string Pin { get; set; }
        public string New_Pin { get; set; }
    }
}

