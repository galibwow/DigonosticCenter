using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DisgonisticCenter.DAL;
using DisgonisticCenter.Model;

namespace DisgonisticCenter.BLL
{
    public class TestSetupManager
    {
        TestSetupGateway testSetupGateway=new TestSetupGateway();
        public bool SaveTestType(TestSetup testSetup)
        {
            bool isSave = false;
            int a = testSetupGateway.SaveTestName(testSetup);
            if (a > 0)
            {
                isSave = true;
            }
            return isSave;
        }

        public bool IsTestNameExist(TestSetup testSetup)
        {
            return testSetupGateway.IsTestNameExist(testSetup);
        }

        public List<TestSetupVM> GetAllTestNames()
        {
            return testSetupGateway.GetAllTestSetupwithType();
        }
        public List<TestSetup> GetAllTestSetups()
        {
            return testSetupGateway.GetAllTestSetup();
        }

        public TestSetup GetAllTestById(int Id)
        {
            return testSetupGateway.GetAllTestSetupByTestReqId(Id);
            ;
        }
        public PatientsAndTest GetAllpaitentBytestreqId(int Id)
        {
            
            return testSetupGateway.GetAllpaitentBytestreqId(Id);
            
        }

     


        internal List<TestSetup> GetlistoftestbytestId(int p)
        {
            return testSetupGateway.GetlistofTests(p);
        }
    }
}