namespace BANKFE.Models
{
    public class PinModel
    {
        public PinModel(string username)
        {
            UserName = username;
        }

        public PinModel(string username, string pin)
        {
            UserName = username;
            Pin = pin;
        }

        public string UserName { get; set; }
        public string Pin { get; set; }
    }
}

