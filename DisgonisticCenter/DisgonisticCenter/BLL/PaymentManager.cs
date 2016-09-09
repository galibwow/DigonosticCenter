using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DisgonisticCenter.DAL;
using DisgonisticCenter.Model;

namespace DisgonisticCenter.BLL
{
    public class PaymentManager
    {

        TestRequestGateway testRequestGateway=new TestRequestGateway();
  public int UpdatePaymentStatus(string billNo, string mobileNo,decimal amount,decimal paidamount)
        {
            return testRequestGateway.UpdatePaymentStatus(billNo, mobileNo,amount,paidamount);
        }

        public List<SearchVIew> GetAllBillInfo(string bill)
        {
            return testRequestGateway.GetAllBillInfo(bill);
        } 
    }
}