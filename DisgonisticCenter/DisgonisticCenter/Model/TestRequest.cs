using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DisgonisticCenter.Model
{
    [Serializable]
    public class TestRequest
    {
   
        public int Id { get; set; }

        public string NameOfThePatient { get; set; }

        public DateTime Dob { get; set; }
        public string MobileNo { get; set; }

        public int TestId { get; set; }

        public string BillNo { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime DueDate { get; set; }
        public decimal PaymentStatus { get; set; }

        public decimal PaidAmount { get; set; }
        
    }
}