using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BANKFE.Models
{
    public class Modal : IModal
    {
        public string Title { get; set; }
        public string Desc { get; set; }
        public string cssStyle { get => "text-info"; }
        public string callback { get; set; }

        public string GetMessage() => Desc;
    }
}
