using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BANKFE.Models.ChangeData
{
    public class ChangeData
    {
        public string USERNAME { get; set; }

        public string ADDRESS { get; set; }
        public string PROVINCE { get; set; }
        public string KABUPATEN_KOTA { get; set; }
        public string KECAMATAN { get; set; }
        public string KELURAHAN { get; set; }
        public string JOB { get; set; }
    }
}
