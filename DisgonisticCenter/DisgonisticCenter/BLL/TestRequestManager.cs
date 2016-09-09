using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using DisgonisticCenter.DAL;
using DisgonisticCenter.Model;

namespace DisgonisticCenter.BLL
{
    public class TestRequestManager
    {
        TestRequestGateway testRequestGateway = new TestRequestGateway();
        TestSetupGateway testSetupGateway = new TestSetupGateway();


        public int Save(TestRequest testRequest)
        {
            if (testRequestGateway.IsMobileNoExist(testRequest.MobileNo))
            {
                throw new Exception("Mobile no already exist.");
            }

            return testRequestGateway.SaveCustomerReq(testRequest);
        }
       


        public TestSetup GetFee(int testId)
        {
            return testRequestGateway.GetFee(testId);
        }

        public TestRequest GetallTestRequestByBillNo(string bill)
        {
            return testRequestGateway.GetallTestSetupByBillNo(bill);
        }


        public List<string> Getbillnobysearch(string bill)
        {
            return testRequestGateway.GetallTestRequestsBysearch(bill);

        } 
   


        public TestRequest GetPatientByMobileNo(string Mobile)
        {
            TestRequest testRequest = new TestRequest();
            testRequest= testRequestGateway.GetPatientMobileNumber(Mobile);
            return testRequest;
        }

        public int SavePatientTests(int patientId, int testId, string requestDate)
        {
            if (testRequestGateway.IsPatientTestExists(patientId, testId))
            {
                throw new Exception("Added Duplicate Tests, please select single test.");
            }
            else
            {
                return testRequestGateway.SavePatientTests(patientId, testId, requestDate);
            }

        }

        public List<TestRequest> GetAllTestRequests()
        {
            return testRequestGateway.GetAllTestRequestEntries();
        }

        public List<TestRequest> GeTestRequestsByDate(string startDate, string endDate)
        {
            return testRequestGateway.GetallTestRequestsByDate(startDate, endDate);
        }


    }
}