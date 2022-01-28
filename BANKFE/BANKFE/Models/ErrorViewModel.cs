using System;

namespace BANKFE.Models
{
    public class ErrorViewModel : IErrorModal
    {
        public string Title { get; set; }
        public string Desc { get; set; }

        public string RequestId { get; set; }
        public bool ShowRequestId { get { return !string.IsNullOrEmpty(RequestId); } }

        public string GetMessage()
        {
            return Desc + "\n" + "ERROR: " + RequestId;
        }
    }
}
