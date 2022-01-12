using System;
using System.Linq;
using System.Text;

namespace BANKFE.Models
{
    public class ChangePassword
    {
        public ChangePassword(string token, string userName, string password, string new_Password, string mode, string reff)
        {
            Token = token;
            UserName = userName;
            Password = password;
            New_Password = new_Password;
            Mode = mode;
            Reff = reff;
        }

        public string Token { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string New_Password { get; set; }
        public string Mode { get; set; }
        public string Reff { get; set; }

        private string _token { get; set; }
        private double _refId { get; set; }

        public bool IsTokenValid()
        {
            var sha1 = System.Security.Cryptography.SHA1.Create();
            var hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(UserName));
            /// RefID terbentuk dari tahun, bulan, tanggal, jam dan menit
            /// Convert RefID ke jadi int

            _token = string.Concat(hash.Select(b => b.ToString("x2")));
            double res = double.Parse(Reff) - double.Parse(DateTime.Now.ToString("yyyyMMddHHmm"));
            if (_token == Token && res >= 0)
            {
                return true;
            }

            return false;
        }
    }
}
