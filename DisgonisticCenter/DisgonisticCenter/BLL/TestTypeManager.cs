using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DisgonisticCenter.DAL;
using DisgonisticCenter.Model;

namespace DisgonisticCenter.BLL
{
    public class TestTypeManager
    {
        TestTypeGateway testTypeGateway=new TestTypeGateway();

        public bool SaveTestType(TestType testType)
        {
            bool isSave = false;
            int a=testTypeGateway.SaveTestType(testType);
            if (a > 0)
            {
                isSave = true;
            }
            return isSave;
        }

        public bool IsTestTypeExist(TestType testType)
        {
            return testTypeGateway.IsTestTypeExist(testType);
        }

        public List<TestType> GetAllTestTypes()
        {
            return testTypeGateway.GetAllTestTypes();
        } 
    }
}