using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DisgonisticCenter.Model
{
    public class TestSetup
    {

        public int Id { get; set; }
        
        public string TestName { get; set; }

        public double Fee { get; set; }
        public int TestTypeId{ get; set; }
    }
}