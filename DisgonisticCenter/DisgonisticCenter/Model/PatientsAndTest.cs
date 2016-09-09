using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DisgonisticCenter.Model
{
    public class PatientsAndTest
    {
        public int PaitentId { get; set; }
        public int TestId { get; set; }

        public TestSetup TestSetups { get; set; }

        public TestRequest TestRequests { get; set; }

    }
}