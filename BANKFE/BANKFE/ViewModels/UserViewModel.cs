using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BANKFE.ViewModels
{
    public class UserViewModel
    {
        public string Username { get; set; }
        public string PinStatus { get; set; }
        public string IsValidate { get; set; }
        public string IsActive { get; set; }
    }
}
