using System;

namespace BANKFE.Models
{
    public class Registration
    {
        public int ID { get; set; }
        public string NIK { get; set; }
        public string NAME { get; set; }
        public string BIRTH_PLACE { get; set; }
        public DateTime BIRTH_DATE { get; set; }
        public string MOTHER_MAIDEN_NAME { get; set; }
        public string ADDRESS { get; set; }
        public string KELURAHAN { get; set; }
        public string KECAMATAN { get; set; }
        public string KABUPATEN_KOTA { get; set; }
        public string PROVINCE { get; set; }
        public char GENDER { get; set; }
        public string MARITAL_STATUS { get; set; }
        public string JOB { get; set; }
        public string EMAIL { get; set; }
        public string USERNAME { get; set; }
        public string PASSWORD { get; set; }
        public string PHONE { get; set; }
        public string FOTO_KTP_SELFIE { get; set; }
        public string VIDEO { get; set; }
        public string USER_TYPE { get; set; }
    }
}
