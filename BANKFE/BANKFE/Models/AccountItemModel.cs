using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BANKFE.Models
{
    public class AccountItemModel
    {
        public string ControllerName { get; set; }
        public string ActionName { get; set; }
        public string Desc { get; set; }

        public AccountItemModel()
        {

        }

        public AccountItemModel(string controllerName, string actionName)
        {
            ControllerName = controllerName;
            ActionName = actionName;
        }
    }
}
