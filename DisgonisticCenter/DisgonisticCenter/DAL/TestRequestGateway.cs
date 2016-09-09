using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using DisgonisticCenter.Model;
using DisgonisticCenter.UI;

namespace DisgonisticCenter.DAL
{
    public class TestRequestGateway
    {
        private string connectionString = WebConfigurationManager.ConnectionStrings["DiagonosticDbConnectionString"].ConnectionString;


        public int SaveCustomerReq(TestRequest testRequest)
        {
            SqlConnection connection=new SqlConnection(connectionString);
            string quary = "Insert Into TestRequestEntry(NameOfPatient,MobileNo,DOB,TotalAmount,DueDate,PaymentStatus)Values('" + testRequest.NameOfThePatient + "','" + testRequest.MobileNo + "','" + testRequest.Dob + "','" + testRequest.TotalAmount + "','" + testRequest.DueDate + "','"+testRequest.PaymentStatus+"') ";

            SqlCommand command = new SqlCommand(quary, connection);
            connection.Open();
            int save = command.ExecuteNonQuery();
            connection.Close();
            return save;
        }


        public TestRequest GetPatientMobileNumber(string Mobile)
        {
            
            SqlConnection connection=new SqlConnection(connectionString);
            string quary = "Select * from TestRequestEntry where MobileNo='" + Mobile + "'";
            SqlCommand command=new SqlCommand(quary,connection);
            connection.Open();

            TestRequest request=new TestRequest();
            SqlDataReader reader=command.ExecuteReader();
            while (reader.Read())
            {
                TestRequest testRequest=new TestRequest();
                testRequest.Id = Convert.ToInt32(reader["Id"].ToString());
                testRequest.NameOfThePatient = reader["NameOfPatient"].ToString();
                testRequest.Dob = Convert.ToDateTime(reader["DOB"]);
                testRequest.BillNo = reader["BillNo"].ToString();
                testRequest.MobileNo = reader["MobileNo"].ToString();
                testRequest.TotalAmount = Convert.ToDecimal(reader["TotalAmount"].ToString());
                testRequest.DueDate = Convert.ToDateTime(reader["DueDate"].ToString());
                request = testRequest;
            }
            connection.Close();
            reader.Close();
            return request;
        }

        public List<SearchVIew> GetAllBillInfo(string bill)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            string query = "SELECT * FROM SearchView WHERE BillNo = '" + bill+ "' OR MobileNo = '" + bill + "'";
            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();

            SqlDataReader reader = command.ExecuteReader();
            List<SearchVIew> searches = new List<SearchVIew>();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    SearchVIew search = new SearchVIew();
                    search.PatientId = Convert.ToInt32(reader["Id"].ToString());
                    search.BillNo = reader["BillNo"].ToString();
                    search.MobileNo = reader["MobileNo"].ToString();
                    search.TestName = reader["TestName"].ToString();
                    search.TestFee = Convert.ToDecimal(reader["Fee"].ToString());
                    search.Amount = Convert.ToDecimal(reader["PaymentStatus"].ToString());
                    searches.Add(search);
                }
                reader.Close();
            }
            connection.Close();
            return searches;
        } 
        public TestSetup GetFee(int testId)
        {
            SqlConnection connection=new SqlConnection(connectionString);
            string quary="select * From TestSetup Where Id='"+testId+"'";
            SqlCommand command=new SqlCommand(quary,connection);
            connection.Open();
            TestSetup Set=new TestSetup();
            SqlDataReader reader=command.ExecuteReader();
            while (reader.Read())
            {
               TestSetup setup=new TestSetup();
                setup.Id = Convert.ToInt32(reader["Id"].ToString());
                setup.TestName = reader["TestName"].ToString();
                setup.Fee = Convert.ToDouble(reader["Fee"].ToString());
                setup.TestTypeId = Convert.ToInt32(reader["TestTypeId"].ToString());
                Set = setup;
            }
            reader.Close();
            connection.Close();
            return Set;
        }

        public bool IsMobileNoExist(string mobileNo)
        {
            SqlConnection connection=new SqlConnection(connectionString);
            string query = "SELECT * FROM TestRequestEntry WHERE MobileNo='" + mobileNo + "'";
            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            bool IsMobileNoExist = false;
            if (reader.HasRows)
            {
                IsMobileNoExist = true;
            }
            reader.Close();
            connection.Close();
            return IsMobileNoExist;
        }





        public bool IsPatientTestExists(int patientId, int testId)
        { SqlConnection connection=new SqlConnection(connectionString);
        string query = "SELECT * FROM PatientTest WHERE TestReqid=" + patientId + " AND TestId=" + testId;
            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            bool isPatientTestExist = false;
            if (reader.HasRows)
            {
                isPatientTestExist = true;
            }
            reader.Close();
            connection.Close();
            return isPatientTestExist;
        }
        public int SavePatientTests(int patientId, int testId, string requestDate)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            string query = "INSERT INTO PatientTest(TestReqid,TestId,RequestDate) VALUES(" + patientId + "," + testId + ",'" + requestDate + "')";
           SqlCommand command = new SqlCommand(query, connection);
            connection.Open();
            int rowAffected = command.ExecuteNonQuery();
            connection.Close();
            return rowAffected;
        }

          public TestRequest GetallTestSetupByBillNo(string BillNo)
        {
            SqlConnection connection=new SqlConnection(connectionString);
              string quary = " SELECT * FROM TestRequestEntry WHERE BillNo='" + BillNo + "' OR MobileNo='" + BillNo + "'";
              SqlCommand command = new SqlCommand(quary, connection);
              connection.Open();
              TestRequest Set = new TestRequest();
              SqlDataReader reader = command.ExecuteReader();
              while (reader.Read())
              {
                 TestRequest testRequest = new TestRequest();
                  testRequest.Id = Convert.ToInt32(reader["Id"].ToString());
                  testRequest.NameOfThePatient = reader["NameOfPatient"].ToString();
                  testRequest.Dob = Convert.ToDateTime(reader["DOB"]);
                  testRequest.BillNo = reader["BillNo"].ToString();
                  testRequest.MobileNo = reader["MobileNo"].ToString();
                  testRequest.TotalAmount = Convert.ToDecimal(reader["TotalAmount"].ToString());
                  testRequest.DueDate = Convert.ToDateTime(reader["DueDate"].ToString());
                  testRequest.PaymentStatus = Convert.ToDecimal(reader["PaymentStatus"].ToString());
                  testRequest.PaidAmount = Convert.ToDecimal(reader["PaidAmount"].ToString());
                  
                  
                  Set = testRequest;
              }
              reader.Close();
              connection.Close();
              return Set;
        }

          public int UpdatePaymentStatus(string billNo, string mobileNo,decimal amount,decimal paidAmount)
          { SqlConnection connection=new SqlConnection(connectionString);
          string query = "UPDATE TestRequestEntry Set PaymentStatus='" + amount + "', PaidAmount='"+paidAmount+"' Where BillNo='" + billNo + "' OR MobileNo='" + mobileNo + "'";
              SqlCommand command = new SqlCommand(query, connection);
              connection.Open();
              int rowAffected = command.ExecuteNonQuery();
              connection.Close();
              return rowAffected;
          }

        public List<TestRequest> GetAllTestRequestEntries()
        {
            SqlConnection connection=new SqlConnection(connectionString);
            string quary = " SELECT * FROM TestRequestEntry";
            SqlCommand command = new SqlCommand(quary, connection);
            connection.Open();
            List<TestRequest>testRequests=new List<TestRequest>();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                TestRequest testRequest = new TestRequest();
                testRequest.Id = Convert.ToInt32(reader["Id"].ToString());
                testRequest.NameOfThePatient = reader["NameOfPatient"].ToString();
                testRequest.Dob = Convert.ToDateTime(reader["DOB"].ToString());
                testRequest.BillNo = reader["BillNo"].ToString();
                testRequest.MobileNo = reader["MobileNo"].ToString();
                testRequest.TotalAmount = Convert.ToDecimal(reader["TotalAmount"].ToString());
                testRequest.DueDate = Convert.ToDateTime(reader["DueDate"].ToString());
                testRequest.PaymentStatus = Convert.ToDecimal(reader["PaymentStatus"].ToString());



               testRequests.Add(testRequest);
            }
            reader.Close();
            connection.Close();
            return testRequests;
        }

        public List<TestRequest> GetallTestRequestsByDate(string StartDate, string EndDate)
        {
            SqlConnection connection=new SqlConnection(connectionString);
            string quary =
                "Select * from TestRequestEntry where DueDate Between'" +
                StartDate + "'And'" + EndDate + "'";

            SqlCommand command = new SqlCommand(quary, connection);
            connection.Open();

            SqlDataReader reader = command.ExecuteReader();

            List<TestRequest> unpaidReportList = new List<TestRequest>();

            while (reader.Read())
            {
                TestRequest testRequest=new TestRequest();
                testRequest.Id = Convert.ToInt32(reader["Id"].ToString());
                testRequest.NameOfThePatient = reader["NameOfPatient"].ToString();
                testRequest.BillNo = reader["BillNo"].ToString();
                testRequest.MobileNo = reader["MobileNo"].ToString();
                testRequest.TotalAmount = Convert.ToDecimal(reader["TotalAmount"].ToString());

                testRequest.PaymentStatus = Convert.ToDecimal(reader["PaymentStatus"].ToString());

                

                unpaidReportList.Add(testRequest);
            }



            reader.Close();
            connection.Close();

            return unpaidReportList;
        }

        public List<string> GetallTestRequestsBysearch(string bill)
        { SqlConnection connection=new SqlConnection(connectionString);
            string quary = " SELECT * FROM TestRequestEntry WHERE BillNo='" + bill + "'";
            SqlCommand command = new SqlCommand(quary, connection);
            connection.Open();
            List<string> Set = new List<string>();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                TestRequest testRequest = new TestRequest();
                testRequest.Id = Convert.ToInt32(reader["Id"].ToString());
               
                testRequest.BillNo = reader["BillNo"].ToString();
               


                Set.Add( testRequest.BillNo);
            }
            reader.Close();
            connection.Close();
            return Set;

        }
    }
}