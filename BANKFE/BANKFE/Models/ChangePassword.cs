﻿namespace BANKFE.Models
{
    public class ChangePassword
    {
        public ChangePassword(string token, string userName, string password, string new_Password)
        {
            Token = token;
            UserName = userName;
            Password = password;
            New_Password = new_Password;
        }

        public string Token { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string New_Password { get; set; }
    }
}
