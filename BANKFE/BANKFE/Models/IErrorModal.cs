using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BANKFE.Models
{
    public interface IErrorModal : IModal
    {
        public string RequestId { get; set; }
        public bool ShowRequestId { get; }
    }
}
