namespace BANKFE.Models
{
    public class User
    {
        public int USER_ID { get; set; }
        public string USERNAME { get; set; }
        public string PASSWORD { get; set; }
        public string PIN { get; set; }
        public string NAME { get; set; }
        public string ADDRESS { get; set; }
        public string PHONE { get; set; }
        public string FOTO_KTP { get; set; }
        public string FOTO_SELFIE { get; set; }

        public bool IS_VALIDATE { get; set; }

        public string LOGIN_HOLD { get; set; }


        public int LOGIN_FAILED { get; set; }

        public string USER_TYPE { get; set; }
    }
}
