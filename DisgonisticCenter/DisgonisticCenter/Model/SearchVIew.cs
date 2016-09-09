using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DisgonisticCenter.Model
{[Serializable]
    public class SearchVIew
    {

        public int PatientId { get; set; }

        public string BillNo { get; set; }

        public string MobileNo { get; set; }

        public string TestName { get; set; }

        public decimal TestFee { get; set; }

        public decimal Amount { get; set; }

    }
}